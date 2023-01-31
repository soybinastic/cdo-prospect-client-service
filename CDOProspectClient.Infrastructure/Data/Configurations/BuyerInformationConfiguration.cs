using CDOProspectClient.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CDOProspectClient.Infrastructure.Data.Configurations;

public class BuyerInformationConfiguration : IEntityTypeConfiguration<BuyerInformation>
{
    public void Configure(EntityTypeBuilder<BuyerInformation> builder)
    {
        builder.Property(bi => bi.Gender)
            .HasConversion<string>();
        builder.Property(bi => bi.CivilStatus)
            .HasConversion<string>();
        builder.Property(bi => bi.Financing)
            .HasConversion<string>();
    }
}