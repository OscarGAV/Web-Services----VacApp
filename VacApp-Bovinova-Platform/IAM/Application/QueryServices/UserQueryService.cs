using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Queries;
using VacApp_Bovinova_Platform.IAM.Domain.Repositories;
using VacApp_Bovinova_Platform.IAM.Domain.Services;

namespace VacApp_Bovinova_Platform.IAM.Application.QueryServices
{
    public class UserQueryService(
        IUserRepostory userRepository
        ) : IUserQueryService
    {
        public async Task<User?> Handle(GetUserByIdQuery query)
        {
            return await userRepository.FindByIdAsync(query.Id);
        }
        
        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
        {
            return await userRepository.FindAllAsync();
        }
        
        public async Task<User?> Handle(GetUserByEmailQuery query)
        {
            return await userRepository.FindByEmailAsync(query.Email);
        }
        
        public async Task<User?> Handle(GetUserByNameQuery query)
        {
            return await userRepository.FindByNameAsync(query.UserName);
        }
        
        public async Task<string?> GetUserNameByEmail(string email)
        {
            var user = await userRepository.FindByEmailAsync(email);
            return user?.Username;
        }
        
        public async Task<string?> GetEmailByUserName(string userName)
        {
            var user = await userRepository.FindByNameAsync(userName);
            return user?.Email;
        }
    }
}