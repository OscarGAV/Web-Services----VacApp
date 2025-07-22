using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

public interface IStableCommandService
{
    Task<Stable?> Handle(CreateStableCommand command);
    
    //Update
    Task <Stable?> Handle(UpdateStableCommand command);
    
    //Delete
    Task<Stable?> Handle(DeleteStableCommand command);
}