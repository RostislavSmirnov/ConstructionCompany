using Domain.Entities.ConstructionProject;
using Domain.Entities.Staff;
using Microsoft.EntityFrameworkCore;

namespace Persistence.SqlLite.DbContexts;

public class ConstructionCompanyDbContext : DbContext
{
    public DbSet<BuildingObject> BuildingObjects { get; set; } = null!;
    public DbSet<SupplyHub> SupplyHubs { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;

    public ConstructionCompanyDbContext(DbContextOptions<ConstructionCompanyDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BuildingObject>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.Description).HasMaxLength(2000);

            b.HasMany(x => x.SupplyHubs)
             .WithOne(x => x.BuildingObject)
             .HasForeignKey(x => x.BuildingObjectId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<SupplyHub>(s =>
        {
            s.HasKey(x => x.Id);
            s.Property(x => x.Name).IsRequired().HasMaxLength(200);
            s.Property(x => x.Description).HasMaxLength(2000);

            s.HasOne(x => x.Parent)
             .WithMany()
             .HasForeignKey(x => x.ParentId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Account>(a =>
        {
            a.HasKey(x => x.Id);
            a.Property(x => x.Login).IsRequired().HasMaxLength(100);
            a.Property(x => x.Role).IsRequired().HasMaxLength(50);
            a.Property(x => x.PasswordHash).IsRequired().HasMaxLength(256);

            a.HasOne<Employee>()
             .WithOne(e => e.Account)
             .HasForeignKey<Employee>(e => e.AccountId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Employee>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.Property(x => x.Surname).IsRequired().HasMaxLength(100);
            e.Property(x => x.Patronymic).HasMaxLength(100);
        });
    }
}
