using OrderManagement.Api.Dtos;
using OrderManagement.Domain;
using Riok.Mapperly.Abstractions;

namespace OrderManagement.Api.Mapperly;

// Package fuer "Riok.Mapperly" (NuGet Package) Deklarationen
// partial -> Deklaration befindet sich hier, Implementierung in anderer Datei (CustomerMapper.g.cs - wird von Mapperly generiert)
// (siehe Solution Explorer: OrderManagement.Api/Dependencies/Analyzers/Riok.Mapperly)
[Mapper]
public static partial class CustomerMapper
{
    public static partial CustomerDto ToCustomerDto(this Customer customer);
    public static partial IEnumerable<CustomerDto> ToCustomerDtoEnumeration(this IEnumerable<Customer> customers);

    [MapperIgnoreTarget(nameof(Customer.TotalRevenue))]
    public static partial Customer ToCustomer(this CustomerForCreationDto customer);

    [MapperIgnoreTarget(nameof(Customer.TotalRevenue))]
    public static partial void UpdateCustomer(this CustomerForUpdateDto customerForUpdateDto, Customer customer);
}
