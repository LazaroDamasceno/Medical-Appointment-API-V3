using System.ComponentModel.DataAnnotations;
using v3.Common;

namespace v3.Customers.DTOs;

public record CustomerResponseDto(
    [Required] string Id, 
    [Required] string FullName, 
    [Required] string Address
);