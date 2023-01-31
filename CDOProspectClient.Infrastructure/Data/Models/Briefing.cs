using System.Text.Json.Serialization;

namespace CDOProspectClient.Infrastructure.Data.Models;

public class Briefing
{
    public int Id { get; set; }
    public int RequirementId { get; set; }
    public Requirement Requirement { get; set; } = null!;
    [JsonIgnore]
    public BuyerInformation Buyer { get; set; } = null!;
    public DateTime ReservationDate { get; set; }
    public string ProjectName { get; set; } = null!;
    public string PH { get; set; } = null!;
    public string Block { get; set; } = null!;
    public string Lot { get; set; } = null!;
    public string SalesChannel { get; set; } = null!;
    public string Broker { get; set; } = null!;
    public string DirectSeller { get; set; } = null!;
    public Financing Financing { get; set; }
    public List<string> ReservationDocuments { get; set; } = new();
    public string ReferenceNumber { get; set; } = null!;
    // image url
    public string Conforme { get; set; } = null!;
    // image url
    public string BriefedBy { get; set; } = null!;
    // image url
    public string Witness { get; set; } = null!;
    public DateTimeOffset Date { get; set; }
}