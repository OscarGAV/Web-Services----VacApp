namespace VacApp_Bovinova_Platform.CampaignManagement.Interfaces.REST.Resources;

public record GoalResource(
    int Id,
    string Description,
    string Metric, 
    int TargetValue, 
    int CurrentValue,
    int CampaignId
    );