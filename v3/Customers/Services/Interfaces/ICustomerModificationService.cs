using v3.Customers.DTOs;

namespace v3.Customers.Services.Interfaces;

public interface ICustomerModificationService
{
    Task ModifyAsync(string customerId, CustomerModificationDto modificationDto);
}