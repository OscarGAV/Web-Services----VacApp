using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST.Resources;

public record StaffResource(int Id,
    string Name,
    int EmployeeStatus,
    int? CampaignId);