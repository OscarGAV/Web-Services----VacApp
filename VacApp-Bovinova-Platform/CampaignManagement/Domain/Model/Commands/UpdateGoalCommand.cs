namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;

public record UpdateGoalCommand(int CampaignId, string description, string metric, int targetValue, int currentValue);