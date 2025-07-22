using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.CampaignManagement.Interfaces.REST.Transform;

public static class CampaignResourceFromEntityAssembler
{
    public static CampaignResource ToResourceFromEntity(Campaign campaign) =>
    new CampaignResource(
        campaign.Id, 
        campaign.Name, 
        campaign.Description, 
        campaign.StartDate, 
        campaign.EndDate, 
        campaign.Status, 
        campaign.Goals, 
        campaign.Channels,
        campaign.StableId);
}