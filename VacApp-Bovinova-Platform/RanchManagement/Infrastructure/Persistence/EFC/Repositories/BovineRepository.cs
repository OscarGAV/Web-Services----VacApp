using Microsoft.EntityFrameworkCore;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace VacApp_Bovinova_Platform.RanchManagement.Infrastructure.Persistence.EFC.Repositories;

public class BovineRepository(AppDbContext ctx)
    : BaseRepository<Bovine>(ctx), IBovineRepository
{
    public async Task<Bovine?> FindByNameAsync(string name)
    {
        return await Context.Set<Bovine>().FirstOrDefaultAsync(f=>f.Name == name);
    }
    
    public async Task<IEnumerable<Bovine>> FindByStableIdAsync(int? stableId)
    {
        return await Context.Set<Bovine>().Where(f => f.StableId == stableId).ToListAsync();
    }
    
    public async Task<IEnumerable<Bovine>> FindByUserIdAsync(RanchUserId userId)
    {
        return await Context.Set<Bovine>().Where(f => f.RanchUserId == userId).ToListAsync();
    }
    
    public async Task<int> CountBovinesByStableIdAsync(int stableId)
    {
        return await Context.Set<Bovine>().CountAsync(b => b.StableId == stableId);
    }
}