using OrderManagement.Domain;
using System.Text.Json.Serialization;

namespace OrderManagement.Api.Dtos;

public record CustomerForUpdateDto
{
    // Guid rausgeloescht, da diese nicht veraendert werden darf
    // Restliche Properties duerfen veraendert werden

    public required string Name { get; set; }

    public required int ZipCode { get; set; }

    public required string City { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required Rating Rating { get; set; }
}
