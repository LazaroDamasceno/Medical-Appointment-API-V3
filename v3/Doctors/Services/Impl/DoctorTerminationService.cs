using MongoDB.Driver;
using v3.Context;
using v3.Doctors.Domain;
using v3.Doctors.Exceptions;
using v3.Doctors.Services.Interfaces;
using v3.Doctors.Utils;

namespace v3.Doctors.Services.Impl;

public class DoctorTerminationService(    
    MongoDbContext context, 
    DoctorFinder doctorFinder
): IDoctorTerminationService
{
    public async Task Terminate(string medicalLicenseNumber)
    {
        var doctor = await doctorFinder.FindByMedicalLicenceNumber(medicalLicenseNumber);
        OnActiveDoctor(doctor);
        await UpdateDoctor(medicalLicenseNumber);
        var doctorAuditTrail = DoctorAuditTrail.Create(doctor);
        await context.DoctorAuditTrailCollection.InsertOneAsync(doctorAuditTrail);
    }
    
    private static void OnActiveDoctor(Doctor doctor)
    {
        if (doctor.TerminatedAt == null)
        {
            const string message = "Doctor cannot be rehired, because they're already active.";
            throw new ImmutableDoctorException(message);
        }
    }
    
    private async Task UpdateDoctor(string medicalLicenseNumber)
    {
        var filter = Builders<Doctor>.Filter.Eq(x => x.MedicalLicenseNumber, medicalLicenseNumber);
        var update = Builders<Doctor>.Update.Set(x => x.TerminatedAt, DateTime.UtcNow);
        await context.DoctorsCollection.UpdateOneAsync(filter, update);
    }
}