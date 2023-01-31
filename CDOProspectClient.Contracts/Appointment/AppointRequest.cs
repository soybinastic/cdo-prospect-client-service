using System.ComponentModel.DataAnnotations;

namespace CDOProspectClient.Contracts.Appointment;

public record AppointmentRequest(
    int AgentId,
    DateTime AppointmentDate,
    ClientRequest Client
);

public record ClientRequest(
    [Required]string Name,
    [Required, Phone]string PhoneNumber,
    [Required]string Occupation
);