namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.UserResources;

public record UserInfoResource(
    string Name,
    int TotalBovines,
    //int TotalCampaigns,
    int TotalVaccinations,
    int TotalStables
    );