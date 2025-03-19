using MongoDB.Driver;
using v3.Context;
using v3.Customers.DTOs;
using v3.Customers.Services.Interfaces;
using v3.Customers.Utils;

namespace v3.Customers.Services.Impl;

public class CustomerRetrievalService(
    MongoDbContext context,
    CustomerFinder customerFinder
): ICustomerRetrievalService {
    
    public async Task<List<CustomerResponseDto>> GetAll()
    {
        var allCustomers = await context
            .CustomersCollection
            .Find(_ => true)
            .ToListAsync();
        return CustomerResponseMapper.MapToList(allCustomers);
    }

    public async Task<CustomerResponseDto> GetById(string customerId)
    {
        var foundCustomer = await customerFinder.FindByIdAsync(customerId);
        return CustomerResponseMapper.MapToDto(foundCustomer);
    }
}