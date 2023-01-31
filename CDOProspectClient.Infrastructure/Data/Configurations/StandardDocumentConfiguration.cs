using CDOProspectClient.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CDOProspectClient.Infrastructure.Data.Configurations;

public class StandardDocumentConfiguration : IEntityTypeConfiguration<StandardDocument>
{
    public void Configure(EntityTypeBuilder<StandardDocument> builder)
    {
        builder.Property(sd => sd.GovtIssuedSpouseValidIds)
            .HasConversion(
                ids => string.Join(",", ids),
                id => id.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()  
            );
        builder.Property(sd => sd.GovtIssuedValidIds)
            .HasConversion(
                ids => string.Join(",", ids),
                id => id.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()  
            );
    }
}