using CDOProspectClient.Infrastructure.Data.Models;
namespace CDOProspectClient.Application.Repositories.AgentRepo;

public class AgentRepository : AgentAbstractRepository
{
    public AgentRepository(ApplicationDbContext context) : base(context){}
    public override async Task<Agent?> FindbyEmail(string email)
    {
        var result = await _context.Agents.Include(a => a.Profile)
            .FirstOrDefaultAsync(a => a.Profile.Email == email);
        return result;
    }

    public override async Task<Agent?> FindByUserId(string userId)
    {
        var result = await _context.Agents.Include(a => a.Profile)
            .FirstOrDefaultAsync(a => a.UserId == userId);
        return result;
    }
}