using OrderManagement.Domain;

namespace OrderManagement.Logic;

internal class DbCustomer
{
  public DbCustomer(Guid id, string name, int zipCode, string city, Rating rating, decimal totalRevenue)
  {
    Id = id;
    Name = name ?? throw new ArgumentNullException(nameof(name));
    ZipCode = zipCode;
    City = city ?? throw new ArgumentNullException(nameof(city));
    Rating = rating;
    TotalRevenue = totalRevenue;
  }

  public Guid Id { get; set; }

  public string Name { get; set; }

  public int ZipCode { get; set; }

  public string City { get; set; }

  public Rating Rating { get; set; }

  public decimal TotalRevenue { get; set; }

  public Customer ToCustomer()
  {
    var customer = new Customer(Id, Name, ZipCode, City, Rating);
    customer.TotalRevenue = this.TotalRevenue;
    return customer;
  }
}

internal static class CustomerExtensions
{
  public static DbCustomer ToDbCustomer(this Customer customer) => new DbCustomer(
    customer.Id,
    customer.Name,
    customer.ZipCode,
    customer.City,
    customer.Rating,
    customer.TotalRevenue);
}
