using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.AdminResources;

namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST.Transform;

public static class AdminSignInCommandFromResourceAssembler
{
    public static AdminSignInCommand ToCommandFromResource(AdminSignInResource resource)
    {
        return new AdminSignInCommand(resource.Email, resource.Password);
    }
}