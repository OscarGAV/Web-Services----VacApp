namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

public class UpdateBovineResource
{
    /*
    string Name,
    string Gender,
    DateTime? BirthDate,
    string? Breed,
    string? Location,
    string? BovineImg,
    int? StableId
     */

    public string Name { get; set; }
    public string Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Breed { get; set; }
    public string? Location { get; set; }
    public int? StableId { get; set; }
}