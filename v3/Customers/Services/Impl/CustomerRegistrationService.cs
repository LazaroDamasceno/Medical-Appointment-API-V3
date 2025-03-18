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
        var ssn = registrationDto.PersonRegistrationDto.Ssn;
        if (await personalDataChecker.IsSsnDuplicated(ssn)) throw new DuplicatedSsnException();
        
        var email = registrationDto.PersonRegistrationDto.Email;
        if (await personalDataChecker.IsEmailDuplicated(ssn)) throw new DuplicatedEmailException();
        
        var person = personRegistrationService.Create(registrationDto.PersonRegistrationDto).Result;
        var customer = Customer.Create(registrationDto.Address, person);
        await context.CustomersCollection.InsertOneAsync(customer);
        return CustomerResponseMapper.MapToDto(customer);
    }
}