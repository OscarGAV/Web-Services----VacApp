using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.UserResources;

namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST.Transform.TransformFromUserResources
{
    public static class UserResourceFromEntityAssembler
    {
        public static UserResource ToResourceFromEntity(string token, string? userName, string? email)
        {
            return new UserResource(token, userName, email);
        }
    }
}