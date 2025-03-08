using v3.Doctors.DTOs;

namespace v3.Doctors.Services.Interfaces;

public interface IDoctorHiringService
{
    Task<DoctorResponseDto> Hire(DoctorHiringDto hiringDto);
}