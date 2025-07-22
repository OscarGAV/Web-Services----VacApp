using VacApp_Bovinova_Platform.IAM.Infrastructure.Pipeline.Middleware.Components;

namespace VacApp_Bovinova_Platform.IAM.Infrastructure.Pipeline.Middleware.Extensions
{
    public static class RequestAuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestAuthorizationMiddleware>();
        }
    }
}