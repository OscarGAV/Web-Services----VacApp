using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.CampaignManagement.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.CampaignManagement.Interfaces.REST.Transform;

public static class UpdateCampaignStatusFromResourceAssembler
{
    public static UpdateCampaignStatusCommand ToCommandFromResource(UpdateCampaignStatusResource resource, int campaignId) =>
        new UpdateCampaignStatusCommand(campaignId, resource.Status);
}