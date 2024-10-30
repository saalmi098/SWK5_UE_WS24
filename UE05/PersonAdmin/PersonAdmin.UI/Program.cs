using Dal.Common;
using Microsoft.Extensions.Configuration;
using PersonAdmin.BusinessLogic;
using PersonAdmin.Dal.Ado;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Dal.Simple;

//var connectionString = "Data Source=localhost;Initial Catalog=person_db;Persist Security Info=True;User ID=sa;Password=Swk5-rocks!;Trust Server Certificate=True";
//var connectionFactory = new DefaultConnectionFactory(connectionString, "Microsoft.Data.SqlClient");

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

// PersonDbConnection is the connection string name in appsettings.json
var connectionFactory = DefaultConnectionFactory.FromConfiguration(configuration, "PersonDbConnection");

CancellationTokenSource tokenSource = new CancellationTokenSource();

await TestAsync(new SimplePersonDao(), tokenSource.Token);
await TestAsync(new AdoPersonDao(connectionFactory), tokenSource.Token);

//tokenSource.Cancel();

async Task TestAsync(IPersonDao dao, CancellationToken cancellationToken = default)
{
    Console.WriteLine(dao.GetType());

    var service = new PersonService(dao, Console.Out);
    await service.TestFindAllAsync(cancellationToken);
    await service.TestFindByIdAsync(cancellationToken);
    await service.TestUpdateAsync(1, cancellationToken);
    await service.TestTransactionsAsync(cancellationToken);
    await service.TestFindAllAsync(cancellationToken);

    Console.WriteLine("---------------------------------");
}