using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;

public record CreateCampaignCommand(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    string Status,
    List<Goal> Goals,
    List<Channel> Channel,
    int? StableId,
    CampaignUserId? CampaignUserId
    );