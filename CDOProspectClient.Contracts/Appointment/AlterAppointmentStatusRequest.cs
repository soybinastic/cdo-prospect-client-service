namespace CDOProspectClient.Contracts.Appointment;

public record AlterAppointmentStatusRequest(
    int AppointmentId,
    int Status
);