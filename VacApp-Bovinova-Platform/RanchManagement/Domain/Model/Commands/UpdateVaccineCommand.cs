namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

public record UpdateVaccineCommand(int Id,
    string Name,
    string? VaccineType,
    DateTime? VaccineDate,
    int BovineId);