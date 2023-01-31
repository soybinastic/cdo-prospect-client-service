namespace CDOProspectClient.Infrastructure.Data.Models;


public class Screening
{
    public int Id { get; set; }
    public int RequirementId { get; set; }
    public Requirement Requirement { get; set; } = null!;
    // -> BuyerInformation
    public BuyerInformation BuyerInformation { get; set; } = null!;
    // -> Document
    public Document Document { get; set; } = null!;
    // -> Computation
    public Computation Computation { get; set; } = null!;
    public string InterviewedBy { get; set; } = null!;
    public string Conforme { get; set; } = null!;
    public string? Remarks { get; set; }
    public DateTimeOffset Date { get; set; }
}