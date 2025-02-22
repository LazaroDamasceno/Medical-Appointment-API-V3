using System.ComponentModel.DataAnnotations;

namespace v3.Customers.DTOs;

public record CustomerResponseDto(
    [Required] string Id, 
    [Required] string FullName, 
    [Required] string Address
);