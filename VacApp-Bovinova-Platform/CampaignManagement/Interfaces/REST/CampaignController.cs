using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Services;
using VacApp_Bovinova_Platform.CampaignManagement.Interfaces.REST.Resources;
using VacApp_Bovinova_Platform.CampaignManagement.Interfaces.REST.Transform;
using VacApp_Bovinova_Platform.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace VacApp_Bovinova_Platform.CampaignManagement.Interfaces.REST;

[Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class CampaignController(ICampaignCommandService campaignCommandService, ICampaignQueryService campaignQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateCampaign([FromBody] CreateCampaignResource resource)
    {
        // Extrae el userId desde el claim 'sid' del JWT
        var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized("Usuario no autenticado.");
        
        var createCampaignCommand = CreateCampaignCommandFromResourceAssembler.ToCommandFromResource(resource, userId);
        var result = await campaignCommandService.Handle(createCampaignCommand);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetCampaignById), new { id = result.Id },
            CampaignResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCampaignById(int id)
    {
        var getCampaignByIdQuery = new GetCampaignByIdQuery(id);
        var result = await campaignQueryService.Handle(getCampaignByIdQuery);
        if (result is null) return NotFound();
        var resource = CampaignResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpGet("all-campaigns")]
    public async Task<ActionResult> GetAllCampaigns()
    {
        // Recuperar el userId desde el claim 'sid'
        var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized("Usuario no autenticado.");
        
        var campaigns = await campaignQueryService.Handle(new GetAllCampaignsQuery(userId));
        var campaignResources = campaigns.Select(CampaignResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(campaignResources);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteCampaign([FromRoute] int id)
    {
        var campaigns = await campaignCommandService.Handle(new DeleteCampaignCommand(id));

        if (!campaigns.Any())
            return NotFound(new { message = "Campaign not found" });

        return Ok(new { message = "Deleted successfully" });
    }


    [HttpPatch("{id}/update-status")]
    public async Task<ActionResult> UpdateCampaignStatus([FromRoute] int id, [FromBody] UpdateCampaignStatusResource resource)
    {
        var updateCampaignStatusCommand = UpdateCampaignStatusFromResourceAssembler.ToCommandFromResource(resource, id);
        var result = await campaignCommandService.Handle(updateCampaignStatusCommand);
        if (result is null) return BadRequest();
        var resourceFromEntity = CampaignResourceFromEntityAssembler.ToResourceFromEntity(result);
        return CreatedAtAction(nameof(GetCampaignById), new { id = result.Id }, resourceFromEntity);
    }

    [HttpPatch("{id}/add-goal")]
    public async Task<ActionResult> AddGoalToCampaign([FromRoute] int id, [FromBody] AddGoalToCampaignResource resource)
    {
        var addGoalToCampaignCommand = AddGoalToCampaignFromResourceAssembler.ToCommandFromResource(resource, id);
        var result = await campaignCommandService.Handle(addGoalToCampaignCommand);
        if (result is null) return BadRequest();
        var resourceFromEntity = CampaignResourceFromEntityAssembler.ToResourceFromEntity(result);
        return CreatedAtAction(nameof(GetCampaignById), new { id = result.Id }, resourceFromEntity);
    }

    [HttpPatch("{id}/add-channel")]
    public async Task<ActionResult> AddChannelToCampaign([FromRoute] int id, [FromBody] AddChannelToCampaignResource resource)
    {
        var addChannelToCampaignCommand = AddChannelToCampaignFromResourceAssembler.ToCommandFromResource(resource, id);
        var result = await campaignCommandService.Handle(addChannelToCampaignCommand);
        if (result is null) return BadRequest();
        var resourceFromEntity = CampaignResourceFromEntityAssembler.ToResourceFromEntity(result);
        return CreatedAtAction(nameof(GetCampaignById), new { id = result.Id }, resourceFromEntity);
    }

    [HttpGet("{id}/goals")]
    public async Task<ActionResult> GetGoalsFromCampaign([FromRoute] int id)
    {
        var getGoalsFromCampaignIdQuery = new GetGoalsFromCampaignIdQuery(id);
        var result = await campaignQueryService.Handle(getGoalsFromCampaignIdQuery);
        if (result is null) return NotFound();
        var resources = result.Select(GoalResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id}/channels")]
    public async Task<ActionResult> GetChannelsFromCampaign([FromRoute] int id)
    {
        var getChannelsFromCampaignIdQuery = new GetChannelsFromCampaignIdQuery(id);
        var result = await campaignQueryService.Handle(getChannelsFromCampaignIdQuery);
        if (result is null) return NotFound();
        var resources = result.Select(ChannelResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}