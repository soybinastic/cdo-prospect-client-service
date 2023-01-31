using CDOProspectClient.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CDOProspectClient.Infrastructure.Data.Configurations;


public class BriefingConfiguration : IEntityTypeConfiguration<Briefing>
{
    public void Configure(EntityTypeBuilder<Briefing> builder)
    {
        builder.Property(b => b.Financing)
            .HasConversion<string>();
        builder.Property(b => b.ReservationDocuments)
            .HasConversion(
                rds => string.Join(',', rds),
                rd => rd.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );
    }
}