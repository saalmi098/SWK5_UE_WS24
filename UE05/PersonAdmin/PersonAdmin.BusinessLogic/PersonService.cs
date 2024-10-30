using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;
using System.Transactions;

namespace PersonAdmin.BusinessLogic;

public class PersonService(IPersonDao personDao, TextWriter writer)
{
    // statt "async void" immer "async Task" verwenden
    public async Task TestFindAllAsync(CancellationToken cancellationToken = default)
    {
        writer.WriteLine("FindAll");

        var persons = await personDao.FindAllAsync(cancellationToken);

        persons
            .ToList()
            .ForEach(p => writer.WriteLine($"{p.Id,5} | {p.FirstName,-10} | {p.LastName,-12} | {p.DateOfBirth,10:dd.MM.yyyy}"));

        writer.WriteLine();
    }

    public async Task TestFindByIdAsync(CancellationToken cancellationToken = default)
    {
        Person? p1 = await personDao.FindByAsync(1, cancellationToken);
        writer.WriteLine($"FindById(1) -> {p1?.ToString() ?? "<null>"}");
        writer.WriteLine();
    }

    public async Task TestUpdateAsync(int id, CancellationToken cancellationToken = default)
    {
        Person? person = await personDao.FindByAsync(id, cancellationToken);
        writer.WriteLine($"before update: {person?.ToString() ?? "<null>"}");
        if (person is null)
            return;

        person.DateOfBirth = person.DateOfBirth.AddDays(id);
        await personDao.UpdateAsync(person, cancellationToken);

        person = await personDao.FindByAsync(id, cancellationToken);
        writer.WriteLine($"after update: {person?.ToString() ?? "<null>"}");
    }

    public async Task TestTransactionsAsync(CancellationToken cancellationToken = default)
    {
        writer.WriteLine("TestTransactions");

        try
        {
            // we want to make sure that either both updates are persisted or none

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await personDao.UpdateAsync(new Person(2, "Before", "Exception", DateTime.Now), cancellationToken);

                throw new Exception();

                await personDao.UpdateAsync(new Person(3, "After", "Exception", DateTime.Now), cancellationToken);

                // wenn wir aufgrund einer Exception aus dem using Block herausspringen,
                // wird die Transaktion automatisch rollbacked

                transaction.Complete(); // commit (dadurch wird die Transaktion committed und nicht rollbacked)
            }
        }
        catch (Exception e)
        {
            writer.WriteLine(e);
        }

        writer.WriteLine();
    }
}
