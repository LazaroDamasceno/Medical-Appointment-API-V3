using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;
using v3.Context;
using v3.Customers.Domain;
using v3.Customers.DTOs;
using v3.Customers.Services.Interfaces;
using v3.Customers.Utils;
using v3.People.Exceptions;
using v3.People.Services.Interfaces;

namespace v3.Customers.Services.Impl;

public class CustomerRegistrationService(
    MongoDbContext context,
    IPersonRegistrationService personRegistrationService
    ): ICustomerRegistrationService {
    
    public async Task<CustomerResponseDto> Create([Required] CustomerRegistrationDto registrationDto)
    {
        await ValidateHiring(
            registrationDto.PersonRegistrationDto.Ssn,
            registrationDto.PersonRegistrationDto.Email
        );
        var person = personRegistrationService.Create(registrationDto.PersonRegistrationDto).Result;
        var customer = Customer.Create(registrationDto.Address, person);
        await context.CustomersCollection.InsertOneAsync(customer);
        return CustomerResponseMapper.MapToDto(customer);
    }
    
    private async Task ValidateHiring(string ssn, string email)
    {
        if (await IsSsnDuplicated(ssn))
        {
            throw new DuplicatedSsnException();
        }

        if (await IsEmailDuplicated(email))
        {
            throw new DuplicatedEmailException();
        }
    }

    private async Task<bool> IsSsnDuplicated(string ssn)
    {
        var filter = Builders<Customer>.Filter.Eq(x => x.Person.Ssn, ssn);
        return await context.CustomersCollection.Find(filter).AnyAsync();
    }
    
    private async Task<bool> IsEmailDuplicated(string email)
    {
        var filter = Builders<Customer>.Filter.Eq(x => x.Person.Email, email);
        return await context.CustomersCollection.Find(filter).AnyAsync();
    }
    
}