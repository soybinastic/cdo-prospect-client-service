namespace CDOProspectClient.Contracts.Requirement;

public class RequirementRequest
{
    public int AgentId { get; set; }
    public ScreeningRequest Screening { get; set; } = null!;
    public BriefingRequest Briefing { get; set; } = null!;
}