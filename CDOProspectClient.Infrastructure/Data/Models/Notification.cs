namespace CDOProspectClient.Infrastructure.Data.Models;

public class Notification
{
    public int Id { get; set; }
    // notification creator
    public string ActorId { get; set; } = null!;
    // notification receiver
    public string NotifierId { get; set; } = null!;
    public int NotificationObjectId { get; set; }
    public NotificationObject NotificationObject { get; set; } = null!;
    public NotificationStatus Status { get; set; }
    public DateTime DateNotified { get; set; }
}

public enum NotificationStatus
{
    Delivered,
    Seen
}