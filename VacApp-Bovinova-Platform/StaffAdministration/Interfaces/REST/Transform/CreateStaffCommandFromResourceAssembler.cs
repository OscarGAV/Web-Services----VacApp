using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Commands;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST.Transform;

public static class CreateStaffCommandFromResourceAssembler
{
    public static CreateStaffCommand ToCommandFromResource(CreateStaffResource resource, int userId)
    {
        return new CreateStaffCommand(
            resource.Name,
            resource.EmployeeStatus,
            resource.CampaignId,
            new StaffUserId(userId)
        );
    }
}