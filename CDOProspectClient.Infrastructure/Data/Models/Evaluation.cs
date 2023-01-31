namespace CDOProspectClient.Infrastructure.Data.Models;

public class Evaluation
{
    public int Id { get; set; }
    public int RequirementId { get; set; }
    public Requirement Requirement { get; set; } = null!;
    public DateTimeOffset Date { get; set; }
    public EvaluationStatus Status { get; set; }
}

public enum EvaluationStatus
{
    Pending,
    Approved,
    Cancelled,
    Rejected,
}