namespace CDOProspectClient.Infrastructure.Data.Models;


public class TitlingInstruction
{
    public int Id { get; set; }
    public int BuyerInformationId { get; set; }
    public BuyerInformation BuyerInformation { get; set; } = null!;
    public TitlingInstructionOption TitlingInstructionOption { get; set; }
    public string? Data { get; set; }
}

public enum TitlingInstructionOption
{
    Individual,
    MarriedTo,
    Spouses,
    CoOwners
}