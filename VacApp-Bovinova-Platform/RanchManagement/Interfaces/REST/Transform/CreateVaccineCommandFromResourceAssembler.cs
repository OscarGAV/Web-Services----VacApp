using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Transform;

public class CreateVaccineCommandFromResourceAssembler
{
    public static CreateVaccineCommand ToCommandFromResource(CreateVaccineResource resource)
    {
        return new CreateVaccineCommand(
            resource.Name,
            resource.VaccineType,
            resource.VaccineDate,
            resource.VaccineImg,
            resource.BovineId
        );
    }
}