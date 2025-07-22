using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;

namespace VacApp_Bovinova_Platform.CampaignManagement.Interfaces.REST.Resources;

public record CreateCampaignResource(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    string Status,
    List<Goal> Goals,
    List<Channel> Channels,
    int? StableId
    );