using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands.AdminCommands;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.AdminResources;

namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST.Transform.TransformFromAdminResources;

public static class CreateAdminCommandFromResourceAssembler
{
    public static CreateAdminCommand ToCommandFromResource(CreateAdminResource resource)
    {
        return new CreateAdminCommand(resource.Email);
    }
}