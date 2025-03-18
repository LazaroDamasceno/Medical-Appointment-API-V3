using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;
using v3.Common;
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
    IPersonRegistrationService personRegistrationService,
    PersonalDataChecker personalDataChecker
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
        if (await personalDataChecker.IsSsnDuplicated(ssn))
        {
            throw new DuplicatedSsnException();
        }

        if (await personalDataChecker.IsEmailDuplicated(email))
        {
            throw new DuplicatedEmailException();
        }
    }
}