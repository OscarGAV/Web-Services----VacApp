using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

public interface IVaccineQueryService
{
    Task<IEnumerable<Vaccine>> Handle(GetAllVaccinesQuery query);
    
    Task<Vaccine> Handle(GetVaccinesByIdQuery query);
}