namespace CDOProspectClient.Infrastructure.Data.Models;


public class Appointment
{
    public int Id { get; set; }
    public int AgentId { get; set; }
    public Agent Agent { get; set; } = null!;
    public Client Client { get; set; } = null!;
    public DateTime DateAppointment { get; set; }
    public AppointmentStatus Status { get; set; }
}

public enum AppointmentStatus
{
    Pending,
    Confirm,
    Archived
}