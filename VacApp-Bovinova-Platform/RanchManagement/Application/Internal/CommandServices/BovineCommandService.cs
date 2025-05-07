using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.RanchManagement.Application.Internal.CommandServices;

public class BovineCommandService(IBovineRepository bovineRepository, 
    IUnitOfWork unitOfWork) : IBovineCommandService
{
    public async Task<Bovine?> Handle(CreateBovineCommand command)
    {
        // Check if a Bovine entity with the given Name already exists
        var bovine = 
            await bovineRepository.FindByNameAsync(command.Name);
        if (bovine != null) 
            throw new Exception($"Bovine entity with name '{command.Name}' already exists.");
        // Create a new Bovine entity from the command data
        bovine = new Bovine(command);

        try
        {
            // Add the new Bovine entity to the repository and complete the transaction
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
}