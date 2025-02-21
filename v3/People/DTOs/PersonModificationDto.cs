using System.ComponentModel.DataAnnotations;

namespace v3.People.DTOs;

public record PersonModificationDto(
    [Required]
    string FirstName,
    string MiddleName,
    [Required]
    string LastName,
    [Required]
    DateOnly BirthDate,
    [Required, EmailAddress]
    string Email,
    [Required, StringLength(10)]
    string PhoneNumber,
    [Required, MinLength(1)]
    string Gender
);