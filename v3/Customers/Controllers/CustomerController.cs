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
    public async Task<ActionResult<CustomerResponseDto>> Create([Required] [FromBody] CustomerRegistrationDto registrationDto)
    {
        var response = await registrationService.Create(registrationDto);
        return StatusCode(201, response);
    }

    [HttpGet]
    public async Task<List<CustomerResponseDto>> GetAllAsync()
    {
        return await retrievalService.GetAllAsync();
    }

    [HttpGet("{customerId}")]
    public async Task<CustomerResponseDto> GetByIdAsync(string customerId)
    {
        return await retrievalService.GetByIdAsync(customerId);
    }
}