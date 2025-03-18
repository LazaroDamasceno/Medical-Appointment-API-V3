using MongoDB.Driver;
using v3.Context;
using v3.People.Domain;

namespace v3.Common;

public class PersonalDataChecker(MongoDbContext context)
{
    
    public async Task<bool> IsSsnDuplicated(string ssn)
    {
        var filter = Builders<Person>.Filter.Eq(x => x.Ssn, ssn);
        return await context
            .PeopleCollection
            .Find(filter)
            .AnyAsync();
    }
    
    public async Task<bool> IsEmailDuplicated(string email)
    {
        var filter = Builders<Person>.Filter.Eq(x => x.Email, email);
        return await context
            .PeopleCollection
            .Find(filter)
            .AnyAsync();
    }
}