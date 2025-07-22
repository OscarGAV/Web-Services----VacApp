namespace VacApp_Bovinova_Platform.IAM.Domain.Model.Commands.UserCommands;

public record UpdateUserCommand(
    string Username, 
    string Email
);