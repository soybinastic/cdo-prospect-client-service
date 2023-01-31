using CDOProspectClient.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CDOProspectClient.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<Agent> Agents => Set<Agent>();
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<PropertyType> PropertyTypes => Set<PropertyType>();
    public DbSet<Screening> Screenings => Set<Screening>();
    public DbSet<BuyerInformation> BuyerInformations => Set<BuyerInformation>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<Computation> Computations => Set<Computation>();
    public DbSet<Briefing> Briefings => Set<Briefing>();
    public DbSet<Requirement> Requirements => Set<Requirement>();
    public DbSet<Evaluation> Evaluations => Set<Evaluation>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Admin> Admins => Set<Admin>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.Entity<Property>()
        //     .HasOne(p => p.Agent)
        //     .WithMany(p => p.Properties)
        //     .HasForeignKey(p => p.AssignedTo);
        
        // seed property type data
        builder.Entity<PropertyType>()
            .HasData(new PropertyType[] { 
                new PropertyType { Id = 1, Name = "Single Unit" }, 
                new PropertyType { Id = 2, Name = "Combined Unit" } 
                });

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}