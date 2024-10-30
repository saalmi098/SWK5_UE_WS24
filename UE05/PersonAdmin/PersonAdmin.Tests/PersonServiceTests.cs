using PersonAdmin.BusinessLogic;
using PersonAdmin.Dal.Simple;

namespace PersonAdmin.Tests;

[TestClass]
public class PersonServiceTests
{
    [TestMethod]
    public async Task UpdateWithInvalidIdWritesCorrectResult()
    {
        // Vorteil: Nutzt SimplePersonDao, um die Business Logic zu testen (keine echte Datenbank notwendig)
        var dao = new SimplePersonDao();
        var writer = new StringWriter();
        var service = new PersonService(dao, writer);

        await service.TestUpdateAsync(int.MaxValue);
        Assert.AreEqual($"before update: <null>\r\n", writer.ToString());
    }
}
