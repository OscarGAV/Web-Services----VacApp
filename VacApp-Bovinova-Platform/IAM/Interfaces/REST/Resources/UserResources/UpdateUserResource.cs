namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.UserResources;

public record UpdateUserResource(
    string Username,
    string Email
);