using CDOProspectClient.Infrastructure.Data.Models;

namespace CDOProspectClient.Application.Repositories.AdminRepo;


public class AdminRepository : AdminAbstractRepository
{
    public AdminRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<Admin?> FindByUserId(string userId)
    {
        var admin = await _context.Admins
            .Include(a => a.Profile)
            .FirstOrDefaultAsync(a => a.UserId == userId);
        return admin;
    }
}