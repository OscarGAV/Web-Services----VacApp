namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.ValueObjects;

public record CampaignUserId(int UserIdentifier)
{
    public CampaignUserId() : this(0) { }
}