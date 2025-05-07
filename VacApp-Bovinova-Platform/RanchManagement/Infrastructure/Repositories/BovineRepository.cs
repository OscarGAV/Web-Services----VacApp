using Microsoft.EntityFrameworkCore;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace VacApp_Bovinova_Platform.RanchManagement.Infrastructure.Repositories;

public class BovineRepository(AppDbContext ctx)
    : BaseRepository<Bovine>(ctx), IBovineRepository
{
    public async Task<Bovine?> FindByNameAsync(string name)
    {
        return await Context.Set<Bovine>().FirstOrDefaultAsync(f=>f.Name == name);
    }
}