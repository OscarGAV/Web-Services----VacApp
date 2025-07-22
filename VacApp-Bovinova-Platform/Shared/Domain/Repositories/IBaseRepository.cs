namespace VacApp_Bovinova_Platform.Shared.Domain.Repositories;

/// <summary>
///     Base repository interface for all repositories
/// </summary>
/// <remarks>
/// This interface is used to define the basic CRUD operations for all repositories.
/// </remarks>
/// <typeparam name="TEntity">The Entity Type</typeparam>
public interface IBaseRepository<TEntity>
{
    /// <summary>
    ///  Adds an entity to the repository
    /// </summary>
    /// <param name="entity">Entity object to add</param>
    /// <returns></returns>
    Task AddAsync(TEntity entity);
    
    /// <summary>
    ///  Update entity
    /// </summary>
    /// <param name="entity">The entity object to update</param>
    void Update(TEntity entity);
    
    /// <summary>
    /// Remove entity
    /// </summary>
    /// <param name="entity">The entity to remove</param>
    void Remove(TEntity entity);
    
    /// <summary>
    /// Find entity by id
    /// </summary>
    /// <param name="id">The entity ID to find</param>
    /// <returns>Entity object if found</returns>
    Task<TEntity?> FindByIdAsync(int id);
    
    /// <summary>
    /// Get All entities
    /// </summary>
    /// <returns>An Enumerable containing all entity objects</returns>
    Task<IEnumerable<TEntity>> ListAsync();
    
    
    Task SaveChangesAsync();

}