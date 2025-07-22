using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands;

namespace VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates
{
    public class Admin : IEntityWithCreatedUpdatedDate
    {
        [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
        [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }

        [Required]
        public int Id { get; private set; }

        [Required]
        [EmailAddress]
        public string Email { get; private set; }

        [Required]
        public bool EmailConfirmed { get; set; } = true;

        private Admin(string email)
        {
            this.Email = email;
        }

        public Admin(CreateAdminCommand command)
        {
            Email = command.Email;
            ValidateAdminEmail(Email);
        }
        
        public void Update(UpdateAdminCommand command)
        {
            Email = command.Email;
            ValidateAdminEmail(Email);
        }

        private void ValidateAdminEmail(string email)
        {
            if (!email.EndsWith("@vacapp.com", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Admin email must end with @vacapp.com", nameof(email));
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Invalid email format.", nameof(email));
            }
        }
        
        public bool ValidateLogin(string password)
        {
            // Admins can use any password
            return true;
        }
    }
}