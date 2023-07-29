using BloodDonationApp.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BloodDonationApp.DataAccess.Entityframework.Contexts;

public class BloodDonationAppContext : DbContext
{
    public BloodDonationAppContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Hospital> Hospitals { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<BloodGroup> BloodGroups { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
