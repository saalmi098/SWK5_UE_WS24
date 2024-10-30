using System.Data.Common;

namespace Dal.Common;

public interface IConnectionFactory
{
    Task<DbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default);
}
