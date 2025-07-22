using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Transform;

public static class UpdateStableCommandFromResourceAssembler
{
    public static UpdateStableCommand ToCommandFromResource(int id, UpdateStableResource resource)
    {
        return new UpdateStableCommand
        (
            Id: id,
            Name: resource.Name,
            Limit: resource.Limit
        );
    }
}