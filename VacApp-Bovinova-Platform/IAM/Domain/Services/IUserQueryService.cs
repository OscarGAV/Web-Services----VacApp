using System.Collections;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Queries;

namespace VacApp_Bovinova_Platform.IAM.Domain.Services
{
    public interface IUserQueryService
    {
        Task<User?> Handle(GetUserByIdQuery query);
        Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
        Task<User?> Handle(GetUserByEmailQuery query);
        Task<User?> Handle(GetUserByNameQuery query);
        Task<string?> GetUserNameByEmail(string email);
        Task<string?> GetEmailByUserName(string userName);
    }
}