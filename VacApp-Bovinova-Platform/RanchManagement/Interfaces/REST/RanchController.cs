using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Authorization;

namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST;

/// <summary>
/// API controller for managing bovines
/// </summary>
[ApiController]
[Authorize]
[Route("/api/v1/bovines")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Bovines")]
public class BovineController(IBovineCommandService commandService,
    IBovineQueryService queryService) : ControllerBase
{
    /// <summary>
    /// Posts a new bovine to the system.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new bovine",
        Description = "Creates a new bovine associated with the authenticated user.",
        OperationId = "CreateBovines"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "Bovine successfully created", typeof(BovineResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateBovines([FromForm] CreateBovineResource resource)
    {
        // Extrae el userId desde el claim 'sid' del JWT
        var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized("Usuario no autenticado.");

        // Usa el ID extraído para crear el comando
        var command = CreateBovineCommandFromResourceAssembler.ToCommandFromResource(resource, userId);
        var result = await commandService.Handle(command);

        if (result is null) return BadRequest();

        return CreatedAtAction(nameof(GetBovineById), new { id = result.Id },
            BovineResourceFromEntityAssembler.ToResourceFromEntity(result));
    }


    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all bovines",
        Description = "Get all bovines",
        OperationId = "GetAllBovine")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of bovines were found", typeof(IEnumerable<BovineResource>))]
    public async Task<IActionResult> GetAllBovine()
    {
        // Recuperar el userId desde el claim 'sid'
        var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized("Usuario no autenticado.");

        // Usar userId para consultar
        var bovines = await queryService.Handle(new GetAllBovinesQuery(userId));
        var bovineResources = bovines.Select(BovineResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(bovineResources);
    }


    /// <summary>
    /// Gets a bovine by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetBovineById(int id)
    {
        var getBovineById = new GetBovinesByIdQuery(id);
        var result = await queryService.Handle(getBovineById);
        if (result is null) return NotFound();
        var resources = BovineResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resources);
    }

    /// <summary>
    /// Gets all bovines by stable ID.
    /// </summary>
    /// <param name="stableId"></param>
    /// <returns></returns>
    [HttpGet("stable/{stableId}")]
    [SwaggerOperation(
        Summary = "Get all bovines by stable ID",
        Description = "Get all bovines by stable ID",
        OperationId = "GetBovinesByStableId")]
    public async Task<ActionResult> GetBovinesByStableId(int? stableId)
    {
        var getBovinesByStableIdQuery = new GetBovinesByStableIdQuery(stableId);
        var bovines = await queryService.Handle(getBovinesByStableIdQuery);

        if (bovines == null || !bovines.Any())
            return NotFound();

        var bovineResources = bovines.Select(BovineResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(bovineResources);
    }

    /// <summary>
    /// Updates a bovine by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateBovine(int id, [FromForm] UpdateBovineResource resource)
    {
        var command = UpdateBovineCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await commandService.Handle(command);
        if (result is null) return BadRequest();

        return Ok(BovineResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    /// Deletes a bovine by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBovine(int id)
    {
        var command = new DeleteBovineCommand(id);
        var result = await commandService.Handle(command);
        if (result is null)
            return NotFound(new { message = "Bovine not found" });

        return Ok(new { message = "Deleted successfully" });
    }
}

/// <summary>
/// API controller for managing vaccines
/// </summary>
[Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("/api/v1/vaccines")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Vaccines")]
public class VaccineController(
   IVaccineCommandService commandService,
   IVaccineQueryService queryService) : ControllerBase
{
    /// <summary>
    /// Posts a new vaccine to the system.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new vaccine",
        Description = "Creates a new vaccine associated with the authenticated user.",
        OperationId = "CreateVaccines"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "Vaccine successfully created", typeof(VaccineResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateVaccines([FromForm] CreateVaccineResource resource)
    {
        // Extrae el userId desde el claim 'sid' del JWT
        var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized("Usuario no autenticado.");
        
        var command = CreateVaccineCommandFromResourceAssembler.ToCommandFromResource(resource, userId);
        var result = await commandService.Handle(command);
        if (result is null) return BadRequest();

        return CreatedAtAction(nameof(GetVaccineById), new { id = result.Id },
            VaccineResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    /// Gets all vaccines in the system.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all vaccines",
        Description = "Get all vaccines",
        OperationId = "GetAllVaccine")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of vaccines were found", typeof(IEnumerable<VaccineResource>))]
    public async Task<IActionResult> GetAllVaccine()
    {
        // Recuperar el userId desde el claim 'sid'
        var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized("Usuario no autenticado.");
        
        var vaccines = await queryService.Handle(new GetAllVaccinesQuery(userId));
        var vaccineResources = vaccines.Select(VaccineResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(vaccineResources);
    }

    /// <summary>
    /// Gets a vaccine by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetVaccineById(int id)
    {
        var getVaccineById = new GetVaccinesByIdQuery(id);
        var result = await queryService.Handle(getVaccineById);
        if (result is null) return NotFound();
        var resources = VaccineResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resources);
    }

    /// <summary>
    /// Gets all vaccines by bovine ID.
    /// </summary>
    /// <param name="bovineId"></param>
    /// <returns></returns>
    [HttpGet("bovine/{bovineId}")]
    [SwaggerOperation(
        Summary = "Get all vaccines by bovine ID",
        Description = "Get all vaccines by bovine ID",
        OperationId = "GetVaccinesByBovineId")]
    public async Task<ActionResult> GetVaccinesByBovineId(int? bovineId)
    {
        var getVaccinesByBovineIdQuery = new GetVaccinesByBovineIdQuery(bovineId);
        var vaccines = await queryService.Handle(getVaccinesByBovineIdQuery);

        if (vaccines == null || !vaccines.Any())
            return NotFound();

        var vaccineResources = vaccines.Select(VaccineResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(vaccineResources);
    }

    /// <summary>
    /// Updates a vaccine by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateVaccine(int id, [FromForm] UpdateVaccineResource resource)
    {
        var command = UpdateVaccineCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await commandService.Handle(command);
        if (result is null) return BadRequest();

        return Ok(VaccineResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    /// Deletes a vaccine by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteVaccine(int id)
    {
        var command = new DeleteVaccineCommand(id);
        var result = await commandService.Handle(command);
        if (result is null)
            return NotFound(new { message = "Vaccine not found" });

        return Ok(new { message = "Deleted successfully" });
    }

}

/// <summary>
/// API controller for managing stables
/// </summary>
[Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("/api/v1/stables")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Stables")]
public class StableController(
   IStableCommandService commandService,
   IStableQueryService queryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new stable",
        Description = "Creates a new stable associated with the authenticated user.",
        OperationId = "CreateStables"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "Stable successfully created", typeof(StableResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
    public async Task<IActionResult> CreateStables([FromBody] CreateStableResource resource)
    {
        // Extrae el userId desde el claim 'sid' del JWT
        var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized("Usuario no autenticado.");
        
        var command = CreateStableCommandFromResourceAssembler.ToCommandFromResource(resource, userId);
        var result = await commandService.Handle(command);
        if (result is null) return BadRequest();

        return CreatedAtAction(nameof(GetStableById), new { id = result.Id },
            StableResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all stables",
        Description = "Get all stables",
        OperationId = "GetAllStable")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of stables were found", typeof(IEnumerable<StableResource>))]
    public async Task<IActionResult> GetAllStable()
    {
        // Recuperar el userId desde el claim 'sid'
        var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized("Usuario no autenticado.");
        
        var stables = await queryService.Handle(new GetAllStablesQuery(userId));
        var stableResources = stables.Select(StableResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(stableResources);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetStableById(int id)
    {
        var getStableById = new GetStablesByIdQuery(id);
        var result = await queryService.Handle(getStableById);
        if (result is null) return NotFound();
        var resources = StableResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resources);
    }

    /// <summary>
    /// Updates a stable by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStable(int id, [FromBody] UpdateStableResource resource)
    {
        var command = UpdateStableCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await commandService.Handle(command);
        if (result is null) return BadRequest();

        return Ok(StableResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    /// <summary>
    /// Deletes a stable by its ID.
    /// </summary>
    /// <param name="id">Stable ID</param>
    /// <returns>Success message or NotFound</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteStable(int id)
    {
        var command = new DeleteStableCommand(id);
        var result = await commandService.Handle(command);

        if (result is null)
            return NotFound(new { message = "Stable not found" });

        return Ok(new { message = "Deleted successfully" });
    }
}