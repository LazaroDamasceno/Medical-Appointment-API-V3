using v3.Customers.DTOs;

namespace v3.Customers.Services.Interfaces;

public interface ICustomerRegistrationService
{
    Task<CustomerResponseDto> Create(CustomerRegistrationDto registrationDto);
}