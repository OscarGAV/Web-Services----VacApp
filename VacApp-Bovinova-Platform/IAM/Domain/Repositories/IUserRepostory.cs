using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.IAM.Domain.Repositories
{
    public interface IUserRepostory : IBaseRepository<User>
    {
        Task<User?> FindByEmailAsync(string email);
        
        Task<User?> FindByNameAsync(string name);
        
        Task<IEnumerable<User>> FindAllAsync();
        
        Task UpdateAsync(User user);
        
        Task DeleteAsync(User user);

    }
}