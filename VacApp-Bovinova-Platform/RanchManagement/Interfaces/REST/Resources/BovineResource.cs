namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

public record BovineResource(int Id,
    string Name,
    string Gender,
    DateTime? BirthDate,
    string? Breed,
    string? Location,
    string? BovineImg,
    int? StableId);