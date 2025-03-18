using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;
using v3.Common;
using v3.Context;
using v3.Doctors.Utils;
using v3.MedicalSlots.Domain;
using v3.MedicalSlots.DTOs;
using v3.MedicalSlots.Utils;

namespace v3.MedicalSlots.Services.Interfaces;

public class MedicalSlotRegistrationService(
    MongoDbContext context,
    DoctorFinder doctorFinder
): IMedicalSlotRegistrationService
{
    
    public async Task<MedicalSlotResponseDto> Register([Required] MedicalSlotRegistrationDto registrationDto)
    {
        var doctor = await doctorFinder.FindByMedicalLicenceNumber(registrationDto.MedicalLicenseNumber);
        
        var filter = Builders<MedicalSlot>.Filter.Eq(ms => ms.Doctor.Id, doctor.Id) &
                     Builders<MedicalSlot>.Filter.Eq(ms => ms.AvailableAt, registrationDto.AvailableAt) &
                     Builders<MedicalSlot>.Filter.Eq(ms => ms.CompletedAt, null) &
                     Builders<MedicalSlot>.Filter.Eq(ms => ms.CanceledAt, null);
        var isGivenDateTimeAlreadyInUse = await context.MedicalSlotCollection.FindAsync(filter).Result.AnyAsync(); 
        if (isGivenDateTimeAlreadyInUse) throw new BlockedBookingDateTimeException();

        BlockedDateTimeHandler.Handle(registrationDto.AvailableAt);
        
        var medicalSlot = MedicalSlot.Create(doctor, registrationDto.AvailableAt);
        await context.MedicalSlotCollection.InsertOneAsync(medicalSlot);
        return MedicalSlotResponseMapper.Map(medicalSlot);
    }
}