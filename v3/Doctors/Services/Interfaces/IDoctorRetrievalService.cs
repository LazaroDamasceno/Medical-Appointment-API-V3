using v3.Doctors.Domain;
using v3.Doctors.DTOs;

namespace v3.Doctors.Services.Interfaces;

public interface IDoctorRetrievalService
{
    Task<List<DoctorResponseDto>> GetAll();
    Task<DoctorResponseDto> GetByMedicalLicenseNumber(string medicalLicenseNumber);
}