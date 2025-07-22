using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Services;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Queries;
using VacApp_Bovinova_Platform.IAM.Domain.Services;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.UserResources;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Transform;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Queries.UserQueries;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Transform.TransformFromUserResources;

namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Tags("Users")]
    public class UserController(
        IUserCommandService commandService,
        IUserQueryService queryService,
        IBovineQueryService bovineQueryService,
        ICampaignQueryService campaignQueryService,
        IVaccineQueryService vaccineQueryService,
        IStableQueryService stableQueryService
        ) : ControllerBase
    {
        /*
         * 
         */
        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
        {
            var command = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await commandService.Handle(command);

            if (result is null) return BadRequest("User already exists");

            var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(result, resource.Username, resource.Email);

            return CreatedAtAction(nameof(SignUp), userResource);
        }

        /*
         * 
         */
        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<ActionResult> SignIn([FromBody] SignInResource resource)
        {
            if (string.IsNullOrEmpty(resource.Email) && string.IsNullOrEmpty(resource.UserName))
            {
                return BadRequest("Either Email or UserName must be provided.");
            }

            var command = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await commandService.Handle(command);

            if (result is null) return BadRequest("Invalid credentials.");

            var userName = !string.IsNullOrEmpty(resource.UserName)
                ? resource.UserName
                : await queryService.GetUserNameByEmail(resource.Email!);

            var email = !string.IsNullOrEmpty(resource.Email)
                ? resource.Email
                : await queryService.GetEmailByUserName(resource.UserName!);

            var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(result, userName, email);

            return Ok(userResource);
        }

        /*
         * 
         */
        [HttpGet("get-info")]
        [SwaggerResponse(StatusCodes.Status200OK, "User info", typeof(UserInfoResource))]
        public async Task<ActionResult> GetInfo()
        {
            // Get user ID from JWT claims
            var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid or missing user ID");
            
            // Use the query handler to get the user by ID
            var user = await queryService.Handle(new GetUserByIdQuery(userId));
            if (user is null)
                return NotFound("User not found");

            // Total Bovines
            var totalBovines = bovineQueryService.Handle(new GetAllBovinesQuery(user.Id)).Result.Count();

            // Total Stables
            var totalStables = stableQueryService.Handle(new GetAllStablesQuery(user.Id)).Result.Count();

            // Total Campaigns
            var totalCampaigns = campaignQueryService.Handle(new GetAllCampaignsQuery(user.Id)).Result.Count();

            // Total Vaccinations
            var totalVaccinations = vaccineQueryService.CountVaccinesByUserIdAsync(new RanchUserId(user.Id)).Result;
            
            // Build and return the response
            var resource = new UserInfoResource(user.Username, totalBovines, totalVaccinations, totalStables);
            return Ok(resource);
        }

                /*
         * 
         */
        [HttpPut("update-profile")]
        [SwaggerResponse(StatusCodes.Status200OK, "User updated successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserResource resource)
        {
            // Validar que los campos requeridos estén presentes
            if (string.IsNullOrWhiteSpace(resource.Username) || string.IsNullOrWhiteSpace(resource.Email))
            {
                return BadRequest("Username and Email are required");
            }

            // Obtener el ID del usuario desde el JWT
            var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid or missing user ID");

            try
            {
                var command = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
                var result = await commandService.Handle(command, userId);
        
                if (!result)
                    return NotFound("User not found");

                return Ok(new { message = "User updated successfully" });
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

        /*
         * 
         */
        [HttpDelete("delete-account")]
        [SwaggerResponse(StatusCodes.Status200OK, "Account deleted successfully")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        public async Task<IActionResult> DeleteAccount()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid or missing user ID");

            try
            {
                var command = new DeleteUserCommand(userId);
                var result = await commandService.Handle(command);
        
                if (!result)
                    return NotFound("User not found");

                return Ok(new { message = "Account deleted successfully" });
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        /*
         * 
         */
        [HttpGet("profile")]
        [SwaggerResponse(StatusCodes.Status200OK, "User profile", typeof(UserProfileResource))]
        public async Task<ActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid or missing user ID");

            var user = await queryService.Handle(new GetUserByIdQuery(userId));
            if (user is null)
                return NotFound("User not found");

            var resource = new UserProfileResource(user.Username, user.Email, user.EmailConfirmed);
            return Ok(resource);
        }

    }
}