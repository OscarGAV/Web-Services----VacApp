using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;

namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;

// public record AddGoalToCampaignCommand(int CampaignId, string Description, string Metric, int TargetValue, int CurrentValue);

public record AddGoalToCampaignCommand(int CampaignId, Goal Goal);