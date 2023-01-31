namespace CDOProspectClient.Infrastructure.Data.Models;

public class Client
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Occupation { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}