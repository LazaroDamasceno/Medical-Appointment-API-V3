using v3.MedicalSlots.DTOs;

namespace v3.MedicalSlots.Services.Interfaces;

public interface IMedicalSlotRetrievalService
{
    Task<MedicalSlotResponseDto> FindById(string medicalSlotId);
    Task<MedicalSlotResponseDto> FindById(string medicalLicenseNumber, string medicalSlotId);
    Task<IEnumerable<MedicalSlotResponseDto>> FindAll();
    Task<IEnumerable<MedicalSlotResponseDto>> FindAll(string medicalLicenseNumber);
}