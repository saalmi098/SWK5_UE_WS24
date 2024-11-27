using OrderManagement.Api.Dtos;
using OrderManagement.Domain;

namespace OrderManagement.Api.Mapping;

public static class CustomerMapper
{
    // Auskommentiert wegen Mapperly
    //public static CustomerDto ToCustomerDto(this Customer customer)
    //{
    //    return new CustomerDto
    //    {
    //        Id = customer.Id,
    //        Name = customer.Name,
    //        ZipCode = customer.ZipCode,
    //        City = customer.City,
    //        Rating = customer.Rating,
    //        TotalRevenue = customer.TotalRevenue
    //    };
    //}

    public static Customer ToCustomer(this CustomerDto customerDto)
    {
        return new Customer(
            customerDto.Id,
            customerDto.Name,
            customerDto.ZipCode,
            customerDto.City,
            customerDto.Rating
        );
    }
}
