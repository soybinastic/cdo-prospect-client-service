using System.ComponentModel.DataAnnotations;
using CDOProspectClient.Contracts.Validators;
using Microsoft.AspNetCore.Http;

namespace CDOProspectClient.Contracts.Requirement;


public class ScreeningRequest
{
    public BuyerInformationRequest BuyerInformation { get; set; } = null!;
    public DocumentRequest Document { get; set; } = null!;
    public ComputationRequest Computation { get; set; } = null!;
    // must be IFormFile type
    public IFormFile InterviewedBy { get; set; } = null!;
    // must be IFormFile type
    public IFormFile Conforme { get; set; } = null!;
    public string? Remarks { get; set; }
}


public class BuyerInformationRequest
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;
    [Required]
    public DateTime BirthDate { get; set; }
    [Required, Phone]
    public string ContactNumber { get; set; } = null!;
    [Required]
    public string Citizenship { get; set; } = null!;
    [Required]
    public string Facebook { get; set; } = null!;
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Please select gender")]
    public Gender? Gender { get; set; }
    [Required(ErrorMessage = "Please select civil status")]
    public CivilStatus? CivilStatus { get; set; }
    [Required(ErrorMessage = "Please select financing")]
    public Financing? Financing { get; set; }
    public TitlingInstructionRequest TitlingInstruction { get; set; } = null!;
    public NegativeDataBankRecordRequest NegativeDataBankRecord { get; set; } = null!;
    public PagIbigMembershipRequest PagIbigMembership { get; set; } = null!;
    public EmployerDetailRequest EmployerDetail { get; set; } = null!;
}

public class TitlingInstructionRequest
{
    [Required(ErrorMessage = "Please select titling instruction option")]
    public TitlingInstructionOption? TitlingInstructionOption { get; set; }
    public string? Data { get; set; }
}

public class NegativeDataBankRecordRequest
{
    public bool CancelledCreditCard { get; set; }
    public bool BouncedCheck { get; set; }
    public bool PendingCourtCases { get; set; }
    public bool UnpaidTelecomBill { get; set; }
    public string? Others { get; set; }
}

public class PagIbigMembershipRequest
{
    public bool PagIBIGMembership { get; set; }
    public int NumberOfYears { get; set; }
    public bool WOHML { get; set; }
    public bool Updated { get; set; }
    public bool WFHL { get; set; }
}

public class EmployerDetailRequest
{
    [Required]
    public string CompanyName { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;
    [Required, Phone]
    public string ContactNumber { get; set; } = null!;
    [Required]
    public string ImmedaiteSuperior { get; set; } = null!;
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
}

public class DocumentRequest
{
    
    // -> Standard Document
    public StandardDocumentRequest StandardDocument { get; set; } = null!;
    // -> Source Of Income
    public SourceOfIncomeRequest SourceOfIncome { get; set; } = null!;
}

public class StandardDocumentRequest
{
    // must be IFormFileCollection type
    [FileLength(maxLength: 2, errorMessage: "Please upload 2 government issued valid ids")]
    public IFormFileCollection GovtIssuedValidIds { get; set; } = null!;
    // must be IFormFileCollection type
    [FileLength(maxLength: 2, errorMessage: "Please upload 2 government issued spouse valid ids")]
    public IFormFileCollection GovtIssuedSpouseValidIds { get; set; } = null!;
    // must be IFormFile type
    public IFormFile TINNumber { get; set; } = null!;
    // must be IFormFile type
    public IFormFile BirthCertificate { get; set; } = null!;
    // must be IFormFile type
    public IFormFile? MarriageCertificate { get; set; }
    // must be IFormFile type
    public IFormFile ClearOneByOnePicture { get; set; } = null!;
    // must be IFormFile type
    public IFormFile ProofOfMailingOrBilling { get; set; } = null!;
    // must be IFormFile type
    public IFormFile PostDatedChecks { get; set; } = null!;
    // must be IFormFile type
    public IFormFile AuthorizeRepresentative { get; set; } = null!;
    // must be IFormFile type
    public IFormFile SPANotarizedAndConsularized { get; set; } = null!;
    // must be IFormFile type
    public IFormFile BankAndPagIbigSPA { get; set; } = null!;
    // must be IFormFile type
    public IFormFile OathOfAllegianceOrAffidavitOfCitizenship { get; set; } = null!;
    // must be IFormFile type
    public IFormFile? Others { get; set; }
}

public class SourceOfIncomeRequest
{
    public LocallyEmployedRequest LocallyEmployed { get; set; } = null!;
    public SelfEmployedRequest SelfEmployed { get; set; } = null!;
    public OverseasFilipinoWorkerRequest OverseasFilipinoWorker { get; set; } = null!;
}

public class LocallyEmployedRequest
{
    public bool Compensation { get; set; }
    public bool LatestITR { get; set; }
    public bool ThreeMonthsOfPayslips { get; set; }
}

public class SelfEmployedRequest
{
    // -> Formal
    public SelfEmployedFormalRequest Formal { get; set; } = null!;
    // -> Informal
    public SelfEmployedInformalRequest Informal { get; set; } = null!;

}

public class SelfEmployedFormalRequest
{
    public bool Latest2YearsITR { get; set; }
    public bool Latest2YearsAFS { get; set; }
    public bool Latest6MonthsBankStatements { get; set; }
}

public class SelfEmployedInformalRequest
{
    public bool COEIdOfSignatory { get; set; }
    public bool COEOtherAttachments { get; set; }
}

public class OverseasFilipinoWorkerRequest
{
    public string Country { get; set; } = null!;
    public bool NCEC { get; set; }
    public bool ThreeMonthsPayslipsOrRemittance { get; set; }
    public bool BankStatements { get; set; }
    public bool PassportWithEntryAndExit  { get; set; }
}

public class ComputationRequest
{
    public decimal SellingPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal NetSellingPrice { get; set; }
    public decimal TaxesAndFees { get; set; }
    public decimal TotalReceivable { get; set; }
    public decimal NumberOfDownpayments { get; set; }
    public decimal EMA { get; set; }
    public decimal GrossIncome { get; set; }
    public decimal MonthlyIncomeRatio { get; set; }
}

public enum TitlingInstructionOption
{
    Individual,
    MarriedTo,
    Spouses,
    CoOwners
}


public enum Financing
{
    Cash,
    PagIBIG,
    Bank,
    Deffered
}

public enum CivilStatus
{
    Single,
    Married,
    WidowOrWidower,
    LegallySeparated
}

public enum Gender
{
    Male,
    Female
}

