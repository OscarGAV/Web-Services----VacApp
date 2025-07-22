using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.IAM.Domain.Repositories;

public interface IAdminRepository: IBaseRepository<Admin>
{
    Task<Admin?> FindByEmailAsync(string email);
    Task<IEnumerable<Admin>> FindAllAsync();
    Task UpdateAsync(Admin admin);
    Task DeleteAsync(Admin admin);
}