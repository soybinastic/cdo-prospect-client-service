namespace CDOProspectClient.Infrastructure.Data.Models;

public class Computation
{
    public int Id { get; set; }
    public int ScreeningId { get; set; }
    public Screening Screening { get; set; } = null!;
    public decimal SellingPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal NetSellingPrice { get; set; }
    public decimal TaxesAndFees { get; set; }
    public decimal TotalReceivable { get; set; }
    public decimal NumberOfDownpayments { get; set; }
    public decimal EMA { get; set; }
    public decimal GrossIncome { get; set; }
    public decimal MonthlyIncomeRatio { get; set; }
}