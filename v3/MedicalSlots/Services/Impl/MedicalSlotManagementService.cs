using MongoDB.Driver;
using v3.Context;
using v3.Doctors.Utils;
using v3.MedicalSlots.Domain;
using v3.MedicalSlots.Exceptions;
using v3.MedicalSlots.Services.Interfaces;
using v3.MedicalSlots.Utils;

namespace v3.MedicalSlots.Services.Impl;

public class MedicalSlotManagementService(
    MongoDbContext context,
    DoctorFinder doctorFinder,
    MedicalSlotFinder medicalSlotFinder
): IMedicalSlotManagementService {
    
    public async Task Complete(string medicalLicenseNumber, string medicalSlotId)
    {
        var doctor = await doctorFinder.FindByMedicalLicenceNumber(medicalLicenseNumber);
        var medialSlot = await medicalSlotFinder.FindById(medicalSlotId);
        var doctorFilter = Builders<MedicalSlot>.Filter.Eq(x => x.Id, doctor.Id);
        
        if (await IsDoctorNotAssociatedWithMedicalSlot(doctor.Id))
        {
            throw new InaccessibleMedicalSlotException(medicalLicenseNumber);
        }
        
        if (await IsMedicalSlotCanceled(doctor.Id, medialSlot.AvailableAt))
        {
            var message = $"Medical slot whose id is {medialSlot.Id} is already canceled.";
            throw new ImmutableMedicalSlotException(message);
        }
        
        if (await IsMedicalSlotCompleted(doctor.Id, medialSlot.AvailableAt))
        {
            var message = $"Medical slot whose id is {medialSlot.Id} is already completed.";
            throw new ImmutableMedicalSlotException(message);
        }
        
        var update = Builders<MedicalSlot>.Update.Set(x => x.CompletedAt, DateTime.Now);
        await context.MedicalSlotCollection.UpdateOneAsync(doctorFilter, update);
    }
    
        public async Task Cancel(string medicalLicenseNumber, string medicalSlotId)
    {
        var doctor = await doctorFinder.FindByMedicalLicenceNumber(medicalLicenseNumber);
        var medialSlot = await medicalSlotFinder.FindById(medicalSlotId);
        var doctorFilter = Builders<MedicalSlot>.Filter.Eq(x => x.Id, doctor.Id);
        
        if (await IsDoctorNotAssociatedWithMedicalSlot(doctor.Id))
        {
            throw new InaccessibleMedicalSlotException(medicalLicenseNumber);
        }
        
        if (await IsMedicalSlotCanceled(doctor.Id, medialSlot.AvailableAt))
        {
            var message = $"Medical slot whose id is {medialSlot.Id} is already canceled.";
            throw new ImmutableMedicalSlotException(message);
        }
        
        if (await IsMedicalSlotCompleted(doctor.Id, medialSlot.AvailableAt))
        {
            var message = $"Medical slot whose id is {medialSlot.Id} is already completed.";
            throw new ImmutableMedicalSlotException(message);
        }
        
        var update = Builders<MedicalSlot>.Update.Set(x => x.CanceledAt, DateTime.Now);
        await context.MedicalSlotCollection.UpdateOneAsync(doctorFilter, update);
    }

    private async Task<bool> IsDoctorNotAssociatedWithMedicalSlot(Guid doctorId)
    {
        var doctorFilter = Builders<MedicalSlot>.Filter.Eq(x => x.Id, doctorId);
        return !await context
            .MedicalSlotCollection
            .FindAsync(doctorFilter)
            .Result
            .AnyAsync();
    }
        
    private async Task<bool> IsMedicalSlotCanceled(Guid doctorId, DateTime availableAt)
    {
        var filter = Builders<MedicalSlot>.Filter.Eq(x => x.Id, doctorId) &
                                        Builders<MedicalSlot>.Filter.Eq(x => x.AvailableAt, availableAt) &
                                        Builders<MedicalSlot>.Filter.Eq(x => x.CanceledAt != null, true) &
                                        Builders<MedicalSlot>.Filter.Eq(x => x.CompletedAt == null, true);
        return await context
            .MedicalSlotCollection
            .FindAsync(filter)
            .Result
            .AnyAsync();
    }
    
    private async Task<bool> IsMedicalSlotCompleted(Guid doctorId, DateTime availableAt)
    {
        var filter = Builders<MedicalSlot>.Filter.Eq(x => x.Id, doctorId) &
                     Builders<MedicalSlot>.Filter.Eq(x => x.AvailableAt, availableAt) &
                     Builders<MedicalSlot>.Filter.Eq(x => x.CanceledAt == null, true) &
                     Builders<MedicalSlot>.Filter.Eq(x => x.CompletedAt != null, true);
        return await context
            .MedicalSlotCollection
            .FindAsync(filter)
            .Result
            .AnyAsync();
    }
}