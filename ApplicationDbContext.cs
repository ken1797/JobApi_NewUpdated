using Microsoft.EntityFrameworkCore;
using Teknorix.JobsApi.Domain.Entities;

namespace Teknorix.JobsApi.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(b =>
        {
            b.Property(p => p.Title).IsRequired().HasMaxLength(100);
            b.Property(p => p.Description).IsRequired();
            b.Property(p => p.Code).IsRequired();
            b.HasOne(p => p.Location).WithMany(l => l.Jobs).HasForeignKey(p => p.LocationId);
            b.HasOne(p => p.Department).WithMany(d => d.Jobs).HasForeignKey(p => p.DepartmentId);
        });

        modelBuilder.Entity<Department>(b =>
        {
            b.Property(p => p.Title).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Location>(b =>
        {
            b.Property(p => p.Title).IsRequired().HasMaxLength(100);
            b.Property(p => p.City).IsRequired();
            b.Property(p => p.State).IsRequired();
            b.Property(p => p.Country).IsRequired();
            b.Property(p => p.Zip).IsRequired();
        });

        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Title = "Software Development", CreatedAt = DateTime.UtcNow },
            new Department { Id = 2, Title = "Project Management", CreatedAt = DateTime.UtcNow }
        );
        modelBuilder.Entity<Location>().HasData(
            new Location { Id = 1, Title = "US Head Office", City = "Baltimore", State = "MD", Country = "United States", Zip = "21202", CreatedAt = DateTime.UtcNow },
            new Location { Id = 2, Title = "India Office", City = "Pune", State = "MH", Country = "India", Zip = "411001", CreatedAt = DateTime.UtcNow }
        );
    }
}
