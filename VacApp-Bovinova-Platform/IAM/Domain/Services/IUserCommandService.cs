using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands;

namespace VacApp_Bovinova_Platform.IAM.Domain.Services
{
    public interface IUserCommandService
    {
        Task<string> Handle(SignUpCommand command);
        Task<string> Handle(SignInCommand command);
        Task UpdateUserAsync(User user);
        Task<bool> Handle(UpdateUserCommand command, int userId);
        Task<bool> Handle(DeleteUserCommand command);
    }
}