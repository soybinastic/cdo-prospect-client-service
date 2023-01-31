namespace CDOProspectClient.Infrastructure.Data.Models;

public class SourceOfIncome
{
    public int Id { get; set; }
    public int DocumentId { get; set; }
    public Document Document { get; set; } = null!;
    public LocallyEmployed LocallyEmployed { get; set; } = null!;
    public SelfEmployed SelfEmployed { get; set; } = null!;
    public OverseasFilipinoWorker OverseasFilipinoWorker { get; set; } = null!;
}