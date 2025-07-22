using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace VacApp_Bovinova_Platform.IAM.Infrastructure.Pipeline.Middleware.Attributes
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userTypeClaim = context.HttpContext.User.FindFirst("user_type")?.Value;
            
            // Verify if user is admin
            if (string.IsNullOrEmpty(userTypeClaim) || userTypeClaim != "Admin")
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Access denied. Admin privileges required." });
                return;
            }
            
            base.OnActionExecuting(context);
        }
    }
}