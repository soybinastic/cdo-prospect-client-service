namespace CDOProspectClient.Infrastructure.Data.Models;

public class SelfEmployedFormal
{
    public int Id { get; set; }
    public int SelfEmployedId { get; set; }
    public SelfEmployed SelfEmployed { get; set; } = null!;
    public bool Latest2YearsITR { get; set; }
    public bool Latest2YearsAFS { get; set; }
    public bool Latest6MonthsBankStatements { get; set; }
}