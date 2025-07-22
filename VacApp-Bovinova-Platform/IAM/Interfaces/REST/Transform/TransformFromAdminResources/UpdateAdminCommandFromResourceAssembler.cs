using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands.AdminCommands;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.AdminResources;

namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST.Transform.TransformFromAdminResources;

public static class UpdateAdminCommandFromResourceAssembler
{
    public static UpdateAdminCommand ToCommandFromResource(UpdateAdminResource resource)
    {
        return new UpdateAdminCommand(resource.Email);
    }
}