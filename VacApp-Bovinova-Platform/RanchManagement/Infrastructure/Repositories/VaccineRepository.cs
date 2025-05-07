using Microsoft.EntityFrameworkCore;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace VacApp_Bovinova_Platform.RanchManagement.Infrastructure.Repositories;

public class VaccineRepository(AppDbContext ctx)
    : BaseRepository<Vaccine>(ctx), IVaccineRepository
{
    public async Task<Vaccine?> FindByNameAsync(string name)
    {
        return await Context.Set<Vaccine>().FirstOrDefaultAsync(f => f.Name == name);
    }
}