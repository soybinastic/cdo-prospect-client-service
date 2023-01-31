using CDOProspectClient.Infrastructure.Data.Models;
using CDOProspectClient.Infrastructure.Repositories;

namespace CDOProspectClient.Application.Repositories.AdminRepo;

public abstract class AdminAbstractRepository : Repository<Admin>
{
    protected AdminAbstractRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<IEnumerable<Admin>> FindAll()
    {
        var admins = await _context.Admins
            .Include(a => a.Profile)
            .ToListAsync();
        return admins;
    }

    public abstract Task<Admin?> FindByUserId(string userId);
}