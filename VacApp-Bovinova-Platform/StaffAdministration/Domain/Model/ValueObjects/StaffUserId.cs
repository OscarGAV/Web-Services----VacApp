namespace VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.ValueObjects;

public record StaffUserId(int UserIdentifier)
{
    public StaffUserId() : this(0) { }
}