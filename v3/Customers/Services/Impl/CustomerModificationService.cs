using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Driver;
using v3.Context;
using v3.Customers.Domain;
using v3.Customers.DTOs;
using v3.Customers.Services.Interfaces;
using v3.Customers.Utils;
using v3.People.Services;
using v3.People.Services.Interfaces;

namespace v3.Customers.Services.Impl;

public class CustomerModificationService(
    MongoDbContext context,
    CustomerFinderUtil customerFinderUtil,
    IPersonModificationService personModificationService
): ICustomerModificationService {
    public async Task ModifyAsync(string customerId, [Required] CustomerModificationDto modificationDto)
    {
        var foundCustomer = await customerFinderUtil.FindByIdAsync(customerId);
        var modifiedPerson = personModificationService.ModifyAsync(foundCustomer.Person, modificationDto.PersonModificationDto).Result;
        foundCustomer.Modify(modifiedPerson, modificationDto.Address);
        var filter = Builders<Customer>.Filter.Eq(x => x.Id, new ObjectId(customerId));
        await context.CustomersCollection.ReplaceOneAsync(filter, foundCustomer);
    }
}