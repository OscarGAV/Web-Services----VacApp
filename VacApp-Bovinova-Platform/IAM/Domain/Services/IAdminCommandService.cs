using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands.AdminCommands;

namespace VacApp_Bovinova_Platform.IAM.Domain.Services;

public interface IAdminCommandService
{
    Task<string> Handle(CreateAdminCommand command);
    Task<string> Handle(AdminSignInCommand command);
    Task<bool> Handle(UpdateAdminCommand command, int adminId);
    Task<bool> DeleteAdminAsync(int adminId);
}