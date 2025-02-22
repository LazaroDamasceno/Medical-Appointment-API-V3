using MongoDB.Driver;
using v3.Context;
using v3.Customers.DTOs;
using v3.Customers.Services.Interfaces;
using v3.Customers.Utils;

namespace v3.Customers.Services.Impl;

public class CustomerRetrievalService(
    MongoDbContext context,
    CustomerFinderUtil customerFinderUtil
): ICustomerRetrievalService {
    
    public async Task<List<CustomerResponseDto>> GetAllAsync()
    {
        var allCustomers = await context
            .CustomersCollection
            .FindAsync(_ => true)
            .Result
            .ToListAsync();
        return CustomerResponseMapper.MapToList(allCustomers);
    }

    public async Task<CustomerResponseDto> GetByIdAsync(string customerId)
    {
        var foundCustomer = await customerFinderUtil.FindByIdAsync(customerId);
        return CustomerResponseMapper.MapToDto(foundCustomer);
    }
}