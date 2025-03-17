using v3.Doctors.Utils;
using v3.MedicalSlots.Domain;
using v3.MedicalSlots.DTOs;

namespace v3.MedicalSlots.Utils;

public static class MedicalSlotResponseMapper
{
    public static MedicalSlotResponseDto Map(MedicalSlot medicalSlot)
    {
        return new MedicalSlotResponseDto(
            DoctorResponseMapper.Map(medicalSlot.Doctor),
            medicalSlot.AvailableAt,
            medicalSlot.CanceledAt,
            medicalSlot.CompletedAt
        );
    }
}