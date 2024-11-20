namespace OrderManagement.Domain;

public class Order(Guid id, string article, DateTimeOffset orderDate, decimal totalPrice, Customer? customer = null)
{
	public Guid Id { get; set; } = id;

	public string Article { get; set; } = article ?? throw new ArgumentNullException(nameof(article));

	public DateTimeOffset OrderDate { get; set; } = orderDate;

	public decimal TotalPrice { get; set; } = totalPrice;

	public Customer? Customer { get; set; } = customer;
}
