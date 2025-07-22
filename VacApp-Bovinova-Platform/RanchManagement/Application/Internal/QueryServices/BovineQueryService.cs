using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

namespace VacApp_Bovinova_Platform.RanchManagement.Application.Internal.QueryServices;

public class BovineQueryService(IBovineRepository bovineRepository) : IBovineQueryService
{
    /// <summary>
    /// Retrieves all Bovines
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Bovine>> Handle(GetAllBovinesQuery query)
    {
        return await bovineRepository.FindByUserIdAsync(new RanchUserId(query.UserId));
    }
    
    /// <summary>
    /// Retrieves a Bovine entity by its unique identifier.
    /// </summary>
    /// <param name="query"></param>
    /// <returns> The Bovine entity with the specified ID, if found; otherwise, null. </returns>
    public async Task<Bovine> Handle(GetBovinesByIdQuery query)
    {
        return await bovineRepository.FindByIdAsync(query.Id);
    }
    
    /// <summary>
    /// Retrieves all bovines by stable ID.
    /// </summary>
    /// <param name="query"></param>
    /// <returns> A collection of bovines associated with the specified stable ID. </returns>
    public async Task<IEnumerable<Bovine>> Handle(GetBovinesByStableIdQuery query)
    {
        return await bovineRepository.FindByStableIdAsync(query.StableId);
    }
    
    public async Task<int> CountBovinesByUserIdAsync(RanchUserId userId)
    {
        var bovines = await bovineRepository.FindByUserIdAsync(userId);
        return bovines.Count();
    }
}