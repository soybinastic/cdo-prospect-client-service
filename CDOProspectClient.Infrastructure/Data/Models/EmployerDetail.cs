namespace CDOProspectClient.Infrastructure.Data.Models;


public class EmployerDetail
{
    public int Id { get; set; }
    public int BuyerInformationId { get; set; }
    public BuyerInformation BuyerInformation { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string ContactNumber { get; set; } = null!;
    public string ImmedaiteSuperior { get; set; } = null!;
    public string Email { get; set; } = null!;
}