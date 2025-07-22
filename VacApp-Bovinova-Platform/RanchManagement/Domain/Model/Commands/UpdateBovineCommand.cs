namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

public record UpdateBovineCommand(int Id,
    string Name,
    string Gender,
    DateTime? BirthDate,
    string? Breed,
    string? Location,
    int? StableId);