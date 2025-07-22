using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

public interface IBovineQueryService
{
    Task<IEnumerable<Bovine>> Handle(GetAllBovinesQuery query);
    
    Task<Bovine> Handle(GetBovinesByIdQuery query);
    
    Task<IEnumerable<Bovine>> Handle(GetBovinesByStableIdQuery query);
    Task<int> CountBovinesByUserIdAsync(RanchUserId userId);
}