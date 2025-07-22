using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

public interface IStableQueryService
{
    Task<IEnumerable<Stable>> Handle(GetAllStablesQuery query);
    
    Task<Stable> Handle(GetStablesByIdQuery query);
    Task<int> CountStablesByUserIdAsync(RanchUserId userId);
}