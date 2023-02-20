namespace CDOProspectClient.Infrastructure.Data.Models;

public class Buyer
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Occupation { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}