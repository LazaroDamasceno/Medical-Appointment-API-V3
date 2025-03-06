namespace v3.Doctors.DTOs;

public record DoctorResponseDto(
    string MedicalLicenseNumber,
    string FullName
);