using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

public record CreateStableCommand(
    string Name,
    int Limit,
    RanchUserId? RanchUserId);