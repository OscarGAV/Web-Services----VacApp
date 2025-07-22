using System.ComponentModel.DataAnnotations;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;

public class Stable
{
    /// <summary>
    /// Identifier for the Stable entity
    /// </summary>
    [Required]
    public int Id { get; private set; }
    
    /// <summary>
    /// Name of the Stable
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Name { get; private set; }
    
    /// <summary>
    /// Max. Capacity of the Stable
    /// </summary>
    [Required]
    public int Limit { get; private set; }
    
    /// <summary>
    /// User Identifier As Foreign Key
    /// </summary>
    public RanchUserId? RanchUserId { get; set; }
    
    // Default constructor for EF Core
    private Stable()
    {
        Name = "Stable A";
    }
    
    // Constructor with parameters
    public Stable(CreateStableCommand command)
    {
        if (command.Limit <= 0)
        {
            throw new ArgumentException("Limit must be greater than 0");
        }
        
        if (string.IsNullOrEmpty(command.Name))
            throw new ArgumentException("Name must not be empty");
        
        Limit = command.Limit;
        Name = command.Name;
        RanchUserId = command.RanchUserId ?? throw new ArgumentException("RanchUserId must be set by the system");
    }
    
    //Update
    public void Update(UpdateStableCommand command)
    {
        if (command.Limit <= 0)
        {
            throw new ArgumentException("Limit must be greater than 0");
        }
        
        Limit = command.Limit;
        Name = command.Name;
    }
}