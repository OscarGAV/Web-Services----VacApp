using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Queries;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Queries.AdminQueries;
using VacApp_Bovinova_Platform.IAM.Domain.Repositories;
using VacApp_Bovinova_Platform.IAM.Domain.Services;

namespace VacApp_Bovinova_Platform.IAM.Application.QueryServices;

public class AdminQueryService(IAdminRepository adminRepository) : IAdminQueryService
{
    public async Task<Admin?> Handle(GetAdminByIdQuery query)
    {
        return await adminRepository.FindByIdAsync(query.Id);
    }

    public async Task<Admin?> Handle(GetAdminByEmailQuery query)
    {
        return await adminRepository.FindByEmailAsync(query.Email);
    }

    public async Task<IEnumerable<Admin>> Handle(GetAllAdminsQuery query)
    {
        return await adminRepository.FindAllAsync();
    }
}