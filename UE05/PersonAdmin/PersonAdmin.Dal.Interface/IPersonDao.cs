using PersonAdmin.Domain;

namespace PersonAdmin.Dal.Interface;

public interface IPersonDao
{
    Task<IEnumerable<Person>> FindAllAsync(CancellationToken cancellationToken = default);
    Task<Person?> FindByAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Person person, CancellationToken cancellationToken = default);
}
