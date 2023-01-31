using System.Text.Json.Serialization;

namespace CDOProspectClient.Infrastructure.Data.Models;

public class Agent
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int ProfileId { get; set; }
    public Profile Profile { get; set; } = null!;
    public bool IsActive { get; set; }
    public virtual List<Property> Properties { get; set; } = new();
    public virtual List<Requirement> Requirements { get; set; } = new();
    public virtual List<Appointment> Appointments { get; set; } = new();
}