using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Queries;

namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Services;

public interface ICampaignCommandService
{
    Task<Campaign?> Handle(CreateCampaignCommand command);
    
    Task<IEnumerable<Campaign>> Handle(DeleteCampaignCommand command);

    Task<Campaign?> Handle(UpdateCampaignStatusCommand command);

    Task<Campaign?> Handle(AddGoalToCampaignCommand command);
    
    Task<Campaign?> Handle(AddChannelToCampaignCommand command);

    Task<Goal?> Handle(UpdateGoalCommand command);
}