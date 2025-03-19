using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using v3.MedicalSlots.DTOs;
using v3.MedicalSlots.Services.Interfaces;

namespace v3.MedicalSlots.Controllers;

[ApiController]
[Route("api/v3/medical-slots")]
public class MedicalSlotController(
    IMedicalSlotRegistrationService registrationService,
    IMedicalSlotManagementService managementService,
    IMedicalSlotRetrievalService retrievalService
 ) : ControllerBase {
    
    [HttpPost]
    public async Task<MedicalSlotResponseDto> Register([Required] [FromBody] MedicalSlotRegistrationDto registrationDto)
    {
        return await registrationService.Register(registrationDto);
    }

    [HttpPatch("{medicalLicenseNumber}/{medicalSlotId}/cancellation")]
    public async Task<IActionResult> Cancel(string medicalLicenseNumber, string medicalSlotId)
    {
        await managementService.Cancel(medicalLicenseNumber, medicalSlotId);
        return NoContent();
    }
    
    [HttpPatch("{medicalLicenseNumber}/{medicalSlotId}/completion")]
    public async Task<IActionResult> Complete(string medicalLicenseNumber, string medicalSlotId)
    {
        await managementService.Complete(medicalLicenseNumber, medicalSlotId);
        return NoContent();
    }

    [HttpGet("by-id/{medicalSlotId}")]
    public async Task<MedicalSlotResponseDto> FindById(string medicalSlotId)
    {
        return await retrievalService.FindById(medicalSlotId);
    }

    [HttpGet("{medicalLicenseNumber}/{medicalSlotId}")]
    public async Task<MedicalSlotResponseDto> FindById(string medicalLicenseNumber, string medicalSlotId)
    {
        return await retrievalService.FindById(medicalLicenseNumber, medicalSlotId);
    }

    [HttpGet]
    public async Task<IEnumerable<MedicalSlotResponseDto>> FindAll()
    {
        return await retrievalService.FindAll();
    }
    
    [HttpGet("by-doctor/{medicalLicenseNumber}")]
    public async Task<IEnumerable<MedicalSlotResponseDto>> FindAll(string medicalLicenseNumber)
    {
        return await retrievalService.FindAll(medicalLicenseNumber);
    }
}