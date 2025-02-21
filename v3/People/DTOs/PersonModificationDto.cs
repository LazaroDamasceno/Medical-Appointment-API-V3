using System.ComponentModel.DataAnnotations;

namespace v3.People.DTOs;

public record PersonRegistrationDto(
    [Required]
    string FirstName,
    string MiddleName,
    [Required]
    string LastName,
    [Required, StringLength(9)]
    string Ssn,
    [Required, EmailAddress]
    string Email,
    [Required, StringLength(10)]
    string PhoneNumber,
    [Required, MinLength(1)]
    string Gender
);