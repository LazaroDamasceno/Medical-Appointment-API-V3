using MongoDB.Driver;
using v3.Context;
using v3.Doctors.Utils;
using v3.MedicalSlots.Domain;
using v3.MedicalSlots.Exceptions;
using v3.MedicalSlots.Services.Interfaces;
using v3.MedicalSlots.Utils;

namespace v3.MedicalSlots.Services.Impl;

public class MedicalSlotCancellationService(
    MongoDbContext context,
    DoctorFinder doctorFinder,
    MedicalSlotFinder medicalSlotFinder
): IMedicalSlotCancellationService
{
    
    public async Task Cancel(string medicalLicenseNumber, string medicalSlotId)
    {
        var doctor = await doctorFinder.FindByMedicalLicenceNumber(medicalLicenseNumber);
        var medialSlot = await medicalSlotFinder.FindById(medicalSlotId);
        
        var filter = Builders<MedicalSlot>.Filter.Eq(x => x.Id, doctor.Id);
        var isDoctorNotAssociatedWithMedicalSlot = !await context
            .MedicalSlotCollection
            .FindAsync(filter)
            .Result
            .AnyAsync();
        if (isDoctorNotAssociatedWithMedicalSlot)
        {
            const string message = "";
            throw new InaccessibleMedicalSlotException(medicalLicenseNumber);
        }
        
        filter = Builders<MedicalSlot>.Filter.Eq(x => x.Id, doctor.Id) &
                 Builders<MedicalSlot>.Filter.Eq(x => x.AvailableAt, medialSlot.AvailableAt) &
                 Builders<MedicalSlot>.Filter.Eq(x => x.CanceledAt != null, true) &
                 Builders<MedicalSlot>.Filter.Eq(x => x.CompletedAt == null, true);
        var isMedicalSlotCanceled = !await context
            .MedicalSlotCollection
            .FindAsync(filter)
            .Result
            .AnyAsync();
        if (isMedicalSlotCanceled)
        {
            const string message = "";
            throw new ImmutableMedicalSlotException(message);
        }
        
        filter = Builders<MedicalSlot>.Filter.Eq(x => x.Id, doctor.Id) &
                 Builders<MedicalSlot>.Filter.Eq(x => x.AvailableAt, medialSlot.AvailableAt) &
                 Builders<MedicalSlot>.Filter.Eq(x => x.CanceledAt == null, true) &
                 Builders<MedicalSlot>.Filter.Eq(x => x.CompletedAt != null, true);
        var isMedicalSlotCompleted = !await context
            .MedicalSlotCollection
            .FindAsync(filter)
            .Result
            .AnyAsync();
        if (isMedicalSlotCompleted)
        {
            const string message = "";
            throw new ImmutableMedicalSlotException(message);
        }
        
        filter = Builders<MedicalSlot>.Filter.Eq(x => x.Id, doctor.Id);
        var update = Builders<MedicalSlot>.Update.Set(x => x.CanceledAt, DateTime.Now);
        await context.MedicalSlotCollection.UpdateOneAsync(filter, update);
    }
}