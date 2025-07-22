using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Commands;

namespace VacApp_Bovinova_Platform.StaffAdministration.Domain.Services;

public interface IStaffCommandService
{
    Task<Staff?> Handle(CreateStaffCommand command);
    
    Task<Staff?> Handle(UpdateStaffCommand command);
    
    Task<Staff?> Handle(DeleteStaffCommand command);
}