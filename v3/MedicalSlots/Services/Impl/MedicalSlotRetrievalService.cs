using MongoDB.Driver;
using v3.Context;
using v3.Doctors.Utils;
using v3.MedicalSlots.Domain;
using v3.MedicalSlots.DTOs;
using v3.MedicalSlots.Exceptions;
using v3.MedicalSlots.Services.Interfaces;
using v3.MedicalSlots.Utils;

namespace v3.MedicalSlots.Services.Impl;

public class MedicalSlotRetrievalService(
    MongoDbContext context, 
    DoctorFinder doctorFinder,
    MedicalSlotFinder medicalSlotFinder
): IMedicalSlotRetrievalService {
    
    public async Task<MedicalSlotResponseDto> FindById(string medicalSlotId)
    {
        var medicalSlot = await medicalSlotFinder.FindById(medicalSlotId);
        return MedicalSlotResponseMapper.Map(medicalSlot);
    }

    public async Task<MedicalSlotResponseDto> FindById(string medicalLicenseNumber, string medicalSlotId)
    {
        var doctor = await doctorFinder.FindByMedicalLicenceNumber(medicalSlotId);
        var medicalSlot = await medicalSlotFinder.FindById(medicalSlotId);
        if (medicalSlot.Doctor.Id != doctor.Id)
        {
            throw new InaccessibleMedicalSlotException(doctor.MedicalLicenseNumber);
        }
        return MedicalSlotResponseMapper.Map(medicalSlot);
    }

    public async Task<List<MedicalSlotResponseDto>> FindAll()
    {
        var all = await context
            .MedicalSlotCollection
            .Find(_ => true)
            .ToListAsync();
        return all.Select(MedicalSlotResponseMapper.Map).ToList();
    }
}