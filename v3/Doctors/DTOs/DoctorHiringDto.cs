using v3.People.DTOs;

namespace v3.Doctors.DTOs;

public record DoctorHiringDto(
    string MedicalLicenseNumber,
    PersonRegistrationDto PersonRegistrationDto
);