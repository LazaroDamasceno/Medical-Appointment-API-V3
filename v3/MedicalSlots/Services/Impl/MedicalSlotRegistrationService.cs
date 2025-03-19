using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;
using v3.Common;
using v3.Context;
using v3.Doctors.Utils;
using v3.MedicalSlots.Domain;
using v3.MedicalSlots.DTOs;
using v3.MedicalSlots.Services.Interfaces;
using v3.MedicalSlots.Utils;

namespace v3.MedicalSlots.Services.Impl;

public class MedicalSlotRegistrationService(
    MongoDbContext context,
    DoctorFinder doctorFinder
): IMedicalSlotRegistrationService
{
    
    public async Task<MedicalSlotResponseDto> Register([Required] MedicalSlotRegistrationDto registrationDto)
    {
        var doctor = await doctorFinder.FindByMedicalLicenceNumber(registrationDto.MedicalLicenseNumber);
        await ValidateRegistration(doctor.Id, registrationDto.AvailableAt);
        var medicalSlot = MedicalSlot.Create(doctor, registrationDto.AvailableAt);
        await context.MedicalSlotCollection.InsertOneAsync(medicalSlot);
        return MedicalSlotResponseMapper.Map(medicalSlot);
    }

    private async Task ValidateRegistration(string doctorId, DateTime availableAt)
    {
        PastDateTimeHandler.Handle(availableAt);
        
        if (await IsBookingDateTimeUnavailable(doctorId, availableAt))
        {
            throw new UnavaialbleBookingDateTimeException();
        }
    }

    private async Task<bool> IsBookingDateTimeUnavailable(string doctorId, DateTime availableAt)
    {
        var filter = Builders<MedicalSlot>.Filter.Eq(ms => ms.Doctor.Id, doctorId) &
                     Builders<MedicalSlot>.Filter.Eq(ms => ms.AvailableAt, availableAt) &
                     Builders<MedicalSlot>.Filter.Eq(ms => ms.CompletedAt, null) &
                     Builders<MedicalSlot>.Filter.Eq(ms => ms.CanceledAt, null);
        return await context.MedicalSlotCollection.Find(filter).AnyAsync(); 
    }
}