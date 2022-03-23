using HallOfFame.Domain.Entities;

namespace HallOfFame.Domain.Repositories;

public interface IPersonRepo : IRepository<Person>
{
    Task<long> CreateAsync(Person person, CancellationToken cancellationToken = default);
}