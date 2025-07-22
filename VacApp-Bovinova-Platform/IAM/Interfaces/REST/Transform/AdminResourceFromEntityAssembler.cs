using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.AdminResources;

namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST.Transform;

public static class AdminResourceFromEntityAssembler
{
    public static AdminResource ToResourceFromEntity(string token, string email)
    {
        return new AdminResource(token, email);
    }
}