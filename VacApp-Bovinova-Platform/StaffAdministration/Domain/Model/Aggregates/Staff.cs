using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Commands;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;

public class Staff
{
    [Required]
    public int Id { get; private set; }

    [Required]
    [StringLength(100)]
    public string Name { get; private set; }
    
    [Required]
    public EmployeeStatus EmployeeStatus { get; private set; }
    
    public int? CampaignId { get; private set; }
    [ForeignKey(nameof(CampaignId))]
    public Campaign? Campaign { get; private set; }
    
    
    /// <summary>
    /// User Identifier As Foreign Key
    /// </summary>
    public StaffUserId? StaffUserId { get; set; }

    public Staff()
    {
        Name = "";
        EmployeeStatus = new EmployeeStatus();
    }
    
    public Staff(string name, int employeeStatus, int? campaignId, StaffUserId? staffUserId)
    {
        Name = name;
        EmployeeStatus = new EmployeeStatus(employeeStatus);
        CampaignId = campaignId;
        StaffUserId = staffUserId;
    }

    // Constructor for creating a new Staff
    public Staff(CreateStaffCommand command)
    {
        Name = command.Name;
        EmployeeStatus = new EmployeeStatus(command.EmployeeStatus);
        CampaignId = command.CampaignId ?? throw new ArgumentException("CampaignId is required");
        StaffUserId = command.StaffUserId ?? throw new ArgumentException("StaffUserId must be set by the system");

    }

    // Update method for modifying an existing Staff
    public void Update(UpdateStaffCommand command)
    {
        Name = command.Name;
        EmployeeStatus = new EmployeeStatus(command.EmployeeStatus);
        CampaignId = command.CampaignId;
    }
}