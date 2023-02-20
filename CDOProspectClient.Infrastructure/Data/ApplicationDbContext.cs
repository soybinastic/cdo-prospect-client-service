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
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<NotificationEntityType> NotificationEntityTypes => Set<NotificationEntityType>();
    public DbSet<Buyer> Buyers => Set<Buyer>();

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
        
        builder.Entity<NotificationEntityType>()
            .HasData(new NotificationEntityType[]
            {
                new NotificationEntityType { Id = 1, Entity = "Requirement", NotificationMessage = "Forwarded a requirement" },
                new NotificationEntityType { Id = 2, Entity = "Requirement", NotificationMessage = "Cancelled the forwarded requirement" },
                new NotificationEntityType { Id = 3, Entity = "Requirement", NotificationMessage = "Approved the requirement you have submit" },
                new NotificationEntityType { Id = 4, Entity = "Requirement", NotificationMessage = "Your requirement has been rejected" },
                new NotificationEntityType { Id = 5, Entity = "Appointment", NotificationMessage = "Someone has set you up with an appointment" }
            });
        
        builder.Entity<Notification>()
            .Property(n => n.Status)
            .HasConversion<string>();

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}