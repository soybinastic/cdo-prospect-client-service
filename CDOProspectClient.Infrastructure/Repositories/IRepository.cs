namespace CDOProspectClient.Infrastructure.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> Create(T entity);
    Task<IEnumerable<T>> FindAll();
    Task<T?> FindOne(int id);
    Task<T> Update(int id, T updatedEntity);
    Task Delete(int id);
}