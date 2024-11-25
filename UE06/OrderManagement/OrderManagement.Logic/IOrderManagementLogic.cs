using OrderManagement.Domain;

namespace OrderManagement.Logic;

public interface IOrderManagementLogic
{
  Task<IEnumerable<Customer>> GetCustomersAsync();
  Task<IEnumerable<Customer>> GetCustomersByRatingAsync(Rating rating);
  Task<bool> CustomerExistsAsync(Guid customerId);
  Task<Customer?> GetCustomerAsync(Guid customerId);
  Task AddCustomerAsync(Customer customer);
  Task<bool> DeleteCustomerAsync(Guid customerId);
  Task UpdateCustomerAsync(Customer customer);

  Task<IEnumerable<Order>> GetOrdersOfCustomerAsync(Guid customerId);
  Task<bool> OrderExistsAsync(Guid order);
  Task<Order> GetOrderAsync(Guid orderId);
  Task AddOrderForCustomerAsync(Guid customerId, Order order);
  Task UpdateOrderAsync(Order order);
  Task<bool> DeleteOrderAsync(Guid orderId);

  Task<decimal> UpdateTotalRevenueAsync(Guid customerId);
	Task<decimal> UpdateTotalRevenuesAsync();
}
