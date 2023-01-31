using CDOProspectClient.Infrastructure.Data.Models;

namespace CDOProspectClient.Application.Repositories.PropertyRepo;


public class PropertyRepository : PropertyAbstractRepository
{
    public PropertyRepository(ApplicationDbContext context) : base(context) {}
    public override async Task<IEnumerable<Property>> FindAvailable()
    {
        var availables = await _context.Properties
            .Include(p => p.Agent)
            .Include(p => p.PropertyType)
            .Where(p => p.Available)
            .ToListAsync();
        return availables;
    }

    public override async Task<IEnumerable<Property>> FindByAgents(int agentId)
    {
        var agentProperties = await _context.Properties
            .Include(p => p.Agent)
            .Include(p => p.PropertyType)
            .Where(p => p.AssignedTo == agentId)
            .ToListAsync();

        return agentProperties;
    }

    public override async Task<IEnumerable<PropertyType>> GetAllTypes()
    {
        var types = await _context.PropertyTypes.ToListAsync();
        return types;
    }

    public override async Task<PropertyType?> GetType(int typeId)
    {
        var type = await _context.PropertyTypes.FirstOrDefaultAsync(pt => pt.Id == typeId);
        return type;
    }
}