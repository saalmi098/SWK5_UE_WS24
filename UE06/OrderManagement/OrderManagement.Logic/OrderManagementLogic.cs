using OrderManagement.Domain;

namespace OrderManagement.Logic;

public class OrderManagementLogic : IOrderManagementLogic
{
	private const int PROCESSING_TIME_TOTAL_REVENUE_CUSTOMER = 2000;
	private const int PROCESSING_TIME_TOTAL_REVENUES = 2000;

	private static readonly SemaphoreSlim semaphore = new(1, 1);
	private static readonly Dictionary<Guid, DbCustomer> customers = [];
	private static readonly Dictionary<Guid, DbOrder> orders = [];

	private static async Task<T> RunInLockAsync<T>(Func<T> func)
	{
		await semaphore.WaitAsync();
		try
		{
			return func();
		}
		finally
		{
			semaphore.Release();
		}
	}

	private static async Task DoInLockAsync(Action action)
	{
		await semaphore.WaitAsync();
		try
		{
			action();
		}
		finally
		{
			semaphore.Release();
		}
	}

	private static DbCustomer EnsureCustomerExists(Guid customerId)
	{
		if (!customers.TryGetValue(customerId, out var dbCustomer))
		{
			throw new ArgumentException($"Customer with id {customerId} does not exist");
		}

		return dbCustomer;
	}

	private static DbOrder EnsureOrderExists(Guid orderId)
	{
		if (!orders.TryGetValue(orderId, out var dbOrder))
		{
			throw new ArgumentException($"Order with id {orderId} does not exist");
		}

		return dbOrder;
	}

	public async Task<bool> CustomerExistsAsync(Guid customerId)
	{
		return await RunInLockAsync(() => customers.TryGetValue(customerId, out var _));
	}

	public async Task<Customer?> GetCustomerAsync(Guid customerId)
	{
		return await RunInLockAsync(() =>
		{
			customers.TryGetValue(customerId, out var dbCustomer);
			return dbCustomer?.ToCustomer();
		});
	}

	public async Task AddCustomerAsync(Customer customer)
	{
		await DoInLockAsync(() =>
		{
			if (customer.Id == Guid.Empty)
			{
				customer.Id = Guid.NewGuid();
			}

			if (!customers.TryAdd(customer.Id, customer.ToDbCustomer()))
			{
				throw new ArgumentException($"Customer with id {customer.Id} already exists");
			}
		});
	}

	public async Task AddOrderForCustomerAsync(Guid customerId, Order order)
	{
		await DoInLockAsync(() =>
		{
			var dbCustomer = EnsureCustomerExists(customerId);

			if (order.Id == Guid.Empty)
			{
				order.Id = Guid.NewGuid();
			}
			order.Customer = dbCustomer.ToCustomer();
			orders.Add(order.Id, order.ToDbOrder());
		});
	}

	public async Task<bool> DeleteCustomerAsync(Guid customerId)
	{
		return await RunInLockAsync(() => customers.Remove(customerId));
	}

	public async Task<IEnumerable<Customer>> GetCustomersAsync()
	{
		return await RunInLockAsync(() => customers.Values.Select(c => c.ToCustomer()));
	}

	public async Task<IEnumerable<Customer>> GetCustomersByRatingAsync(Rating rating)
	{
		return await RunInLockAsync(() => customers.Values.Where(c => c.Rating == rating).Select(c => c.ToCustomer()));
	}

	public async Task UpdateCustomerAsync(Customer customer)
	{
		await DoInLockAsync(() =>
		{
			var dbCustomer = EnsureCustomerExists(customer.Id);
			dbCustomer.Name = customer.Name;
			dbCustomer.ZipCode = customer.ZipCode;
			dbCustomer.City = customer.City;
			dbCustomer.Rating = customer.Rating;
		});
	}

	public async Task<bool> OrderExistsAsync(Guid orderId)
	{
		return await RunInLockAsync(() => orders.TryGetValue(orderId, out var _));
	}

	public async Task<Order> GetOrderAsync(Guid orderId)
	{
		return await RunInLockAsync(() =>
		{
			var dbOrder = EnsureOrderExists(orderId);
			var customer = customers[dbOrder.CustomerId].ToCustomer();
			return dbOrder.ToOrder(customer);
		});
	}

	public async Task<IEnumerable<Order>> GetOrdersOfCustomerAsync(Guid customerId)
	{
		return await RunInLockAsync(() =>
		{
			var dbCustomer = EnsureCustomerExists(customerId);
			var customer = dbCustomer.ToCustomer();
			return orders.Values
				.Where(order => order.CustomerId == customerId)
				.Select(dbOrder => dbOrder.ToOrder(customer));
		});
	}

	public async Task UpdateOrderAsync(Order order)
	{
		await DoInLockAsync(() =>
		{
			var dbOrder = EnsureOrderExists(order.Id);
			dbOrder.Article = order.Article;
			dbOrder.OrderDate = order.OrderDate;
			dbOrder.TotalPrice = order.TotalPrice;
		});
	}

