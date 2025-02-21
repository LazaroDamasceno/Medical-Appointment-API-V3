using System.ComponentModel.DataAnnotations;

namespace v3.Common;

public record Address(
    [Required]
    string Street,
    [Required]
    string City,
    [Required]
    string State,
    [Required]
    string PostalCode
);