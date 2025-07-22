using VacApp_Bovinova_Platform.Shared.Domain.Repositories;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Commands;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Repositories;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Services;

namespace VacApp_Bovinova_Platform.StaffAdministration.Application.Internal.CommandServices;

public class StaffCommandService(IStaffRepository staffRepository, 
    IUnitOfWork unitOfWork) : IStaffCommandService
{
    /// <summary>
    /// Handles the creation of a new Staff entity.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Staff?> Handle(CreateStaffCommand command)
    {
        // Check if a Staff entity with the given Name already exists
        var staff = 
            await staffRepository.FindByNameAsync(command.Name);
        if (staff != null) 
            throw new Exception($"Staff entity with name '{command.Name}' already exists.");
        // Create a new Staff entity from the command data
        staff = new Staff(command);

        try
        {
            // Add the new Staff entity to the repository and complete the transaction
            await staffRepository.AddAsync(staff);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return staff;
    }
    
    /// <summary>
    /// Handles the update of an existing staff entity.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Staff?> Handle(UpdateStaffCommand command)
    {
        // Verifies if the staff exists
        var staff = await staffRepository.FindByIdAsync(command.Id);
        if (staff == null)
        {
            throw new Exception($"Staff with ID '{command.Id}' not found.");
        }

        // Updates the staff entity
        staff.Update(command);

        try
        {
            // Updates the staff in the repository and saves changes
            staffRepository.Update(staff);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return staff;
    }
    
    
    /// <summary>
    /// Handles the deletion of an existing staff entity.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Staff?> Handle(DeleteStaffCommand command)
    {
        // Verifies if the staff exists
        var staff = await staffRepository.FindByIdAsync(command.Id);
        if (staff == null)
        {
            throw new Exception($"Staff with ID '{command.Id}' not found.");
        }

        try
        {
            // Deletes the staff from the repository and saves changes
            staffRepository.Remove(staff);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return staff;
    }
}