using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Commands;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Queries;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Services;
using VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST.Resources;
using VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST.Transform;
using VacApp_Bovinova_Platform.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST;

/// <summary>
/// API controller for managing staffs
/// </summary>
[Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("/api/v1/staff")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Staffs")]
public class StaffController(IStaffCommandService commandService,
    IStaffQueryService queryService) : ControllerBase
{
    /// <summary>
    /// Posts a new staff to the system.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateStaffs([FromBody] CreateStaffResource resource)
    {
        // Extrae el userId desde el claim 'sid' del JWT
        var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized("Usuario no autenticado.");
        
        var command = CreateStaffCommandFromResourceAssembler.ToCommandFromResource(resource, userId);
        var result = await commandService.Handle(command);
        if (result is null) return BadRequest();

        return CreatedAtAction(nameof(GetStaffById), new { id = result.Id },
            StaffResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    /// Gets all staffs in the system.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all staffs",
        Description = "Get all staffs",
        OperationId = "GetAllStaff")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of staffs were found", typeof(IEnumerable<StaffResource>))]
    public async Task<IActionResult> GetAllStaff()
    {
        // Recuperar el userId desde el claim 'sid'
        var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized("Usuario no autenticado.");
        
        var staffs = await queryService.Handle(new GetAllStaffQuery(userId));
        var staffResources = staffs.Select(StaffResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(staffResources);
    }

    /// <summary>
    /// Gets a staff by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetStaffById(int id)
    {
        var getStaffById = new GetStaffByIdQuery(id);
        var result = await queryService.Handle(getStaffById);
        if (result is null) return NotFound();
        var resources = StaffResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resources);
    }

    /// <summary>
    /// Gets all staffs by campaign ID.
    /// </summary>
    /// <param name="campaignId"></param>
    /// <returns></returns>
    [HttpGet("search-by-campaign/{campaignId}")]
    [SwaggerOperation(
        Summary = "Get all staffs by campaign ID",
        Description = "Get all staffs by campaign ID",
        OperationId = "GetStaffsByCampaignId")]
    public async Task<ActionResult> GetStaffsByCampaignId(int campaignId)
    {
        var getStaffByCampaignIdQuery = new GetStaffByCampaignIdQuery(campaignId);
        var staffs = await queryService.Handle(getStaffByCampaignIdQuery);

        if (staffs == null || !staffs.Any())
            return NotFound();

        var staffResources = staffs.Select(StaffResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(staffResources);
    }

    /// <summary>
    /// Gets all staffs by employee status.
    /// </summary>
    /// <param name="employeeStatus"></param>
    /// <returns></returns>
    [HttpGet("search-by-employee-status/{employeeStatus}")]
    [SwaggerOperation(
        Summary = "Get all staffs by employee status",
        Description = "Get all staffs by employee status",
        OperationId = "GetStaffByEmployeeStatus")]
    public async Task<ActionResult> GetStaffByEmployeeStatus(int employeeStatus)
    {
        var getStaffByEmployeeStatusQuery = new GetStaffByEmployeeStatusQuery(employeeStatus);
        var staffs = await queryService.Handle(getStaffByEmployeeStatusQuery);

        if (staffs == null || !staffs.Any())
            return NotFound();

        var staffResources = staffs.Select(StaffResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(staffResources);
    }

    /// <summary>
    /// Gets a staff by its name.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpGet("search-by-name/{name}")]
    [SwaggerOperation(
        Summary = "Get Staff by name",
        Description = "Get Staff Member by his name",
        OperationId = "GetStaffByName")]
    public async Task<ActionResult> GetStaffByName(string name)
    {
        var getStaffByNameQuery = new GetStaffByNameQuery(name);
        var result = await queryService.Handle(getStaffByNameQuery);
        if (result is null) return NotFound();
        var resources = StaffResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resources);
    }

    /// <summary>
    /// Updates a staff by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStaff(int id, [FromBody] UpdateStaffResource resource)
    {
        var command = UpdateStaffCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await commandService.Handle(command);
        if (result is null) return BadRequest();

        return Ok(StaffResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    /// Deletes a staff by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteStaff(int id)
    {
        var command = new DeleteStaffCommand(id);
        var result = await commandService.Handle(command);
        
        if (result is null)
            return NotFound(new { message = "Staff not found" });

        return Ok(new { message = "Deleted successfully" });
    }
}