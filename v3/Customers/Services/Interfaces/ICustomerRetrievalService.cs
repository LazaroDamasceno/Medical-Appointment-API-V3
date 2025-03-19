using v3.Customers.DTOs;

namespace v3.Customers.Services.Interfaces;

public interface ICustomerRetrievalService
{
    Task<List<CustomerResponseDto>> GetAll();
    Task<CustomerResponseDto> GetById(string customerId);
}