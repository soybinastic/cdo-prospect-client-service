namespace CDOProspectClient.Infrastructure.Data.Models;

public class Document
{
    public int Id { get; set; }
    public int ScreeningId { get; set; }
    public Screening Screening { get; set; } = null!;

    // -> Standard Document
    public StandardDocument StandardDocument { get; set; } = null!;
    // -> Source Of Income
    public SourceOfIncome SourceOfIncome { get; set; } = null!;
}