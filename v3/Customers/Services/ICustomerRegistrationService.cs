using v3.Customers.DTOs;

namespace v3.Customers.Services;

public interface ICustomerRegistrationService
{
    Task<CustomerResponseDto> Create(CustomerRegistrationDto registrationDto);
}