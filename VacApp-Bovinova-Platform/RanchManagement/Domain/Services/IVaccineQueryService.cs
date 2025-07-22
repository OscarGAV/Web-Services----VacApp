using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

public interface IVaccineQueryService
{
    Task<IEnumerable<Vaccine>> Handle(GetAllVaccinesQuery query);
    
    Task<Vaccine> Handle(GetVaccinesByIdQuery query);
    
    Task<IEnumerable<Vaccine>> Handle(GetVaccinesByBovineIdQuery query);
    Task<int> CountVaccinesByUserIdAsync(RanchUserId userId);
}