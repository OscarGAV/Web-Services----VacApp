namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

public record CreateBovineCommand(
    string Name,
    string Gender,
    DateTime? BirthDate,
    string? Breed,
    string? Location,
    string? BovineImg);