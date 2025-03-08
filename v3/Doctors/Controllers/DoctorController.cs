using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using v3.Doctors.DTOs;
using v3.Doctors.Services.Interfaces;

namespace v3.Doctors.Controllers;

[ApiController]
[Route("api/v3/doctors")]
public class DoctorController(
    IDoctorHiringService hiringService,
    IDoctorRehiringService rehiringService,
    IDoctorTerminationService terminationService,
    IDoctorRetrievalService retrievalService 
) {
    
    public async Task<DoctorResponseDto> Hire([Required] DoctorHiringDto hiringDto)
    {
        return await hiringService.Hire(hiringDto);
    }

    public Task Rehire(string medicalLicenseNumber)
    {
        return rehiringService.Rehire(medicalLicenseNumber);
    }

    public Task Terminate(string medicalLicenseNumber)
    {
        return terminationService.Terminate(medicalLicenseNumber);
    }

    public Task<List<DoctorResponseDto>> GetAll()
    {
        return retrievalService.GetAll();
    }

    public Task<DoctorResponseDto> GetByMedicalLicenseNumber(string medicalLicenseNumber)
    {
        return retrievalService.GetByMedicalLicenseNumber(medicalLicenseNumber);
    }
}