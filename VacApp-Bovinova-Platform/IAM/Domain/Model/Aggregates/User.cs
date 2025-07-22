using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands.UserCommands;

namespace VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates
{
    public class User : IEntityWithCreatedUpdatedDate
    {
        [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
        [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }

        [Required]
        public int Id { get; private set; }

        [Required]
        [StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters long.")]
        public string Username { get; private set; }

        [Required]
        [StringLength(100, ErrorMessage = "Password must be between 6 and 100 characters long.")]
        public string Password { get; private set; }

        [Required]
        [EmailAddress]
        [MaxLength(128, ErrorMessage = "Email cannot exceed 128 characters.")]
        public string Email { get; private set; }
        
        [Required]
        public bool EmailConfirmed { get; init; }
        
        public User()
        {
            Username = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
            EmailConfirmed = false;
        }

        public User(SignUpCommand command)
        {
            Username = command.Username;
            Password = command.Password;
            Email = command.Email;
            if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Invalid email format.", nameof(command.Email));
            }
        }

        public void Update(UpdateUserCommand command)
        {
            Username = command.Username;
            Email = command.Email;
            if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Invalid email format.", nameof(command.Email));
            }
        }
    }
}