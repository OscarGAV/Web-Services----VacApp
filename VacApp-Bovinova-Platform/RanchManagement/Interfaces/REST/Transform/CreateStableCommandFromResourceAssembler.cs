using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Transform;

public static class CreateStableCommandFromResourceAssembler
{
    public static CreateStableCommand ToCommandFromResource(CreateStableResource resource, int userId)
    {
        return new CreateStableCommand(
            resource.Name,
            resource.Limit,
            new RanchUserId(userId)
        );
    }
}