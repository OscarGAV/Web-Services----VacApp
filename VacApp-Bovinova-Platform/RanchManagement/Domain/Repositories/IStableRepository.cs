using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;

public interface IStableRepository : IBaseRepository<Stable>
{
    Task<Stable?> FindByNameAsync(string name);
    Task<IEnumerable<Stable>> FindByUserIdAsync(RanchUserId userId);
}