using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Transform;

public class CreateBovineCommandFromResourceAssembler
{
    public static CreateBovineCommand ToCommandFromResource(CreateBovineResource resource)
    {
        return new CreateBovineCommand(
            resource.Name,
            resource.Gender,
            resource.BirthDate,
            resource.Breed,
            resource.Location,
            resource.BovineImg
        );
    }
}