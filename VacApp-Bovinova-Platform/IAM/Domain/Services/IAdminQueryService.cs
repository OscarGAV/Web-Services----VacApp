using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Queries;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Queries.AdminQueries;

namespace VacApp_Bovinova_Platform.IAM.Domain.Services;

public interface IAdminQueryService
{
    Task<Admin?> Handle(GetAdminByIdQuery query);
    Task<Admin?> Handle(GetAdminByEmailQuery query);
    Task<IEnumerable<Admin>> Handle(GetAllAdminsQuery query);
}