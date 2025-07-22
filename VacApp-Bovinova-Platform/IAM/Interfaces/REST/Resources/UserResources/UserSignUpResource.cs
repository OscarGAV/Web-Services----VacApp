namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.UserResources
{
    public record SignUpResource(
        string Username,
        string Password,
        string Email
    );
}