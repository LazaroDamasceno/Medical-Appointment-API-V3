using v3.Customers.DTOs;

namespace v3.Customers.Services.Interfaces;

public interface ICustomerRetrievalService
{
    Task<List<CustomerResponseDto>> GetAllAsync();
    Task<CustomerResponseDto> GetByIdAsync(string customerId);
}