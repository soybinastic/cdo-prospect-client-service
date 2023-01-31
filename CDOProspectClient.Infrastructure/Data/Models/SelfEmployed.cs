namespace CDOProspectClient.Infrastructure.Data.Models;

public class SelfEmployed
{
    public int Id { get; set; }
    public int SourceOfIncomeId { get; set; }
    public SourceOfIncome SourceOfIncome { get; set; } = null!;

    // -> Formal
    public SelfEmployedFormal Formal { get; set; } = null!;
    // -> Informal
    public SelfEmployedInformal Informal { get; set; } = null!;

}