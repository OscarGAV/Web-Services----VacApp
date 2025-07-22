using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Transform;

public static class VaccineResourceFromEntityAssembler
{
    public static VaccineResource ToResourceFromEntity(Vaccine entity)
    {
        return new VaccineResource(entity.Id,
            entity.Name,
            entity.VaccineType,
            entity.VaccineDate,
            entity.VaccineImg,
            entity.BovineId
        );
    }
}