using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace Dal.Common;

public class DefaultConnectionFactory : IConnectionFactory
{
    private readonly DbProviderFactory dbProviderFactory;

    public static IConnectionFactory FromConfiguration(IConfiguration config, string connectionStringName)
    {
        var connectionString = config.GetConnectionString(connectionStringName);
        var providerName = config["ProviderName"];

        return new DefaultConnectionFactory(connectionString!, providerName!);
    }

    public DefaultConnectionFactory(string connectionString, string providerName)
    {
        ConnectionString = connectionString;
        ProviderName = providerName;
        
        DbProviderFactories.RegisterFactory(
            "Microsoft.Data.SqlClient",
            Microsoft.Data.SqlClient.SqlClientFactory.Instance);

        DbProviderFactories.RegisterFactory(
            "MySql.Data.MySqlClient",
            MySql.Data.MySqlClient.MySqlClientFactory.Instance);

        dbProviderFactory = DbProviderFactories.GetFactory(providerName);
    }

    public string ConnectionString { get; }
    public string ProviderName { get; }

    public async Task<DbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default)
    {
        //cancellationToken.ThrowIfCancellationRequested();

        DbConnection connection = dbProviderFactory.CreateConnection()
            ?? throw new InvalidOperationException($"Failed to create connection for provider {ProviderName}");

        connection.ConnectionString = ConnectionString;
        await connection.OpenAsync(cancellationToken);

        return connection;
    }
}
