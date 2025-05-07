using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

public interface IBovineQueryService
{
    Task<IEnumerable<Bovine>> Handle(GetAllBovinesQuery query);
    
    Task<Bovine> Handle(GetBovinesByIdQuery query);
}