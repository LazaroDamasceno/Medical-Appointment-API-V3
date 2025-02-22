using System.ComponentModel.DataAnnotations;
using v3.Common;
using v3.People.DTOs;

namespace v3.Customers.DTOs;

public record CustomerModificationDto(
    [Required] PersonModificationDto PersonModificationDto,
    [Required] Address Address
);