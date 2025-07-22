using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands.AdminCommands;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.AdminResources;

namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST.Transform.TransformFromAdminResources;

public static class AdminSignInCommandFromResourceAssembler
{
    public static AdminSignInCommand ToCommandFromResource(AdminSignInResource resource)
    {
        return new AdminSignInCommand(resource.Email, resource.Password);
    }
}