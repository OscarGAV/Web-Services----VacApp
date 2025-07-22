using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Services;

public interface ICampaignQueryService
{
    Task<Campaign?> Handle(GetCampaignByIdQuery query);
    
    Task<IEnumerable<Campaign>> Handle(GetAllCampaignsQuery query);

    Task<IEnumerable<Goal>> Handle(GetGoalsFromCampaignIdQuery query);
    
    Task<IEnumerable<Channel>> Handle(GetChannelsFromCampaignIdQuery query);
    
    Task<int> CountCampaignsByUserIdAsync(CampaignUserId userId);
}