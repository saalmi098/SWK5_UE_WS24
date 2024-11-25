namespace OrderManagement.Api.Dtos;

public class OrderDto
{
    public required Guid Id { get; set; }

    public required string Article { get; set; }

    public required DateTimeOffset OrderDate { get; set; }

    public required decimal TotalPrice { get; set; }
}