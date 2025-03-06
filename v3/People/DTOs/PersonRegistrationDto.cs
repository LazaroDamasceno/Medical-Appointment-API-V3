using System.ComponentModel.DataAnnotations;
using v3.People.Enums;

namespace v3.People.DTOs;

public record PersonRegistrationDto(
    [Required]
    string FirstName,
    string MiddleName,
    [Required]
    string LastName,
    [Required]
    DateOnly BirthDate,
    [Required, StringLength(9)]
    string Ssn,
    [Required, EmailAddress]
    string Email,
    [Required, StringLength(10)]
    string PhoneNumber,
    [Required]
    Gender Gender
);