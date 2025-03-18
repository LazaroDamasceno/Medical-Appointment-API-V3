using MongoDB.Driver;
using v3.Context;
using v3.Doctors.Domain;
using v3.Doctors.Exceptions;

namespace v3.Doctors.Utils;

public class DoctorFinder(MongoDbContext context)
{
    
    public async Task<Doctor> FindByMedicalLicenceNumber(string medicalLicenceNumber)
    {
        var filter = Builders<Doctor>.Filter.Eq(x => x.MedicalLicenseNumber, medicalLicenceNumber);
        var doctor = await context.DoctorsCollection.Find(filter).FirstOrDefaultAsync();
        if (doctor == null) throw new NonExistentDoctorException(medicalLicenceNumber);
        return doctor;
    }
}