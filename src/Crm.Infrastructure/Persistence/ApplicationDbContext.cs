using Crm.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Crm.Infrastructure.Persistence;

//public class ApplicationDbContext : DbContext
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Interaction> Interactions { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Deal> Deals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Deal>()
        .Property(d => d.Stage)
        .HasConversion<string>(); // This tells EF to store the enum as a string
    }
}