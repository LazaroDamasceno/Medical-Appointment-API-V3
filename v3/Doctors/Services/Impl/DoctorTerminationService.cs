﻿using MongoDB.Driver;
using v3.Context;
using v3.Doctors.Domain;
using v3.Doctors.Exceptions;
using v3.Doctors.Services.Interfaces;
using v3.Doctors.Utils;

namespace v3.Doctors.Services.Impl;

public class DoctorTerminationService(    
    MongoDbContext context, 
    DoctorFinderUtil doctorFinderUtil
): IDoctorTerminationService
{
    public async Task Terminate(string medicalLicenseNumber)
    {
        var doctor = await doctorFinderUtil.FindByMedicalLicenceNumber(medicalLicenseNumber);
        OnActiveDoctor(doctor);
        var filter = Builders<Doctor>.Filter.Eq(x => x.MedicalLicenseNumber, doctor.MedicalLicenseNumber);
        var update = Builders<Doctor>.Update.Set(x => x.TerminatedAt, null);
        await context.DoctorsCollection.UpdateOneAsync(filter, update);
    }
    
    private void OnActiveDoctor(Doctor doctor)
    {
        if (doctor.TerminatedAt == null)
        {
            const string message = "Doctor cannot be rehired, because they're already active.";
            throw new ImmutableDoctorException(message);
        }
    }
}