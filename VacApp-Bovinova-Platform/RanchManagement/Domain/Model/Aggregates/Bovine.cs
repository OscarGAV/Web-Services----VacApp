using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;

public class Bovine
{
    /// <summary>
    /// Entity Identifier
    /// </summary>
    [Required]
    public int Id { get; private set; }

    /// <summary>
    /// Name of the Bovine
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; private set; }

    /// <summary>
    /// Gender of the Bovine (Male or Female)
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Gender { get; private set; }

    /// <summary>
    /// Date of Birth of the Bovine
    /// </summary>
    [Required]
    public DateTime? BirthDate { get; private set; }

    /// <summary>
    /// Breed of the Bovine
    /// </summary>
    [Required]
    [StringLength(100)]
    public string? Breed { get; private set; }

    /// <summary>
    /// Actual Location of the Bovine
    /// </summary>
    [Required]
    [StringLength(100)]
    public string? Location { get; private set; }

    /// <summary>
    /// Stable Identifier As Foreign Key
    /// </summary>
    [Required]
    public int? StableId { get; private set; }
    [ForeignKey(nameof(StableId))]
    public Stable? Stable { get; private set; }

    /// <summary>
    /// Bovine Image
    /// </summary>
    [Required]
    [StringLength(300)]
    public string? BovineImg { get; private set; }
    
    
    /// <summary>
    /// User Identifier As Foreign Key
    /// </summary>
    public RanchUserId? RanchUserId { get; set; }
    
    
    // Default constructor for EF Core
    private Bovine()
    {
        Name = "";
        Gender = "Male";
    }
    
    public Bovine(
        string name, 
        string gender, 
        DateTime? birthDate,
        string? breed, 
        string? location, 
        string? bovineImg, 
        int? stableId,
        RanchUserId? ranchUserId
        )
    {
        Name = name;
        Gender = gender;
        BirthDate = birthDate;
        Breed = breed;
        Location = location;
        BovineImg = bovineImg;
        StableId = stableId;
        RanchUserId = ranchUserId;
    }

    // Constructor with parameters
    public Bovine(CreateBovineCommand command)
    {
        if (!command.Gender.ToLower().Equals("male") && !command.Gender.ToLower().Equals("female"))
            throw new ArgumentException("Gender must be either 'male' or 'female'");
        
        Name = command.Name;
        Gender = command.Gender;
        BirthDate = command.BirthDate;
        Breed = command.Breed;
        Location = command.Location;
        BovineImg = command.BovineImg;
        StableId = command.StableId;
        RanchUserId = command.RanchUserId ?? throw new ArgumentException("UserId must be set by the system");
    }

    //Update Bovine
    public void Update(UpdateBovineCommand command)
    {
        if (!command.Gender.ToLower().Equals("male") && !command.Gender.ToLower().Equals("female"))
            throw new ArgumentException("Gender must be either 'male' or 'female'");

        Name = command.Name;
        Gender = command.Gender;
        BirthDate = command.BirthDate;
        Breed = command.Breed;
        Location = command.Location;
        StableId = command.StableId;
    }
}