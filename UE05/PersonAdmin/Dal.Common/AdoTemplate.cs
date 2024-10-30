using System.Data.Common;

namespace Dal.Common;

public class AdoTemplate(IConnectionFactory connectionFactory)
{
    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, RowMapper<T> rowMapper, CancellationToken cancellationToken = default, params QueryParameter[] parameters) // params = "..." in Java
    {
        // await using ... Nutzt IAsyncDisposable, um sicherzustellen, dass die Verbindung geschlossen wird
        await using DbConnection connection = await connectionFactory.CreateConnectionAsync(cancellationToken);

        await using DbCommand command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameters(command, parameters);

        await using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        var items = new List<T>();
        while (await reader.ReadAsync(cancellationToken))
        {
            // yield return wäre hier sehr gefaehrlich, da wenn jemand das IEnumerable verwendet, aber nicht
            // alle Elemente durchgeht, die Verbindung nicht geschlossen wird (da das Ende der Methode nicht erreicht wird)
            // -> daher: Liste zurückgeben
            items.Add(rowMapper(reader));
        }

        return items;
    }

    public async Task<T?> QuerySingleAsync<T>(string sql, RowMapper<T> rowMapper, CancellationToken cancellationToken = default, params QueryParameter[] parameters) =>
        (await QueryAsync(sql, rowMapper, cancellationToken, parameters)).SingleOrDefault();

    public async Task<int> ExecuteAsync(
        string sql,
        CancellationToken cancellationToken = default,
        params QueryParameter[] parameters)
    {
        await using DbConnection connection = await connectionFactory.CreateConnectionAsync(cancellationToken);

        await using DbCommand command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameters(command, parameters);

        return await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private void AddParameters(DbCommand command, QueryParameter[] parameters)
    {
        foreach (var parameter in parameters)
        {
            DbParameter dbParameter = command.CreateParameter();
            dbParameter.ParameterName = parameter.Name;
            dbParameter.Value = parameter.Value;
            command.Parameters.Add(dbParameter);
        }
    }
}
