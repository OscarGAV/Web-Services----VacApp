using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Transform;

public static class StableResourceFromEntityAssembler
{
    public static StableResource ToResourceFromEntity(Stable stable)
    {
        return new StableResource(
            stable.Id,
            stable.Name,
            stable.Limit
        );
    }
}