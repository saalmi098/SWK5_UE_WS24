namespace PersonManagement;

public class Person
{
    public uint Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? City { get; set; }

    public override string ToString()
    {
        return $"{Id} \t {FirstName} {LastName} \t {DateOfBirth.ToShortDateString()} \t {City}";
    }
}
