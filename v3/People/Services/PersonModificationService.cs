using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;
using v3.Context;
using v3.People.Domain;
using v3.People.DTOs;

namespace v3.People.Services;

public class PersonModificationService(MongoDbContext context): IPersonModificationService
{
    public async Task<Person> ModifyAsync([Required] Person person, [Required] PersonModificationDto modificationDto)
    {
        person.Modify(modificationDto);
        var filter = Builders<Person>.Filter.Eq(x => x.Id, person.Id);
        await context.PeopleCollection.ReplaceOneAsync(filter, person);
        return person;
    }
}