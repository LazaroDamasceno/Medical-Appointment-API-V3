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
        await ValidateMedicalSlot(doctor.Id, medialSlot, medicalLicenseNumber);
        var update = Builders<MedicalSlot>.Update.Set(x => x.CompletedAt, DateTime.Now);
        await context.MedicalSlotCollection.UpdateOneAsync(DoctorFilter(doctor.Id), update);
    }
    
    public async Task Cancel(string medicalLicenseNumber, string medicalSlotId)
    {
        var doctor = await doctorFinder.FindByMedicalLicenceNumber(medicalLicenseNumber);
        var medialSlot = await medicalSlotFinder.FindById(medicalSlotId);
        await ValidateMedicalSlot(doctor.Id, medialSlot, medicalLicenseNumber);
        var update = Builders<MedicalSlot>.Update.Set(x => x.CanceledAt, DateTime.Now);
        await context.MedicalSlotCollection.UpdateOneAsync(DoctorFilter(doctor.Id), update);
    }
    
    private async Task ValidateMedicalSlot(Guid doctorId, MedicalSlot medicalSlot, string medicalLicenseNumber)
    {
        if (!await IsDoctorNotAssociatedWithMedicalSlot(DoctorFilter(doctorId)))
        {
            throw new InaccessibleMedicalSlotException(medicalLicenseNumber);
        }

        if (await IsMedicalSlotCanceled(doctorId, medicalSlot.AvailableAt))
        {
            const string message = "Medical slot whose id is {id} is already canceled.";
            throw new ImmutableMedicalSlotException(message);
        }
        
        if (await IsMedicalSlotCompleted(doctorId, medicalSlot.AvailableAt))
        {
            const string message = "Medical slot whose id is {id} is already completed.";
            throw new ImmutableMedicalSlotException(message);
        }
    }

    private static FilterDefinition<MedicalSlot> DoctorFilter(Guid doctorId)
    {
        return Builders<MedicalSlot>.Filter.Eq(x => x.Id, doctorId);
    }

    private async Task<bool> IsDoctorNotAssociatedWithMedicalSlot(FilterDefinition<MedicalSlot> doctorFilter)
    {
        return !await context
            .MedicalSlotCollection
            .Find(doctorFilter)
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
            .Find(filter)
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
            .Find(filter)
            .AnyAsync();
    }
}