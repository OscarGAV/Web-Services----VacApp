namespace VacApp_Bovinova_Platform.CampaignManagement.Interfaces.REST.Resources;

public record AddGoalToCampaignResource(
    string Description,
    string Metric,
    int TargetValue,
    int CurrentValue
    );