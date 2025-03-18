using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;
using v3.Common;
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
    IPersonRegistrationService personRegistrationService,
    PersonalDataChecker personalDataChecker
): IDoctorHiringService
{
    
    public async Task<DoctorResponseDto> Hire([Required] DoctorHiringDto hiringDto)
    {
        var ssn = hiringDto.PersonRegistrationDto.Ssn;
        if (await personalDataChecker.IsSsnDuplicated(ssn)) throw new DuplicatedSsnException();
        
        var email = hiringDto.PersonRegistrationDto.Email;
        if (await personalDataChecker.IsEmailDuplicated(ssn)) throw new DuplicatedEmailException();

        var medicalLicenseNumber = hiringDto.MedicalLicenseNumber;
        if (await IsMedicalLicenseNumberDuplicated(medicalLicenseNumber)) throw new DuplicatedMedicalLicenseNumberException();
        
        var person = await personRegistrationService.Create(hiringDto.PersonRegistrationDto);
        var doctor = Doctor.Create(hiringDto.MedicalLicenseNumber, person);
        await context.DoctorsCollection.InsertOneAsync(doctor);
        return DoctorResponseMapper.Map(doctor);
    }

    private async Task<bool> IsMedicalLicenseNumberDuplicated(string medicalLicenseNumber)
    {
        var filter = Builders<Doctor>.Filter.Eq(p => p.MedicalLicenseNumber, medicalLicenseNumber);
        return await context.DoctorsCollection.Find(filter).AnyAsync();
    }
}