using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Commands;
using VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST.Transform;

public static class UpdateStaffCommandFromResourceAssembler
{
    public static UpdateStaffCommand ToCommandFromResource(int id, UpdateStaffResource resource)
    {
        return new UpdateStaffCommand
        (
            Id: id,
            Name:resource.Name,
            EmployeeStatus:resource.EmployeeStatus,
            CampaignId:resource.CampaignId
        );
    }
}