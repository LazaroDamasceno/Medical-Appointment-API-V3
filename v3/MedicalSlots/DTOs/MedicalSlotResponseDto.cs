using v3.Doctors.DTOs;

namespace v3.MedicalSlots.DTOs;

public record MedicalSlotResponseDto(
    DoctorResponseDto Doctor,
    DateTime AvailableAt,
    DateTime? CanceledAt,
    DateTime? CompletedAt
);