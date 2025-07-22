using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands.UserCommands;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.UserResources;

namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST.Transform.TransformFromUserResources;

public static class UpdateUserCommandFromResourceAssembler
{
    public static UpdateUserCommand ToCommandFromResource(UpdateUserResource resource)
    {
        return new UpdateUserCommand(resource.Username, resource.Email);
    }
}