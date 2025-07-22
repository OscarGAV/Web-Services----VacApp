using Swashbuckle.AspNetCore.Annotations;

namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

public record CreateBovineResource(
    string Name,
    string Gender,
    DateTime? BirthDate,
    string? Breed,
    string? Location,
    IFormFile? FileData,
    int? StableId);