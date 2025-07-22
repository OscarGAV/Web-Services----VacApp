using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST.Resources;

public record UpdateStaffResource()
{
    public string Name { get; set; }
    public int EmployeeStatus { get; set; }
    
    public int? CampaignId { get; set; }
}