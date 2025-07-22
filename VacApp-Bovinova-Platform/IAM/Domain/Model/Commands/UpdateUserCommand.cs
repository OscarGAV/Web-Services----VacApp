namespace VacApp_Bovinova_Platform.IAM.Domain.Model.Commands;

public record UpdateUserCommand(
    string Username, 
    string Email
);