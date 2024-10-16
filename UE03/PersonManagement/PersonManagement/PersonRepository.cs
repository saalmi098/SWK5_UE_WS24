namespace PersonManagement;

public class PersonRepository
{
    private readonly IList<Person> persons = new List<Person>();

    public void AddPerson(Person person)
    {
        persons.Add(person);
    }

    public void AddPersons(IEnumerable<Person> persons)
    {
        this.persons.AddAll(persons); // Extension method
    }

    public void PrintPersons(TextWriter textWriter)
    {
        //foreach (var p in persons)
        //{
        //    textWriter.WriteLine(p);
        //}

        //persons.ForEach(p => textWriter.WriteLine(p)); // Extension method
        persons.ForEach(textWriter.WriteLine); // Kurzschreibweise
    }

    public IEnumerable<(string?, string?)> GetPersonNames()
    {
        return null;
    }

    public IEnumerable<Person> FindPersonsByCity(string city)
    {
        return persons.Filter(p => p.City == city);
    }

    public Person FindYoungestPerson()
    {
        return null;
    }


    public IEnumerable<Person> FindPersonsSortedByAgeAscending()
    {
        return null;
    }
}
