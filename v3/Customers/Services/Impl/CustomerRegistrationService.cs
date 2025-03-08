using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;
using v3.Context;
using v3.Customers.Domain;
using v3.Customers.DTOs;
using v3.Customers.Services.Interfaces;
using v3.Customers.Utils;
using v3.People.Domain;
using v3.People.Exceptions;
using v3.People.Services.Interfaces;

namespace v3.Customers.Services.Impl;

public class CustomerRegistrationService(
    MongoDbContext context,
    IPersonRegistrationService personRegistrationService
): ICustomerRegistrationService {
    
    public async Task<CustomerResponseDto> Create([Required] CustomerRegistrationDto registrationDto)
    {
        var ssnFilter = Builders<Person>.Filter.Eq(p => p.Ssn, registrationDto.PersonRegistrationDto.Ssn);
        var isSsnDuplicated = await context.PeopleCollection.FindAsync(ssnFilter).Result.AnyAsync();
        if (isSsnDuplicated) throw new DuplicatedSsnException();
        
        var emailFilter = Builders<Person>.Filter.Eq(p => p.Email, registrationDto.PersonRegistrationDto.Email);
        var isEmailDuplicated = await context.PeopleCollection.FindAsync(emailFilter).Result.AnyAsync();
        if (isEmailDuplicated) throw new DuplicatedEmailException();
        
        var person = personRegistrationService.Create(registrationDto.PersonRegistrationDto).Result;
        var customer = Customer.Create(registrationDto.Address, person);
        await context.CustomersCollection.InsertOneAsync(customer);
        return CustomerResponseMapper.MapToDto(customer);
    }
}