	public async Task<bool> DeleteOrderAsync(Guid orderId)
	{
		return await RunInLockAsync(() => orders.Remove(orderId));
	}

	private static decimal UpdateTotalRevenueInternal(DbCustomer customer)
	{
		var total = orders.Values.Where(c => c.CustomerId == customer.Id).Sum(order => order.TotalPrice);
		return customer.TotalRevenue = total;
	}

	public async Task<decimal> UpdateTotalRevenueAsync(Guid customerId)
	{
		decimal total = 0;
		await DoInLockAsync(() =>
		{
			var dbCustomer = EnsureCustomerExists(customerId);
			total = UpdateTotalRevenueInternal(dbCustomer);
		});

		await Task.Delay(PROCESSING_TIME_TOTAL_REVENUE_CUSTOMER); // simulate long processing time
		return total;
	}

	public async Task<decimal> UpdateTotalRevenuesAsync()
	{
		decimal total = 0;
		await DoInLockAsync(() =>
		{
			foreach (var customer in customers.Values)
			{
				total = UpdateTotalRevenueInternal(customer);
			}
		});

		await Task.Delay(PROCESSING_TIME_TOTAL_REVENUES); // simulate long processing time
		return total;
	}

	static OrderManagementLogic()
	{

		var customer1 = new Customer(new Guid("cccccccc-0000-0000-0000-111111111111"), "Stefan Mayr", 1010, "Wien", Rating.A);
		customers.Add(customer1.Id, customer1.ToDbCustomer());

		var customer2 = new Customer(new Guid("cccccccc-0000-0000-0000-222222222222"), "Susanne Huber", 4020, "Linz", Rating.B);
		customers.Add(customer2.Id, customer2.ToDbCustomer());

		var customer3 = new Customer(new Guid("cccccccc-0000-0000-0000-333333333333"), "Franz Himmelpfortsleitner", 6020, "Innsbruck", Rating.C);
		customers.Add(customer3.Id, customer3.ToDbCustomer());

		var customer4 = new Customer(new Guid("cccccccc-0000-0000-0000-444444444444"), "Maria Blümchen", 1010, "Wien", Rating.A);
		customers.Add(customer4.Id, customer4.ToDbCustomer());



		Order order;

		order = new(new Guid("aaaaaaaa-0000-0000-0000-000000111111"), "Dell Monitor", new DateTimeOffset(new DateTime(2021, 3, 4)), 528.3m)
		{
			Customer = customer1
		};
		orders.Add(order.Id, order.ToDbOrder());

		order = new(new Guid("aaaaaaaa-0000-0000-0000-000000222222"), "Saugroboter", new DateTimeOffset(new DateTime(2021, 5, 15)), 522.5m)
		{
			Customer = customer1
		};
		orders.Add(order.Id, order.ToDbOrder());

		order = new(new Guid("aaaaaaaa-0000-0000-0000-000000333333"), "Suface Book 3", new DateTimeOffset(new DateTime(2021, 12, 12)), 2258.3m)
		{
			Customer = customer1
		};
		orders.Add(order.Id, order.ToDbOrder());

		order = new(new Guid("aaaaaaaa-0000-0000-0000-000000444444"), "Apple Watch", new DateTimeOffset(new DateTime(2021, 6, 23)), 400.33m)
		{
			Customer = customer1
		};
		orders.Add(order.Id, order.ToDbOrder());

		order = new(new Guid("aaaaaaaa-0000-0000-0000-000000555555"), "Huawei P30", new DateTimeOffset(new DateTime(2021, 6, 23)), 238.5m)
		{
			Customer = customer2
		};
		orders.Add(order.Id, order.ToDbOrder());

		order = new(new Guid("aaaaaaaa-0000-0000-0000-000000666666"), "Nikkon D80", new DateTimeOffset(new DateTime(2021, 6, 23)), 855.3m)
		{
			Customer = customer2
		};
		orders.Add(order.Id, order.ToDbOrder());

		order = new(new Guid("aaaaaaaa-0000-0000-0000-000000777777"), "Blitzgerät", new DateTimeOffset(new DateTime(2021, 6, 23)), 88.3m)
		{
			Customer = customer3
		};
		orders.Add(order.Id, order.ToDbOrder());

		order = new(new Guid("aaaaaaaa-0000-0000-0000-000000888888"), "Braun Ohrthermometer", new DateTimeOffset(new DateTime(2021, 6, 23)), 45.25m)
		{
			Customer = customer4
		};
		orders.Add(order.Id, order.ToDbOrder());

		order = new(new Guid("aaaaaaaa-0000-0000-0000-000000999999"), "Sonos Bream 2", new DateTimeOffset(new DateTime(2021, 6, 23)), 483.99m)
		{
			Customer = customer4
		};
		orders.Add(order.Id, order.ToDbOrder());
	}
}
