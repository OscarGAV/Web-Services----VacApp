using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Transform;

public static class BovineResourceFromEntityAssembler
{
    public static BovineResource ToResourceFromEntity(Bovine entity)
    {
        return new BovineResource(entity.Id,
            entity.Name,
            entity.Gender,
            entity.BirthDate,
            entity.Breed,
            entity.Location,
            entity.BovineImg,
            entity.StableId
        );
    }
}