using Microsoft.EntityFrameworkCore;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Repositories;

namespace VacApp_Bovinova_Platform.StaffAdministration.Infrastructure.Persistence.EFC.Repositories;

public class StaffRepository(AppDbContext ctx)
    : BaseRepository<Staff>(ctx), IStaffRepository
{
    public async Task<Staff?> FindByNameAsync(string name)
    {
        return await Context.Set<Staff>().FirstOrDefaultAsync(f=>f.Name == name);
    }
    
    public async Task<IEnumerable<Staff>> FindByCampaignIdAsync(int? campaignId)
    {
        return await Context.Set<Staff>().Where(f => f.CampaignId == campaignId).ToListAsync();
    }
    
    public async Task<IEnumerable<Staff>> FindByEmployeeStatusAsync(int employeeStatus)
    {
        return await Context.Set<Staff>().Where(f => f.EmployeeStatus.Value == employeeStatus).ToListAsync();
    }
    
    public async Task<IEnumerable<Staff>> FindByUserIdAsync(StaffUserId userId)
    {
        return await Context.Set<Staff>().Where(f => f.StaffUserId == userId).ToListAsync();
    }
}