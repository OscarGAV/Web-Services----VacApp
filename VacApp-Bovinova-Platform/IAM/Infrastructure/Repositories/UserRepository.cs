using Microsoft.EntityFrameworkCore;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Repositories;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace VacApp_Bovinova_Platform.IAM.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepostory
    {
        public async Task<User?> FindByEmailAsync(string email)
        {
            return await context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }
        
        public async Task<User?> FindByNameAsync(string name)
        {
            return await context.Set<User>().FirstOrDefaultAsync(u => u.Username == name);
        }
        
        public async Task<IEnumerable<User>> FindAllAsync()
        {
            return await context.Set<User>().ToListAsync();
        }
        
        public async Task UpdateAsync(User user)
        {
            context.Set<User>().Update(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            context.Set<User>().Remove(user);
            await context.SaveChangesAsync();
        }
    }
}