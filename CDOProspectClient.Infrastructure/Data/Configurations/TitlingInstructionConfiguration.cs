using CDOProspectClient.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CDOProspectClient.Infrastructure.Data.Configurations;

public class TitlingInstructionConfiguration : IEntityTypeConfiguration<TitlingInstruction>
{
    public void Configure(EntityTypeBuilder<TitlingInstruction> builder)
    {
        builder.Property(t => t.TitlingInstructionOption)
            .HasConversion<string>();
    }
}