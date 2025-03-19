using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using v3.Customers.DTOs;
using v3.Customers.Services.Interfaces;

namespace v3.Customers.Controllers;

[ApiController]
[Route("api/v3/customers")]
public class CustomerController(
    ICustomerRegistrationService registrationService,
    ICustomerRetrievalService retrievalService
): ControllerBase {
    
    [HttpPost]
    public async Task<CustomerResponseDto> Create([Required] [FromBody] CustomerRegistrationDto registrationDto)
    {
        return await registrationService.Create(registrationDto);
    }

    [HttpGet]
    public async Task<IEnumerable<CustomerResponseDto>> GetAll()
    {
        return await retrievalService.GetAll();
    }

    [HttpGet("{customerId}")]
    public async Task<CustomerResponseDto> GetById(string customerId)
    {
        return await retrievalService.GetById(customerId);
    }
}