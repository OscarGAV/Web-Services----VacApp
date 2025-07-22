using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;
using VacApp_Bovinova_Platform.Shared.Application.OutboundServices;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.RanchManagement.Application.Internal.CommandServices;

public class VaccineCommandService(
    IVaccineRepository vaccineRepository,
    IMediaStorageService mediaStorageService,
    IUnitOfWork unitOfWork) : IVaccineCommandService
{
    public async Task<Vaccine?> Handle(CreateVaccineCommand command)
    {
        // Check if a Vaccine entity with the given Name already exists
        var vaccine =
            await vaccineRepository.FindByNameAsync(command.Name);
        if (vaccine != null)
            throw new Exception($"Vaccine entity with name '{command.Name}' already exists.");
        
        // Create a new Vaccine entity from the command data
        if (command.FileData is not null)
        {
            var vaccineImg = mediaStorageService.UploadFileAsync(command.Name, command.FileData);
            var commandWithImg = command with { VaccineImg = vaccineImg };
            vaccine = new Vaccine(commandWithImg);
        }
        else
        {
            var commandWithImg = command with { VaccineImg = "https://placehold.co/600x400" };
            vaccine = new Vaccine(commandWithImg);
        }

        try
        {
            // Add the new Vaccine entity to the repository and complete the transaction
            await vaccineRepository.AddAsync(vaccine);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return vaccine;
    }

    /// <summary>
    /// Handles the update of an existing vaccine entity.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Vaccine?> Handle(UpdateVaccineCommand command)
    {
        // Verifies if the vaccine exists
        var vaccine = await vaccineRepository.FindByIdAsync(command.Id);
        if (vaccine == null)
        {
            throw new Exception($"Vaccine with ID '{command.Id}' not found.");
        }

        // Updates the vaccine entity
        vaccine.Update(command);

        try
        {
            // Updates the vaccine in the repository and saves changes
            vaccineRepository.Update(vaccine);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return vaccine;
    }


    /// <summary>
    /// Handles the deletion of an existing vaccine entity.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Vaccine?> Handle(DeleteVaccineCommand command)
    {
        // Verifies if the vaccine exists
        var vaccine = await vaccineRepository.FindByIdAsync(command.Id);
        if (vaccine == null)
        {
            throw new Exception($"Vaccine with ID '{command.Id}' not found.");
        }

        try
        {
            // Deletes the vaccine from the repository and saves changes
            vaccineRepository.Remove(vaccine);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return vaccine;
    }
}