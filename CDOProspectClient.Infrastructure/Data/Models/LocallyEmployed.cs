namespace CDOProspectClient.Infrastructure.Data.Models;

public class LocallyEmployed
{
    public int Id { get; set; }
    public int SourceOfIncomeId { get; set; }
    public SourceOfIncome SourceOfIncome { get; set; } = null!;
    public bool Compensation { get; set; }
    public bool LatestITR { get; set; }
    public bool ThreeMonthsOfPayslips { get; set; }
}