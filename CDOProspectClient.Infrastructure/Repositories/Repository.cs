using CDOProspectClient.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CDOProspectClient.Infrastructure.Repositories;

public abstract class Repository<T> : IRepository<T> where T : class
{
    public readonly ApplicationDbContext _context;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    public virtual async Task<T> Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task Delete(int id)
    {
        var entity = await FindOne(id);
        if(entity is null)
        {
            throw new Exception("Entity to delete not found!");
        }
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<IEnumerable<T>> FindAll()
    {
        var enities = await _context.Set<T>()
            .ToListAsync();
        
        return enities;
    }

    public virtual async Task<T?> FindOne(int id)
    {
        var entity = await _context.Set<T>()
            .FindAsync(id);
        return entity;
    }

    public async Task<T> Update(int id, T updatedEntity)
    {
        var entityCheck = await FindOne(id);
        if(entityCheck is null)
        {
            throw new Exception("Entity to update not found!");
        }

        _context.Set<T>().Update(updatedEntity);
        await _context.SaveChangesAsync();
        return updatedEntity;
    }
}