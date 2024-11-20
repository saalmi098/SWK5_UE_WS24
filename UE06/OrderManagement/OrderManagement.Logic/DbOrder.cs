using OrderManagement.Domain;

namespace OrderManagement.Logic;

internal class DbOrder
{
  public DbOrder(Guid id, string article, DateTimeOffset orderDate, decimal totalPrice, Guid customerId)
  {
    Id = id;
    Article = article;
    OrderDate = orderDate;
    TotalPrice = totalPrice;
    CustomerId = customerId;
  }

  public Guid Id { get; set; }

  public string Article { get; set; }

  public DateTimeOffset OrderDate { get; set; }

  public decimal TotalPrice { get; set; }

  public Guid CustomerId { get; set; }

  public Order ToOrder(Customer customer)
  {
    var order = new Order(Id, Article, OrderDate, TotalPrice, customer);
    order.Customer = customer;
    return order;
  }
}

internal static class OrderExtensions
{
  public static DbOrder ToDbOrder(this Order order) => new DbOrder(
    order.Id,
    order.Article,
    order.OrderDate,
    order.TotalPrice,
    order.Customer is null ? Guid.Empty : order.Customer.Id);
}


