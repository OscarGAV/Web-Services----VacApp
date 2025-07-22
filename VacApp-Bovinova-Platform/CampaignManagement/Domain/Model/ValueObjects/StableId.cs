namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.ValueObjects;

public record StableId(int StableIdentifier)
{
    public StableId() : this(0) { }
}