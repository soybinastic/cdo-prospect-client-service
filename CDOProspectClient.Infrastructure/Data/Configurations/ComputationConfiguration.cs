using CDOProspectClient.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CDOProspectClient.Infrastructure.Data.Configurations;

public class ComputationConfiguration : IEntityTypeConfiguration<Computation>
{
    public void Configure(EntityTypeBuilder<Computation> builder)
    {
        builder.Property(c => c.SellingPrice)
            .HasPrecision(18, 2);
        
        builder.Property(c => c.GrossIncome)
            .HasPrecision(18, 2);
        
        builder.Property(c => c.NumberOfDownpayments)
            .HasPrecision(18, 2);
        
        builder.Property(c => c.NetSellingPrice)
            .HasPrecision(18, 2);
        
        builder.Property(c => c.Discount)
            .HasPrecision(18, 2);
        
        builder.Property(c => c.TaxesAndFees)
            .HasPrecision(18, 2);
        
        builder.Property(c => c.TotalReceivable)
            .HasPrecision(18, 2);
        
        builder.Property(c => c.EMA)
            .HasPrecision(18, 2);
        
        builder.Property(c => c.MonthlyIncomeRatio)
            .HasPrecision(18, 2);
    }
}