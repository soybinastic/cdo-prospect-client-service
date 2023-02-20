namespace CDOProspectClient.Infrastructure.Data.Models;


public class NotificationEntityType
{
    public int Id { get; set; }
    public string Entity { get; set; } = null!;
    public string NotificationMessage { get; set; } = null!;
}