using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using v3.Customers.DTOs;
using v3.Customers.Services;
using v3.Customers.Services.Interfaces;

namespace v3.Customers.Controllers;

[ApiController]
[Route("api/v3/customers")]
public class CustomerController(
    ICustomerRegistrationService registrationService,
    ICustomerRetrievalService retrievalService,
    ICustomerModificationService modificationService
): ControllerBase {
    
    [HttpPost]
    public Task<CustomerResponseDto> Create([Required] [FromBody] CustomerRegistrationDto registrationDto)
    {
        return registrationService.Create(registrationDto);
    }

    [HttpGet]
    public Task<List<CustomerResponseDto>> GetAllAsync()
    {
        return retrievalService.GetAllAsync();
    }

    [HttpGet("{customerId}")]
    public Task<CustomerResponseDto> GetByIdAsync(string customerId)
    {
        return retrievalService.GetByIdAsync(customerId);
    }

    public Task ModifyAsync(string customerId, [Required] [FromBody] CustomerModificationDto modificationDto)
    {
        return modificationService.ModifyAsync(customerId, modificationDto);
    }
}