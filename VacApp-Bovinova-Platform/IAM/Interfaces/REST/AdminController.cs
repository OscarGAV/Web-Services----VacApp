using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Queries;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Queries.AdminQueries;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Queries.UserQueries;
using VacApp_Bovinova_Platform.IAM.Domain.Services;
using VacApp_Bovinova_Platform.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.AdminResources;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Transform;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Transform.TransformFromAdminResources;

namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Tags("Admin Users")]
    public class AdminController(
        IAdminCommandService commandService,
        IAdminQueryService queryService,
        IUserQueryService userQueryService) : ControllerBase
    {
        [HttpPost("create")]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status201Created, "Admin created successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminResource resource)
        {
            try
            {
                var command = CreateAdminCommandFromResourceAssembler.ToCommandFromResource(resource);
                var result = await commandService.Handle(command);

                var adminResource = AdminResourceFromEntityAssembler.ToResourceFromEntity(result, resource.Email);
                return CreatedAtAction(nameof(CreateAdmin), adminResource);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sign-in")]
        [SwaggerResponse(StatusCodes.Status200OK, "Admin signed in successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid credentials")]
        public async Task<ActionResult> SignIn([FromBody] AdminSignInResource resource)
        {
            try
            {
                var command = AdminSignInCommandFromResourceAssembler.ToCommandFromResource(resource);
                var result = await commandService.Handle(command);

                var adminResource = AdminResourceFromEntityAssembler.ToResourceFromEntity(result, resource.Email);
                return Ok(adminResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("profile")]
        [AdminOnly]
        [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse(StatusCodes.Status200OK, "Admin profile retrieved")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Admin not found")]
        public async Task<ActionResult> GetProfile()
        {
            var adminIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (string.IsNullOrEmpty(adminIdClaim) || !int.TryParse(adminIdClaim, out var adminId))
                return Unauthorized("Invalid or missing admin ID");

            var admin = await queryService.Handle(new GetAdminByIdQuery(adminId));
            if (admin is null)
                return NotFound("Admin not found");

            return Ok(new { Email = admin.Email, EmailConfirmed = admin.EmailConfirmed });
        }

        [HttpPut("update-profile")]
        [AdminOnly]
        [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse(StatusCodes.Status200OK, "Admin updated successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateAdminResource resource)
        {
            var adminIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (string.IsNullOrEmpty(adminIdClaim) || !int.TryParse(adminIdClaim, out var adminId))
                return Unauthorized("Invalid or missing admin ID");

            try
            {
                var command = UpdateAdminCommandFromResourceAssembler.ToCommandFromResource(resource);
                var result = await commandService.Handle(command, adminId);

                if (!result)
                    return NotFound("Admin not found");

                return Ok(new { message = "Admin updated successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        [AdminOnly]
        [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse(StatusCodes.Status200OK, "Admin deleted successfully")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        public async Task<IActionResult> DeleteAdmin()
        {
            var adminIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (string.IsNullOrEmpty(adminIdClaim) || !int.TryParse(adminIdClaim, out var adminId))
                return Unauthorized("Invalid or missing admin ID");

            try
            {
                var result = await commandService.DeleteAdminAsync(adminId);
                if (!result)
                    return NotFound("Admin not found");

                return Ok(new { message = "Admin deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        [AdminOnly]
        [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse(StatusCodes.Status200OK, "All admins retrieved")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        public async Task<ActionResult> GetAllAdmins()
        {
            var admins = await queryService.Handle(new GetAllAdminsQuery());
            var adminResources = admins.Select(a => new { Email = a.Email, Id = a.Id });
            return Ok(adminResources);
        }
        
        [HttpGet("all-users")]
        [AdminOnly]
        [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse(StatusCodes.Status200OK, "All users retrieved")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await userQueryService.Handle(new GetAllUsersQuery());

            var userResources = users.Select(u => new
            {
                u.Id,
                u.Username,
                u.Email,
                u.EmailConfirmed
            });

            return Ok(userResources);
        }


    }
}
