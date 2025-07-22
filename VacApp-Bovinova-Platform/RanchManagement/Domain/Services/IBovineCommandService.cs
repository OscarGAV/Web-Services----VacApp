using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

public interface IBovineCommandService
{
    Task<Bovine?> Handle(CreateBovineCommand command);
    
    Task<Bovine?> Handle(UpdateBovineCommand command);
    
    Task<Bovine?> Handle(DeleteBovineCommand command);
}