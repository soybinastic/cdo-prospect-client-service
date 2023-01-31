namespace CDOProspectClient.Infrastructure.Data.Models;

public class SelfEmployedInformal
{
    public int Id { get; set; }
    public int SelfEmployedId { get; set; }
    public SelfEmployed SelfEmployed { get; set; } = null!;
    public bool COEIdOfSignatory { get; set; }
    public bool COEOtherAttachments { get; set; }
}