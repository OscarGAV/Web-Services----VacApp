using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

namespace VacApp_Bovinova_Platform.RanchManagement.Application.Internal.QueryServices;

public class StableQueryService(IStableRepository stableRepository) : IStableQueryService
{
    /// <summary>
    /// Retrieves all Stables
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Stable>> Handle(GetAllStablesQuery query)
    {
        return await stableRepository.FindByUserIdAsync(new RanchUserId(query.UserId));
    }
    
    /// <summary>
    /// Retrieves a Stable entity by its unique identifier.
    /// </summary>
    /// <param name="query"></param>
    /// <returns> The Stable entity with the specified ID, if found; otherwise, null. </returns>
    public async Task<Stable> Handle(GetStablesByIdQuery query)
    {
        return await stableRepository.FindByIdAsync(query.Id);
    }
    
    public async Task<int> CountStablesByUserIdAsync(RanchUserId userId)
    {
        var stables = await stableRepository.FindByUserIdAsync(userId);
        return stables.Count();
    }
}