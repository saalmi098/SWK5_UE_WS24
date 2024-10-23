namespace LinqSamples.Data;

public class Customer
{
    public string Name { get; set; }

    public decimal Revenue { get; set; }

    public int YearOfFoundation { get; set; }

    public Address Location { get; set; }

    public Rating Rating { get; set; }

    public override string ToString()
    {
        return $"{Name}, {Location.City} ({Location.Country}): {Revenue:N0} Euro, found. {YearOfFoundation}, Rating: {Rating}";
    }
}