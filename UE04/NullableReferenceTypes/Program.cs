#nullable enable
// nullable Prüfung für dieses File aktivieren (im csproj-File kann es auch global aktiviert werden)
// Wenn man das Feature aktiviert, unterstützt der Compiler und zeigt Warnungen an, wenn ein Null-Check fehlt

using System.Diagnostics.CodeAnalysis;

var person = new Person("Huber", "Franz");
person.FirstName = null;
person.LastName = null;
person.LastName = "Huber-Mayr";

if (person.FirstName != null)
{
    // ohne Null-Check gibt der Compiler eine Warnung aus
    var firstUpper = person.FirstName.ToUpper();
}
var lastUpper = person.LastName.ToUpper();

IEnumerable<Person>? persons = GetPersons();
if (persons != null)
{
    PrintPersons(persons);

    if (TryGetPerson(persons, "Huber", out Person? p))
    {
        // wenn hier True zurückkommt, ist p sicher nicht null - das weiss aber der Compiler nicht
        // Console.WriteLine(p!.LastName); // Var. 1
        // Var. 2 - Attribut "NotNullWhen" bei TryGetPerson out Parameter
    }
}
else
{
    Console.WriteLine("persons is null");
}

static IEnumerable<Person>? GetPersons()
{
    return null;
}

static void PrintPersons(IEnumerable<Person> persons)
{
    foreach (var p in persons)
    {
        Console.WriteLine(p);
    }
}

static bool TryGetPerson(IEnumerable<Person> persons, string lastName, [NotNullWhen(true)] out Person? person)
{
    if (persons is not null)
    {
        foreach (var p in persons)
        {
            if (p.LastName == lastName)
            {
                person = p;
                return true;
            }
        }
    }

    person = null;
    return false;
}

public class Person(string lastName, string? firstName = null)
{
    public string? FirstName { get; set; } = firstName;

    public string LastName { get; set; } = lastName ?? throw new ArgumentNullException(nameof(lastName));
}