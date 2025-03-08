using System.ComponentModel.DataAnnotations;
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
    
    public async Task<DoctorResponseDto> Hire([Required] DoctorHiringDto hiringDto)
    {
        var ssnFilter = Builders<Person>.Filter.Eq(p => p.Ssn, hiringDto.PersonRegistrationDto.Ssn);
        var isSsnDuplicated = await context.PeopleCollection.FindAsync(ssnFilter).Result.AnyAsync();
        if (isSsnDuplicated) throw new DuplicatedSsnException();
        
        var emailFilter = Builders<Person>.Filter.Eq(p => p.Email, hiringDto.PersonRegistrationDto.Email);
        var isEmailDuplicated = await context.PeopleCollection.FindAsync(emailFilter).Result.AnyAsync();
        if (isEmailDuplicated) throw new DuplicatedEmailException();
        
        var medicalLicenseNumberFilter = Builders<Doctor>.Filter.Eq(p => p.MedicalLicenseNumber, hiringDto.MedicalLicenseNumber);
        var isMedicalLicenseNumberDuplicated = await context.DoctorsCollection.FindAsync(medicalLicenseNumberFilter).Result.AnyAsync();
        if (isMedicalLicenseNumberDuplicated) throw new DuplicatedMedicalLicenseNumberException();
        
        var person = await personRegistrationService.Create(hiringDto.PersonRegistrationDto);
        var doctor = Doctor.Create(hiringDto.MedicalLicenseNumber, person);
        await context.DoctorsCollection.InsertOneAsync(doctor);
        return DoctorResponseMapper.Map(doctor);
    }
}