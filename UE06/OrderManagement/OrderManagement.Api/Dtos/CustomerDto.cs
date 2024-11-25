using OrderManagement.Domain;
using System.Text.Json.Serialization;

namespace OrderManagement.Api.Dtos;

public class CustomerDto
{
    // [JsonRequired] -> ersetzt durch required
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required int ZipCode { get; set; }

    //[JsonPropertyName("location")]
    public required string City { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required Rating Rating { get; set; }

    //[JsonIgnore]
    public decimal TotalRevenue { get; set; }
}