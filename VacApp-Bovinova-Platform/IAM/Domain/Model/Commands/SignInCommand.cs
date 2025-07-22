namespace VacApp_Bovinova_Platform.IAM.Domain.Model.Commands
{
    public record SignInCommand(
        string? Email,
        string? UserName,
        string Password
    );
}