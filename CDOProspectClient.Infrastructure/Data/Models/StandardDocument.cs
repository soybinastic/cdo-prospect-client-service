namespace CDOProspectClient.Infrastructure.Data.Models;


public class StandardDocument
{
    public int Id { get; set; }
    public int DocumentId { get; set; }
    public Document Document { get; set; } = null!;
    public List<string> GovtIssuedValidIds { get; set; } = new();
    public List<string> GovtIssuedSpouseValidIds { get; set; } = new();
    public string TINNumber { get; set; } = null!;
    public string BirthCertificate { get; set; } = null!;
    public string? MarriageCertificate { get; set; }
    public string ClearOneByOnePicture { get; set; } = null!;
    public string ProofOfMailingOrBilling { get; set; } = null!;
    public string PostDatedChecks { get; set; } = null!;
    public string AuthorizeRepresentative { get; set; } = null!;
    public string SPANotarizedAndConsularized { get; set; } = null!;
    public string BankAndPagIbigSPA { get; set; } = null!;
    public string OathOfAllegianceOrAffidavitOfCitizenship { get; set; } = null!;
    public string? Others { get; set; }
}