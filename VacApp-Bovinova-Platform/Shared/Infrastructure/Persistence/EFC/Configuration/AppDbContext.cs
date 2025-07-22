using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* IAM BC  */
        //User
        builder.Entity<User>().HasKey(f => f.Id);
        builder.Entity<User>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(f => f.Username).IsRequired();
        builder.Entity<User>().Property(f => f.Password).IsRequired();
        builder.Entity<User>().Property(f => f.Email).IsRequired();
        builder.Entity<User>().Property(f => f.EmailConfirmed).IsRequired();
        //Admin User
        builder.Entity<Admin>().HasKey(f => f.Id);
        builder.Entity<Admin>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Admin>().Property(f => f.Email).IsRequired();
        builder.Entity<Admin>().Property(f => f.EmailConfirmed).IsRequired();
        
        
        /* Ranch Management BC -------------------------------------------------------------------------------------- */
        //Bovine

        builder.Entity<Bovine>().HasKey(f => f.Id);
        builder.Entity<Bovine>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Bovine>().Property(f => f.Name).IsRequired();
        builder.Entity<Bovine>().Property(f => f.Gender).IsRequired();
        builder.Entity<Bovine>().Property(f => f.BirthDate).IsRequired();
        builder.Entity<Bovine>().Property(f => f.Breed).IsRequired();
        builder.Entity<Bovine>().Property(f => f.Location).IsRequired();
        builder.Entity<Bovine>().Property(f => f.BovineImg).IsRequired();
        builder.Entity<Bovine>().Property(f => f.StableId).IsRequired();
        builder.Entity<Bovine>()
            .Property(f => f.RanchUserId)
            .HasConversion(
                v => v.UserIdentifier,     // al guardar: UserId → int
                v => new RanchUserId(v))        // al leer: int → UserId
            .HasColumnName("user_id")
            .IsRequired();

        
        //Vaccine
        builder.Entity<Vaccine>().HasKey(f => f.Id);
        builder.Entity<Vaccine>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Vaccine>().Property(f => f.Name).IsRequired();
        builder.Entity<Vaccine>().Property(f => f.VaccineType).IsRequired();
        builder.Entity<Vaccine>().Property(f => f.VaccineDate).IsRequired();
        builder.Entity<Vaccine>().Property(f => f.VaccineImg).IsRequired();
        builder.Entity<Vaccine>().Property(f => f.BovineId).IsRequired();
        builder.Entity<Vaccine>()
            .Property(f => f.RanchUserId)
            .HasConversion(
                v => v.UserIdentifier,     // al guardar: UserId → int
                v => new RanchUserId(v))        // al leer: int → UserId
            .HasColumnName("user_id")
            .IsRequired();

        
        //Stable
        builder.Entity<Stable>().HasKey(f => f.Id);
        builder.Entity<Stable>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Stable>().Property(f => f.Limit).IsRequired();
        builder.Entity<Stable>()
            .Property(f => f.RanchUserId)
            .HasConversion(
                v => v.UserIdentifier,     // al guardar: UserId → int
                v => new RanchUserId(v))        // al leer: int → UserId
            .HasColumnName("user_id")
            .IsRequired();
        

        /* ---------------------------------------------------------------------------------------------------------- */
        /* Staff Administration BC -------------------------------------------------------------------------------------- */
        //Staff
        builder.Entity<Staff>().HasKey(f => f.Id);
        builder.Entity<Staff>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Staff>().Property(f => f.Name).IsRequired();
        builder.Entity<Staff>()
            .OwnsOne(f => f.EmployeeStatus, navigationBuilder =>
            {
                navigationBuilder.WithOwner().HasForeignKey("Id");
                navigationBuilder.Property(f => f.Value)
                    .IsRequired()
                    .HasColumnName("employee_status");
            });
        builder.Entity<Staff>().Property(f => f.CampaignId).IsRequired();
        builder.Entity<Staff>()
            .Property(f => f.StaffUserId)
            .HasConversion(
                v => v.UserIdentifier,     // al guardar: UserId → int
                v => new StaffUserId(v))        // al leer: int → UserId
            .HasColumnName("user_id")
            .IsRequired();

        /* ---------------------------------------------------------------------------------------------------------- */
        /* Campaign Management BC -------------------------------------------------------------------------------------- */

        builder.Entity<Campaign>().HasKey(c => c.Id);
        builder.Entity<Campaign>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Campaign>().Property(c => c.Name).IsRequired();
        builder.Entity<Campaign>().Property(c => c.Description).IsRequired();
        builder.Entity<Campaign>().Property(c => c.StartDate).IsRequired();
        builder.Entity<Campaign>().Property(c => c.EndDate).IsRequired();
        builder.Entity<Campaign>().Property(c => c.Status).IsRequired();
        //builder.Entity<Campaign>().Property(c => c.Goal).IsRequired();
        builder.Entity<Campaign>().Property(f => f.StableId).IsRequired();
        builder.Entity<Campaign>()
            .Property(f => f.CampaignUserId)
            .HasConversion(
                v => v.UserIdentifier,     // al guardar: UserId → int
                v => new CampaignUserId(v))        // al leer: int → UserId
            .HasColumnName("user_id")
            .IsRequired();
        /* ---------------------------------------------------------------------------------------------------------- * /
        /* ---------------------------------------------------------------------------------------------------------- * /*/

        builder.UseSnakeCaseNamingConvention();
    }
}