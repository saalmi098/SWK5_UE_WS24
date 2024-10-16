using PersonManagement;
using System.Text.Json;

PersonRepository personRepository = new PersonRepository();
IEnumerable<Person>? persons = new List<Person>();

try
{
	string json = File.ReadAllText("persons.json");
    JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
	persons = JsonSerializer.Deserialize<IEnumerable<Person>>(json, options);

	if (persons is null)
	{
		throw new Exception("Persons deserialization failed");
    }
}
catch (FileNotFoundException fnfEx)
{
	Console.WriteLine(fnfEx.Message);
	return;
}

personRepository.AddPersons(persons);

// Console.WriteLine ist eine Abkuerzung fuer Console.Out.WriteLine (analog dazu gibt es Console.Error und Console.In)
using TextWriter textWriter = Console.Out; // using fuer automatisches Disposen
//using TextWriter textWriter = new StreamWriter("result.txt");
textWriter.WriteLine("=====================================================");
textWriter.WriteLine("Person list");
textWriter.WriteLine("=====================================================");
personRepository.PrintPersons(textWriter);

textWriter.WriteLine();
textWriter.WriteLine("=====================================================");
textWriter.WriteLine("Persons in Hagenberg");
textWriter.WriteLine("=====================================================");
personRepository.FindPersonsByCity("Hagenberg").ForEach(textWriter.WriteLine);

//textWriter.WriteLine();
//textWriter.WriteLine("=====================================================");
//textWriter.WriteLine("Person names");
//textWriter.WriteLine("=====================================================");
//
// TODO
//

//textWriter.WriteLine();
//textWriter.WriteLine("=====================================================");
//textWriter.WriteLine($"Youngest person");
//textWriter.WriteLine("=====================================================");
//
// TODO
//

//textWriter.WriteLine();
//textWriter.WriteLine("=====================================================");
//textWriter.WriteLine("Persons sorted by age ascending");
//textWriter.WriteLine("=====================================================");
//
// TODO
//
