using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.CampaignManagement.Interfaces.REST.Transform;

public static class GoalResourceFromEntityAssembler
{
    public static GoalResource ToResourceFromEntity(Goal goal) =>
    new GoalResource(goal.Id, goal.Description, goal.Metric, goal.TargetValue, goal.CurrentValue, goal.CampaignId);
}