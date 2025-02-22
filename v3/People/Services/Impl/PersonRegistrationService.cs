using System.ComponentModel.DataAnnotations;
using v3.Context;
using v3.People.Domain;
using v3.People.DTOs;
using v3.People.Services.Interfaces;

namespace v3.People.Services.Impl;

public class PersonRegistrationService(MongoDbContext context) : IPersonRegistrationService
{
    
    public Person Create([Required] PersonRegistrationDto registrationDto)
    {
        var person = Person.Create(registrationDto);
        context.PeopleCollection.InsertOne(person);
        return person;
    }
}