namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;

public record UpdateCampaignStatusCommand(int id, string status);