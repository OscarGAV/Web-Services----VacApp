using VacApp_Bovinova_Platform.IAM.Application.OutBoundServices;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands.UserCommands;
using VacApp_Bovinova_Platform.IAM.Domain.Repositories;
using VacApp_Bovinova_Platform.IAM.Domain.Services;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.IAM.Application.CommandServices
{
    public class UserCommandService(
        IUserRepostory userRepository,
        IUnitOfWork unitOfWork,
        IHashingService hashingService,
        ITokenService tokenService
    ) : IUserCommandService
    {
        /*
         * Implement Sign Up Command
         */
        public async Task<string> Handle(SignUpCommand command)
        {
            try
            {
                var hashedCommand = command with { Password = hashingService.GenerateHash(command.Password) };
                var user = new User(hashedCommand);

                var existingUser = await userRepository.FindByEmailAsync(user.Email);

                if (existingUser != null)
                    throw new InvalidOperationException("User already exists");

                await userRepository.AddAsync(user);
                await unitOfWork.CompleteAsync();

                return tokenService.GenerateToken(user);
            }
            catch (ArgumentException ex) when (ex.ParamName?.Contains("Email") == true)
            {
                throw new ArgumentException("The provided email format is invalid. Please enter a valid email address.", "Email");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /*
         * Implement Sign In Command
         */
        public async Task<string> Handle(SignInCommand command)
        {
            try
            {
                User? user = null;

                if (!string.IsNullOrEmpty(command.Email))
                {
                    user = await userRepository.FindByEmailAsync(command.Email);
                }
                else if (!string.IsNullOrEmpty(command.UserName))
                {
                    user = await userRepository.FindByNameAsync(command.UserName);
                }

                if (user == null || !hashingService.VerifyHash(command.Password, user.Password))
                    throw new UnauthorizedAccessException("Invalid username or password");

                return tokenService.GenerateToken(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        
        /*
         * Method required for implement UpdateUserCommand
         */
        public async Task UpdateUserAsync(User user)
        {
            try
            {
                await userRepository.UpdateAsync(user);
                await unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating user: {e.Message}");
                throw;
            }
        }
        
        /*
         * Implement UpdateUserCommand
         */
        public async Task<bool> Handle(UpdateUserCommand command, int userId)
        {
            try
            {
                var user = await userRepository.FindByIdAsync(userId);
                if (user == null)
                    return false;
                
                if (!string.Equals(user.Email, command.Email, StringComparison.OrdinalIgnoreCase))
                {
                    var existingUserWithEmail = await userRepository.FindByEmailAsync(command.Email);
                    if (existingUserWithEmail != null)
                        throw new InvalidOperationException("Email already exists");
                }

                if (!string.Equals(user.Username, command.Username, StringComparison.OrdinalIgnoreCase))
                {
                    var existingUserWithUsername = await userRepository.FindByNameAsync(command.Username);
                    if (existingUserWithUsername != null)
                        throw new InvalidOperationException("Username already exists");
                }

                // Update user
                user.Update(command);
                
                await userRepository.UpdateAsync(user);
                await unitOfWork.CompleteAsync();
                
                return true;
            }
            catch (ArgumentException ex) when (ex.ParamName?.Contains("Email") == true)
            {
                throw new ArgumentException("The provided email format is invalid. Please enter a valid email address.", "Email");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating user: {e.Message}");
                throw;
            }
        }
        
        /*
         * Implement DeleteUserCommand
         */
        public async Task<bool> Handle(DeleteUserCommand command)
        {
            try
            {
                var user = await userRepository.FindByIdAsync(command.UserId);
                if (user == null)
                    return false;

                await userRepository.DeleteAsync(user);
                await unitOfWork.CompleteAsync();
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error deleting user: {e.Message}");
                throw;
            }
        }
        
        
    }
}