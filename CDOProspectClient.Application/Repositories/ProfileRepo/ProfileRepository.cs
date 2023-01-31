using CDOProspectClient.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CDOProspectClient.Application.Repositories.ProfileRepo;

public class ProfileRepository : ProfileAbstractRepository
{
    public ProfileRepository(ApplicationDbContext context) : base(context) {}
    public override async Task<Profile?> FindByEmail(string email)
    {
        return await _context.Profiles.FirstOrDefaultAsync(p => p.Email == email);
    }
}