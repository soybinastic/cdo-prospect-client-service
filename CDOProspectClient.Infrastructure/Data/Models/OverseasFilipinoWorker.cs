namespace CDOProspectClient.Infrastructure.Data.Models;

public class OverseasFilipinoWorker
{
    public int Id { get; set; }
    public int SourceOfIncomeId { get; set; }
    public SourceOfIncome SourceOfIncome { get; set; } = null!;
    public string Country { get; set; } = null!;
    public bool NCEC { get; set; }
    public bool ThreeMonthsPayslipsOrRemittance { get; set; }
    public bool BankStatements { get; set; }
    public bool PassportWithEntryAndExit  { get; set; }
}