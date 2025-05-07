namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

public record CreateVaccineCommand(
    string Name,
    string? VaccineType,
    DateTime? VaccineDate,
    string? VaccineImg, 
    int BovineId);