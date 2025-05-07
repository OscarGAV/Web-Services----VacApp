namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

public record VaccineResource(int Id,
    string Name,
    string? VaccineType,
    DateTime? VaccineDate,
    string? VaccineImg, 
    int BovineId);