using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.Api;

public static class StatusInfo
{
    // ProblemDetails ... standardisiertes Format, um Fehlermeldungen zu kommunizieren

    public static ProblemDetails CustomerAlreadyExists(Guid customerId) => new ProblemDetails
    {
        Title = "Conflicting customer IDs",
        Detail = $"Customer with ID {customerId} already exists",
    };

    public static ProblemDetails InvalidCustomerId(Guid customerId) => new ProblemDetails
    {
        Title = "Invalid customer ID",
        Detail = $"Customer with ID {customerId} does not exist",
    };

    public static ProblemDetails UpdateCustomerTotalsCancelled() => new ProblemDetails()
    {
        Title = "Operation cancelled",
        Detail = "Update customer totals cancelled"
    };
}
