using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Transform;

public static class UpdateVaccineCommandFromResourceAssembler
{
    public static UpdateVaccineCommand ToCommandFromResource(int id, UpdateVaccineResource resource)
    {
        return new UpdateVaccineCommand
        (
            Id: id,
            Name: resource.Name,
            VaccineType: resource.VaccineType,
            VaccineDate: resource.VaccineDate,
            BovineId: resource.BovineId
        );
    }
}