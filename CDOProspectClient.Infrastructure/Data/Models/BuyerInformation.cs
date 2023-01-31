using System.Text.Json.Serialization;

namespace CDOProspectClient.Infrastructure.Data.Models;

public class BuyerInformation
{
    public int Id { get; set; } 
    public int? ScreeningId { get; set; }
    public Screening? Screening { get; set; }
    public int BriefingId { get; set; }
    [JsonIgnore]
    public Briefing? Briefing { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string ContactNumber { get; set; } = null!;
    public string Citizenship { get; set; } = null!;
    public string Facebook { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Gender Gender { get; set; }
    public CivilStatus CivilStatus { get; set; }

    // -> TitlingInstruction
    public TitlingInstruction TitlingInstruction { get; set; } = null!;

    public Financing Financing { get; set; }

    // -> NegativeDataBankRecord
    public NegativeDataBankRecord NegativeDataBankRecord { get; set; } = null!;

    // -> Pag-IBIG Membership
    public PagIbigMembership PagIbigMembership { get; set; } = null!;

    // -> Employer Details
    public EmployerDetail EmployerDetails { get; set; } = null!;
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