using v3.Doctors.DTOs;

namespace v3.Doctors.Services.Interfaces;

public interface IDoctorRetrievalService
{
    Task<IEnumerable<DoctorResponseDto>> GetAll();
    Task<DoctorResponseDto> GetByMedicalLicenseNumber(string medicalLicenseNumber);
}