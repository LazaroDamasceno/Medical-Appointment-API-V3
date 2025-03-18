using v3.MedicalSlots.DTOs;

namespace v3.MedicalSlots.Services.Interfaces;

public interface IMedicalSlotRegistrationService
{
    Task<MedicalSlotResponseDto> Register(MedicalSlotRegistrationDto registrationDto);
}