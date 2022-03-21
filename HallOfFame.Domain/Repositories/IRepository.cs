namespace HallOfFame.Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task<int> AddAsync(T entity, bool persist = true, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(T entity, bool persist = true, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(int id, bool persist = true, CancellationToken cancellationToken = default);
    Task<T> FindAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task ExecuteQueryAsync(string sql, object[] sqlParametersObjects, CancellationToken cancellationToken = default);
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
}