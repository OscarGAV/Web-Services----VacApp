using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        
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
        
        //Vaccine
        
        builder.Entity<Vaccine>().HasKey(f => f.Id);
        builder.Entity<Vaccine>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Vaccine>().Property(f => f.Name).IsRequired();
        builder.Entity<Vaccine>().Property(f => f.VaccineType).IsRequired();
        builder.Entity<Vaccine>().Property(f => f.VaccineDate).IsRequired();
        builder.Entity<Vaccine>().Property(f => f.VaccineImg).IsRequired();
        builder.Entity<Vaccine>().Property(f => f.BovineId).IsRequired();
        
        /* ---------------------------------------------------------------------------------------------------------- */
        
        builder.UseSnakeCaseNamingConvention();
    }
}