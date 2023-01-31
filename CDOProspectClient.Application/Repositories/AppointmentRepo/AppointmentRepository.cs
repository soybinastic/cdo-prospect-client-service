namespace CDOProspectClient.Application.Repositories.AppointmentRepo;


public class AppointmentRepository : AppointmentAbstractRepository
{
    public AppointmentRepository(ApplicationDbContext context) : base(context) {}
}