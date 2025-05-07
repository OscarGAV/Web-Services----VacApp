using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.RanchManagement.Application.Internal.CommandServices;

public class VaccineCommandService(IVaccineRepository vaccineRepository,
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
        vaccine = new Vaccine(command);

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
}