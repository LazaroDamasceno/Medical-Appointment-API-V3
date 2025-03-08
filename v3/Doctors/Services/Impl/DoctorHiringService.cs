using MongoDB.Driver;
using v3.Context;
using v3.Doctors.Domain;
using v3.Doctors.DTOs;
using v3.Doctors.Exceptions;
using v3.Doctors.Services.Interfaces;
using v3.Doctors.Utils;
using v3.People.Domain;
using v3.People.Exceptions;
using v3.People.Services.Interfaces;

namespace v3.Doctors.Services.Impl;

public class DoctorHiringService(
    MongoDbContext context, 
    IPersonRegistrationService personRegistrationService
): IDoctorHiringService
{
    
    public async Task<DoctorResponseDto> Hire(DoctorHiringDto hiringDto)
    {
        OnDuplicatedSsn(hiringDto.PersonRegistrationDto.Ssn);
        OnDuplicatedEmail(hiringDto.PersonRegistrationDto.Email);
        OnDuplicatedMedicalLicenseNumber(hiringDto.MedicalLicenseNumber);
        var person = await personRegistrationService.Create(hiringDto.PersonRegistrationDto);
        var doctor = Doctor.Create(hiringDto.MedicalLicenseNumber, person);
        await context.DoctorsCollection.InsertOneAsync(doctor);
        return DoctorResponseMapper.Map(doctor);
    }
    
    private void OnDuplicatedSsn(string ssn)
    {
        var filter = Builders<Person>.Filter.Eq(p => p.Ssn, ssn);
        var isDuplicated = context.PeopleCollection.Find(filter).Any();
        if (isDuplicated) throw new DuplicatedSsnException();
    }
    
    private void OnDuplicatedEmail(string email)
    {
        var filter = Builders<Person>.Filter.Eq(p => p.Email, email);
        var isDuplicated = context.PeopleCollection.Find(filter).Any();
        if (isDuplicated) throw new DuplicatedEmailException();
    }

    private void OnDuplicatedMedicalLicenseNumber(string medicalLicenseNumber)
    {
        var filter = Builders<Doctor>.Filter.Eq(p => p.MedicalLicenseNumber, medicalLicenseNumber);
        var isDuplicated = context.DoctorsCollection.Find(filter).Any();
        if (isDuplicated) throw new DuplicatedMedicalLicenseNumberException();
    }
}