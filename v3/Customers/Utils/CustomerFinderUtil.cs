using MongoDB.Bson;
using MongoDB.Driver;
using v3.Context;
using v3.Customers.Domain;
using v3.Customers.Exceptions;

namespace v3.Customers.Utils;

public class CustomerFinderUtil(MongoDbContext context)
{
    public async Task<Customer> FindByIdAsync(string customerId)
    {
        var filter = Builders<Customer>.Filter.Eq(x => x.Id, Guid.Parse(customerId));
        var foundCustomer = await context
            .CustomersCollection
            .FindAsync(filter)
            .Result
            .FirstOrDefaultAsync();
        if (foundCustomer == null) throw new NonExistentCustomerException(customerId);
        return foundCustomer;
    }
}