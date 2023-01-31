using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CDOProspectClient.Contracts.Requirement;

public class BriefingRequest
{
    public DateTime ReservationDate { get; set; }
    [Required]
    public string ProjectName { get; set; } = null!;
    [Required]
    public string PH { get; set; } = null!;
    [Required]
    public string Block { get; set; } = null!;
    [Required]
    public string Lot { get; set; } = null!;
    [Required]
    public string SalesChannel { get; set; } = null!;
    [Required]
    public string Broker { get; set; } = null!;
    [Required]
    public string DirectSeller { get; set; } = null!;
    public Financing Financing { get; set; }
    public List<string> ReservationDocuments { get; set; } = new();
    [Required]
    public string ReferenceNumber { get; set; } = null!;
    // image url
    
    public IFormFile Conforme { get; set; } = null!;
    // image url
    public IFormFile BriefedBy { get; set; } = null!;
    // image url
    public IFormFile Witness { get; set; } = null!;
    public DateTimeOffset Date { get; set; }
}