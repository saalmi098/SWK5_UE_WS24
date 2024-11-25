using System.Runtime.Serialization;

namespace OrderManagement.Domain;

[DataContract]
public class Customer(Guid id, string name, int zipCode, string city, Rating rating)
{
    [DataMember]
    public Guid Id { get; set; } = id;

    [DataMember]
    public string Name { get; set; } = name ?? throw new ArgumentNullException(nameof(name));

    [DataMember]
    public int ZipCode { get; set; } = zipCode;

    [DataMember]
    public string City { get; set; } = city ?? throw new ArgumentNullException(nameof(city));

    [DataMember]
    public Rating Rating { get; set; } = rating;

    [DataMember]
    public decimal TotalRevenue { get; set; }
}