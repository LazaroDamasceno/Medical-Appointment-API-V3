using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;
using v3.Context;
using v3.Customers.Domain;
using v3.Customers.DTOs;
using v3.Customers.Services.Interfaces;
using v3.Customers.Utils;
using v3.People.Domain;
using v3.People.Exceptions;
using v3.People.Services;
using v3.People.Services.Interfaces;

namespace v3.Customers.Services.Impl;

public class CustomerRegistrationService(
    MongoDbContext context,
    IPersonRegistrationService personRegistrationService
): ICustomerRegistrationService {
    
    public async Task<CustomerResponseDto> Create([Required] CustomerRegistrationDto registrationDto)
    {
        OnDuplicatedSsn(registrationDto.PersonRegistrationDto.Ssn);
        OnDuplicatedEmail(registrationDto.PersonRegistrationDto.Email);
        var person = personRegistrationService.Create(registrationDto.PersonRegistrationDto).Result;
        var customer = Customer.Create(registrationDto.Address, person);
        await context.CustomersCollection.InsertOneAsync(customer);
        return CustomerResponseMapper.MapToDto(customer);
    }

    private void OnDuplicatedSsn(string ssn)
    {
        var filter = Builders<Person>.Filter.Eq(p => p.Ssn, ssn);
        var isDuplicated = context.PeopleCollection.Find(filter).Any();
        if (isDuplicated) throw new DuplicatedSsnException();
    }
    
    private void OnDuplicatedEmail(string email)
    {
        var filter = Builders<Person>.Filter.Eq(p => p.Email, email);
        var isDuplicated = context.PeopleCollection.Find(filter).Any();
        if (isDuplicated) throw new DuplicatedEmailException();
    }
}