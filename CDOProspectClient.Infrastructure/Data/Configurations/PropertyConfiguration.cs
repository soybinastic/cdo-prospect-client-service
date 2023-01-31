using CDOProspectClient.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CDOProspectClient.Infrastructure.Data.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasOne(p => p.Agent)
            .WithMany(p => p.Properties)
            .HasForeignKey(p => p.AssignedTo);
        
        builder.Property(p => p.Price)
            .HasPrecision(18, 2);
    }
}