using MongoDB.Driver;
using v3.Context;
using v3.Customers.Domain;
using v3.Customers.Exceptions;

namespace v3.Customers.Utils;

public class CustomerFinder(MongoDbContext context)
{
    
    public async Task<Customer> FindByIdAsync(string customerId)
    {
        var filter = Builders<Customer>.Filter.Eq(x => x.Id, customerId);
        var foundCustomer = await context
            .CustomersCollection
            .Find(filter)
            .FirstOrDefaultAsync();
        if (foundCustomer == null) throw new NonExistentCustomerException(customerId);
        return foundCustomer;
    }
}