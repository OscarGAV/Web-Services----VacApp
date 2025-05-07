using System.ComponentModel.DataAnnotations;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;

public class Bovine
{
    /// <summary>
    /// Entity Identifier
    /// </summary>
    [Required]
    public int Id { get; private set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; private set; }
    
    [Required]
    [StringLength(100)]
    public string Gender { get; private set; }
    
    [Required]
    public DateTime? BirthDate { get; private set; }

    [Required]
    [StringLength(100)]
    public string? Breed { get; private set; }

    [Required]
    [StringLength(100)]
    public string? Location { get; private set; }
    
    [Required]
    [StringLength(300)]
    public string? BovineImg { get; private set; }

    // Default constructor for EF Core
    private Bovine() { }

    // Constructor with parameters
    public Bovine(CreateBovineCommand command)
    {
        if (command.Gender != "male" &&
            command.Gender != "Male" &&
            command.Gender != "female" &&
            command.Gender != "Female")
        {
            throw new ArgumentException("Gender must be either 'male' or 'female'");
        }

        Name = command.Name;
        Gender = command.Gender;
        BirthDate = command.BirthDate;
        Breed = command.Breed;
        Location = command.Location;
        BovineImg = command.BovineImg;
    }
}