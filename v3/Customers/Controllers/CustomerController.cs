using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using v3.Customers.DTOs;
using v3.Customers.Services;

namespace v3.Customers.Controllers;

[ApiController]
[Route("api/v3/customers")]
public class CustomerController(
    ICustomerRegistrationService registrationService
): ControllerBase {
    
    [HttpPost]
    public Task<CustomerResponseDto> Create([Required] [FromBody] CustomerRegistrationDto registrationDto)
    {
        return registrationService.Create(registrationDto);
    }
}