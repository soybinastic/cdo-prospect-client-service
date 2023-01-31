namespace CDOProspectClient.Infrastructure.Data.Models;


public class PagIbigMembership
{
    public int Id { get; set; }
    public int BuyerInformationId { get; set; }
    public BuyerInformation BuyerInformation { get; set; } = null!;
    public bool PagIBIGMembership { get; set; }
    public int NumberOfYears { get; set; }
    public bool WOHML { get; set; }
    public bool Updated { get; set; }
    public bool WFHL { get; set; }
}