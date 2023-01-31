using CDOProspectClient.Infrastructure.Data.Models;
using CDOProspectClient.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CDOProspectClient.Application.Repositories.AgentRepo;

public abstract class AgentAbstractRepository : Repository<Agent>
{
    public AgentAbstractRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<Agent?> FindOne(int id)
    {
        var result = await _context.Agents
            .Include(a => a.Profile)
            .FirstOrDefaultAsync(a => a.Id == id);

        return result;
    }

    public override async Task<IEnumerable<Agent>> FindAll()
    {
        var results = await _context.Agents.Include(a => a.Profile)
            .ToListAsync();
        return results;
    }

    public abstract Task<Agent?> FindbyEmail(string email);
    public abstract Task<Agent?> FindByUserId(string userId);
}