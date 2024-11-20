using OrderManagement.Api.Dtos;
using OrderManagement.Domain;

namespace OrderManagement.Api.Mapping;

public static class OrderMapper
{
    public static OrderDto ToOrderDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            Article = order.Article,
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice
        };
    }
}
