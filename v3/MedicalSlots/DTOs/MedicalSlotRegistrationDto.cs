namespace v3.MedicalSlots.DTOs;

public record MedicalSlotRegistrationDto(
    string MedicalLicenseNumber,
    DateTime AvailableAt
);