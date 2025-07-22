using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Queries;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.StaffAdministration.Domain.Services;

public interface IStaffQueryService
{
    Task<IEnumerable<Staff>> Handle(GetAllStaffQuery query);
    
    Task<Staff> Handle(GetStaffByIdQuery query);
    Task<IEnumerable<Staff>> Handle(GetStaffByCampaignIdQuery query);
    
    Task<IEnumerable<Staff>> Handle(GetStaffByEmployeeStatusQuery query);
    
    Task<Staff> Handle(GetStaffByNameQuery query);
    
    Task<int> CountStaffsByUserIdAsync(StaffUserId userId);
}