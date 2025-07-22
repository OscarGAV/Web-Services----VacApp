using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;

namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;

public class Goal
{
    public int Id { get; private set;}
    public string Description {get; private set;}
    public string Metric {get; private set;}
    public int TargetValue {get; private set;}
    public int CurrentValue { get; private set; }
    
    public int CampaignId { get; private set; }
    
    public Goal()
    {
        this.Description = string.Empty;
        this.Metric = string.Empty;
        this.TargetValue = 0;
        this.CurrentValue = 0;
    }

    public Goal(string description, string metric, int targetValue, int currentValue, int campaignId)
    {
        this.Description = description;
        this.Metric = metric;
        this.TargetValue = targetValue;
        this.CurrentValue = currentValue;   
        this.CampaignId = campaignId;
    }

    public void UpdateValues(string description, string metric, int targetValue, int currentValue)
    {
        this.Description = description;
        this.Metric = metric;
        this.TargetValue = targetValue;
        this.CurrentValue = currentValue;   
    }

    /*
    public void AddGoalToCampaign(AddGoalToCampaignCommand command)
    {
        this.Description = command.Description;
        this.Metric = command.Metric;
        this.TargetValue = command.TargetValue;
        this.CurrentValue = command.CurrentValue;
    }
    */
}