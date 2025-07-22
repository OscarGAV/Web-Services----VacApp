using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

namespace VacApp_Bovinova_Platform.RanchManagement.Application.Internal.QueryServices;

public class VaccineQueryService(IVaccineRepository vaccineRepository) : IVaccineQueryService
{
    /// <summary>
    /// Retrieves all Vaccines
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Vaccine>> Handle(GetAllVaccinesQuery query)
    {
        return await vaccineRepository.FindByUserIdAsync(new RanchUserId(query.UserId));
    }
    
    
    /// <summary>
    /// Retrieves a Vaccine entity by its unique identifier.
    /// </summary>
    /// <param name="query"></param>
    /// <returns> The Vaccine entity with the specified ID, if found; otherwise, null. </returns>
    public async Task<Vaccine> Handle(GetVaccinesByIdQuery query)
    {
        return await vaccineRepository.FindByIdAsync(query.Id);
    }
    
    /// <summary>
    /// Retrieves all vaccines by vaccine ID.
    /// </summary>
    /// <param name="query"></param>
    /// <returns> A collection of vaccines associated with the specified vaccine ID. </returns>
    public async Task<IEnumerable<Vaccine>> Handle(GetVaccinesByBovineIdQuery query)
    {
        return await vaccineRepository.FindByBovineIdAsync(query.BovineId);
    }
    
    public async Task<int> CountVaccinesByUserIdAsync(RanchUserId userId)
    {
        var vaccines = await vaccineRepository.FindByUserIdAsync(userId);
        return vaccines.Count();
    }
}