using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.AdminResources;

namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST.Transform;

public static class UpdateAdminCommandFromResourceAssembler
{
    public static UpdateAdminCommand ToCommandFromResource(UpdateAdminResource resource)
    {
        return new UpdateAdminCommand(resource.Email);
    }
}