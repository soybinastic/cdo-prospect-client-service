using CDOProspectClient.Infrastructure.Data.Models;
using CDOProspectClient.Infrastructure.Repositories;

namespace CDOProspectClient.Application.Repositories.AppointmentRepo;


public abstract class AppointmentAbstractRepository : Repository<Appointment>
{
    protected AppointmentAbstractRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<IEnumerable<Appointment>> FindAll()
    {
        var appointments = await _context.Appointments
            .Include(a => a.Agent)
            .ThenInclude(a => a.Profile)
            .Include(a => a.Client)
            .ToListAsync();
        return appointments;
    }

    public override async Task<Appointment?> FindOne(int id)
    {
        var appointment = await _context.Appointments  
            .Include(a => a.Agent)
            .ThenInclude(a => a.Profile)
            .Include(a => a.Client)
            .FirstOrDefaultAsync(a => a.Id == id);
        return appointment;
    }
}