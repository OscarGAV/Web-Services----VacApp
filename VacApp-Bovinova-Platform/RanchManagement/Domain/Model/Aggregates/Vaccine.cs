using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;

public class Vaccine
{
    /// <summary>
    /// Entity Identifier
    /// </summary>
    [Required]
    public int Id { get; private set; }

    /// <summary>
    /// Name of the Vaccine
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; private set; }

    /// <summary>
    /// Vaccine Type
    /// </summary>
    [Required]
    [StringLength(100)]
    public string? VaccineType { get; private set; }

    /// <summary>
    /// Date of the Vaccination
    /// </summary>
    [Required]
    public DateTime? VaccineDate { get; private set; }

    /// <summary>
    /// Vaccine Image
    /// </summary>
    [Required]
    [StringLength(300)]
    public string? VaccineImg { get; private set; }

    /// <summary>
    /// Bovine Identifier As Foreign Key
    /// </summary>
    [Required]
    public int BovineId { get; private set; }
    /// <summary>
    /// Instancing the Bovine Entity for the Foreign Key
    /// </summary>
    [ForeignKey(nameof(BovineId))]
    public Bovine? Bovine { get; private set; }
    
    /// <summary>
    /// User Identifier As Foreign Key
    /// </summary>
    public RanchUserId? RanchUserId { get; set; }

    // Default constructor for EF Core
    private Vaccine()
    {
        Name = "";
    }

    // Constructor with parameters
    public Vaccine(CreateVaccineCommand command)
    {
        Name = command.Name;
        VaccineType = command.VaccineType;
        VaccineDate = command.VaccineDate;
        VaccineImg = command.VaccineImg;
        BovineId = command.BovineId;
        RanchUserId = command.RanchUserId ?? throw new ArgumentException("RanchUserId must be set by the system");
    }

    //Update
    public void Update(UpdateVaccineCommand command)
    {
        Name = command.Name;
        VaccineType = command.VaccineType;
        VaccineDate = command.VaccineDate;
        BovineId = command.BovineId;
    }
}