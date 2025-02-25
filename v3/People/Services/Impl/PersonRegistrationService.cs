using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;
using v3.Context;
using v3.People.Domain;
using v3.People.DTOs;
using v3.People.Services.Interfaces;

namespace v3.People.Services.Impl;

public class PersonRegistrationService(MongoDbContext context) : IPersonRegistrationService
{
    
    public async Task<Person> Create([Required] PersonRegistrationDto registrationDto)
    {
        var filter = Builders<Person>.Filter.Eq(x => x.Ssn, registrationDto.Ssn);
        var foundPerson = await context.PeopleCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        if (foundPerson == null)
        {
            var newPerson = Person.Create(registrationDto);
            await context.PeopleCollection.InsertOneAsync(newPerson);
            return newPerson;
        }
        return foundPerson;
    }
}