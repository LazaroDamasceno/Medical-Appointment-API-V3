using MongoDB.Driver;
using v3.Context;
using v3.Doctors.Domain;
using v3.Doctors.Exceptions;
using v3.Doctors.Services.Interfaces;
using v3.Doctors.Utils;

namespace v3.Doctors.Services.Impl;

public class DoctorRehiringService(
    MongoDbContext context,
    DoctorFinder doctorFinder
): IDoctorRehiringService
{
    public async Task Rehire(string medicalLicenseNumber)
    {
        var doctor = await doctorFinder.FindByMedicalLicenceNumber(medicalLicenseNumber);
        OnTerminatedDoctor(doctor);
        var filter = Builders<Doctor>.Filter.Eq(x => x.MedicalLicenseNumber, doctor.MedicalLicenseNumber);
        var update = Builders<Doctor>.Update.Set(x => x.TerminatedAt, DateTime.UtcNow);
        await context.DoctorsCollection.UpdateOneAsync(filter, update);
        
        var doctorAuditTrail = DoctorAuditTrail.Create(doctor);
        await context.DoctorAuditTrailCollection.InsertOneAsync(doctorAuditTrail);
    }

    private static void OnTerminatedDoctor(Doctor doctor)
    {
        if (doctor.TerminatedAt != null)
        {
            const string message = "Doctor cannot be terminated, because they're already terminated.";
            throw new ImmutableDoctorException(message);
        }
    }
}