namespace CDOProspectClient.Infrastructure.Data.Models;


public class Requirement
{
    public int Id { get; set; }
    // Agent navigation property is not actually nullable property.
    // It's required, but I just set it to nullable to avoid error and issues
    // during updating database.
    public Agent? Agent { get; set; } = null!;
    public Screening Screening { get; set; } = null!;
    public Briefing Briefing { get; set; } = null!;
    public Status Status { get; set; }
}

public enum Status
{
    Approved,
    Pending,
    Cancelled,
    Archived,
    Forwarded,
    Received,
    Rejected
}