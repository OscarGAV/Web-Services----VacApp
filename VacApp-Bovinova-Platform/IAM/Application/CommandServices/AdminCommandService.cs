using VacApp_Bovinova_Platform.IAM.Application.OutBoundServices;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands;
using VacApp_Bovinova_Platform.IAM.Domain.Repositories;
using VacApp_Bovinova_Platform.IAM.Domain.Services;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.IAM.Application.CommandServices;

public class AdminCommandService(
    IAdminRepository adminRepository,
    IUnitOfWork unitOfWork,
    ITokenService tokenService
) : IAdminCommandService
{
    public async Task<string> Handle(CreateAdminCommand command)
    {
        var admin = new Admin(command);

        var existingAdmin = await adminRepository.FindByEmailAsync(admin.Email);
        if (existingAdmin != null)
            throw new Exception("Admin already exists with this email");

        try
        {
            await adminRepository.AddAsync(admin);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating admin: {e.Message}");
            throw;
        }

        return tokenService.GenerateToken(admin);
    }

    public async Task<string> Handle(AdminSignInCommand command)
    {
        var admin = await adminRepository.FindByEmailAsync(command.Email);

        if (admin == null)
            throw new Exception("Admin not found");
        
        // Validate "**@vacapp.com"
        if (!command.Email.EndsWith("@vacapp.com", StringComparison.OrdinalIgnoreCase))
            throw new Exception("Invalid admin email domain");

        // Admin can use any password
        if (!admin.ValidateLogin(command.Password))
            throw new Exception("Invalid login");

        return tokenService.GenerateToken(admin);
    }

    public async Task<bool> Handle(UpdateAdminCommand command, int adminId)
    {
        try
        {
            var admin = await adminRepository.FindByIdAsync(adminId);
            if (admin == null)
                return false;

            // Verifies if email already exists
            if (!string.Equals(admin.Email, command.Email, StringComparison.OrdinalIgnoreCase))
            {
                var existingAdmin = await adminRepository.FindByEmailAsync(command.Email);
                if (existingAdmin != null)
                    throw new Exception("Email already exists");
            }

            admin.Update(command);
            await adminRepository.UpdateAsync(admin);
            await unitOfWork.CompleteAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating admin: {e.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteAdminAsync(int adminId)
    {
        try
        {
            var admin = await adminRepository.FindByIdAsync(adminId);
            if (admin == null)
                return false;

            await adminRepository.DeleteAsync(admin);
            await unitOfWork.CompleteAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting admin: {e.Message}");
            throw;
        }
    }
}