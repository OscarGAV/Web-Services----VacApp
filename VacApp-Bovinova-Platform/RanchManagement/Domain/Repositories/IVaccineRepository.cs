using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;

public interface IVaccineRepository : IBaseRepository<Vaccine>
{
    Task<Vaccine?> FindByNameAsync(string name);
}