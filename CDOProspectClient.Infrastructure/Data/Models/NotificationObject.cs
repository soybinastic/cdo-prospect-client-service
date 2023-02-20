namespace CDOProspectClient.Infrastructure.Data.Models;


public class NotificationObject
{
    public int Id { get; set; }
    public int EntityId { get; set; }
    public int NotificationEntityTypeId { get; set; }
    public NotificationEntityType NotificationEntityType { get; set; } = null!;
}