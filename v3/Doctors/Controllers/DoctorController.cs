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
    
    [HttpPost]
    public async Task<DoctorResponseDto> Hire([Required] DoctorHiringDto hiringDto)
    {
        return await hiringService.Hire(hiringDto);
    }

    [HttpPatch("{medicalLicenseNumber}/rehiring")]
    public Task Rehire(string medicalLicenseNumber)
    {
        return rehiringService.Rehire(medicalLicenseNumber);
    }

    [HttpPatch("{medicalLicenseNumber}/termination")]
    public Task Terminate(string medicalLicenseNumber)
    {
        return terminationService.Terminate(medicalLicenseNumber);
    }

    [HttpGet]
    public Task<List<DoctorResponseDto>> GetAll()
    {
        return retrievalService.GetAll();
    }

    [HttpGet("{medicalLicenseNumber}")]
    public Task<DoctorResponseDto> GetByMedicalLicenseNumber(string medicalLicenseNumber)
    {
        return retrievalService.GetByMedicalLicenseNumber(medicalLicenseNumber);
    }
}