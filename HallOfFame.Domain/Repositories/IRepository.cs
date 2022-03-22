namespace HallOfFame.Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task<int> AddAsync(T person, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(T person, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<T> FindAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task ExecuteQueryAsync(string sql, object[] sqlParametersObjects, CancellationToken cancellationToken = default);
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
}