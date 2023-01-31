using CDOProspectClient.Infrastructure.Data.Models;
using CDOProspectClient.Infrastructure.Repositories;

namespace CDOProspectClient.Application.Repositories.PropertyRepo;

public abstract class PropertyAbstractRepository : Repository<Property>
{
    public PropertyAbstractRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<Property?> FindOne(int id)
    {
        var property = await _context.Properties
            .Include(p => p.Agent)
            .Include(p => p.PropertyType)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        return property;
    }

    public override async Task<IEnumerable<Property>> FindAll()
    {
        var properties = await _context.Properties
            .Include(p => p.Agent)
            .Include(p => p.PropertyType)
            .ToListAsync();
        
        return properties;
    }

    public abstract Task<IEnumerable<Property>> FindAvailable();
    public abstract Task<IEnumerable<Property>> FindByAgents(int agentId);
    public abstract Task<IEnumerable<PropertyType>> GetAllTypes();
    public abstract Task<PropertyType?> GetType(int typeId);
}