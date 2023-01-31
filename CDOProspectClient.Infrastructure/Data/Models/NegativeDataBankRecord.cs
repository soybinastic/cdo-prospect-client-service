namespace CDOProspectClient.Infrastructure.Data.Models;

public class NegativeDataBankRecord
{
    public int Id { get; set; }
    public int BuyerInformationId { get; set; }
    public BuyerInformation BuyerInformation { get; set; } = null!;
    public bool CancelledCreditCard { get; set; }
    public bool BouncedCheck { get; set; }
    public bool PendingCourtCases { get; set; }
    public bool UnpaidTelecomBill { get; set; }
    public string? Others { get; set; }
}