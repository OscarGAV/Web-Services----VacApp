using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Queries;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Repositories;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Services;

namespace VacApp_Bovinova_Platform.StaffAdministration.Application.Internal.QueryServices;

public class StaffQueryService(IStaffRepository staffRepository) : IStaffQueryService
{
    /// <summary>
    /// Retrieves all Staffs
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Staff>> Handle(GetAllStaffQuery query)
    {
        return await staffRepository.FindByUserIdAsync(new StaffUserId(query.UserId));
    }
    
    /// <summary>
    /// Retrieves a Staff entity by its unique identifier.
    /// </summary>
    /// <param name="query"></param>
    /// <returns> The Staff entity with the specified ID, if found; otherwise, null. </returns>
    public async Task<Staff> Handle(GetStaffByIdQuery query)
    {
        return await staffRepository.FindByIdAsync(query.Id);
    }
    
    /// <summary>
    /// Retrieves all staffs by campaign ID.
    /// </summary>
    /// <param name="query"></param>
    /// <returns> A collection of staffs associated with the specified campaign ID. </returns>
    public async Task<IEnumerable<Staff>> Handle(GetStaffByCampaignIdQuery query)
    {
        return await staffRepository.FindByCampaignIdAsync(query.CampaignId);
    }
    
    /// <summary>
    /// Retrieves all staffs by status.
    /// </summary>
    /// <param name="query"></param>
    /// <returns> A collection of staffs associated with the specified employee status. </returns>
    public async Task<IEnumerable<Staff>> Handle(GetStaffByEmployeeStatusQuery query)
    {
        return await staffRepository.FindByEmployeeStatusAsync(query.EmployeeStatus);
    }
    
    /// <summary>
    /// Retrieves a Staff entity by its name.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<Staff> Handle(GetStaffByNameQuery query)
    {
        return await staffRepository.FindByNameAsync(query.Name);
    }
    
    public async Task<int> CountStaffsByUserIdAsync(StaffUserId userId)
    {
        var staffs = await staffRepository.FindByUserIdAsync(userId);
        return staffs.Count();
    }
}