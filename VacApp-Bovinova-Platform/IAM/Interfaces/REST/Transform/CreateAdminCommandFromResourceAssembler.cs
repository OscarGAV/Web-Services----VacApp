using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.AdminResources;

namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST.Transform;

public static class CreateAdminCommandFromResourceAssembler
{
    public static CreateAdminCommand ToCommandFromResource(CreateAdminResource resource)
    {
        return new CreateAdminCommand(resource.Email);
    }
}