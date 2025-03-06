using System.ComponentModel.DataAnnotations;
using v3.People.DTOs;

namespace v3.Doctors.DTOs;

public record DoctorHiringDto(
    [Required] string MedicalLicenseNumber,
    [Required] PersonRegistrationDto PersonRegistrationDto
);