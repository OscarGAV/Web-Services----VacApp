using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;
using VacApp_Bovinova_Platform.Shared.Application.OutboundServices;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.RanchManagement.Application.Internal.CommandServices;

public class BovineCommandService(IBovineRepository bovineRepository,
    IStableRepository stableRepository,
    IMediaStorageService mediaStorageService,
    IUnitOfWork unitOfWork) : IBovineCommandService
{
    /// <summary>
    /// Handles the creation of a new bovine entity.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Bovine?> Handle(CreateBovineCommand command)
    {
        if (!command.StableId.HasValue)
        {
            throw new Exception("StableId is required.");
        }

        // Verifies if the stable exists
        var stable = await stableRepository.FindByIdAsync(command.StableId.Value);
        if (stable == null)
        {
            throw new Exception($"Stable with ID '{command.StableId}' not found.");
        }

        // Count the current bovines in the stable
        var currentBovineCount = await bovineRepository.CountBovinesByStableIdAsync(command.StableId.Value);
        if (currentBovineCount >= stable.Limit)
        {
            throw new Exception("El establo está lleno. Si quiere añadir más bovinos en este establo deberá incrementar su capacidad máxima.");
        }

        // Verifies if the bovine with the same name already exists
        var bovine = await bovineRepository.FindByNameAsync(command.Name);
        if (bovine != null)
        {
            throw new Exception($"Bovine entity with name '{command.Name}' already exists.");
        }

        // Creates a new bovine entity
        if (command.FileData is not null)
        {
            var bovineImg = mediaStorageService.UploadFileAsync(command.Name, command.FileData);
            var commandWithImg = command with { BovineImg = bovineImg };
            bovine = new Bovine(commandWithImg);
        }
        else
        {
            var commandWithImg = command with { BovineImg = "https://placehold.co/600x400" };
            bovine = new Bovine(commandWithImg);
        }

        try
        {
            // Adds the bovine to the repository and saves changes
            await bovineRepository.AddAsync(bovine);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return bovine;
    }

    /// <summary>
    /// Handles the update of an existing bovine entity.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Bovine?> Handle(UpdateBovineCommand command)
    {
        // Verifies if the bovine exists
        var bovine = await bovineRepository.FindByIdAsync(command.Id);
        if (bovine == null)
        {
            throw new Exception($"Bovine with ID '{command.Id}' not found.");
        }

        // Updates the bovine entity
        bovine.Update(command);

        try
        {
            // Updates the bovine in the repository and saves changes
            bovineRepository.Update(bovine);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return bovine;
    }


    /// <summary>
    /// Handles the deletion of an existing bovine entity.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Bovine?> Handle(DeleteBovineCommand command)
    {
        // Verifies if the bovine exists
        var bovine = await bovineRepository.FindByIdAsync(command.Id);
        if (bovine == null)
        {
            throw new Exception($"Bovine with ID '{command.Id}' not found.");
        }

        try
        {
            // Deletes the bovine from the repository and saves changes
            bovineRepository.Remove(bovine);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return bovine;
    }
}