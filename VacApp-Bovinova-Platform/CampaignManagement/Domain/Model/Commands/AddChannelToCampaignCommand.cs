using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;

namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;

public record AddChannelToCampaignCommand(int CampaignId, Channel Channel);