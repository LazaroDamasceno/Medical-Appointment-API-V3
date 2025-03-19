using MongoDB.Driver;
using v3.Context;
using v3.Doctors.DTOs;
using v3.Doctors.Services.Interfaces;
using v3.Doctors.Utils;

namespace v3.Doctors.Services.Impl;

public class DoctorRetrievalService(
    MongoDbContext context,
    DoctorFinder doctorFinder
): IDoctorRetrievalService
{
    
    public async Task<IEnumerable<DoctorResponseDto>> GetAll()
    {
        var all = await context
            .DoctorsCollection
            .Find(_ => true)
            .ToListAsync();
        return all.Select(DoctorResponseMapper.Map);
    }

    public async Task<DoctorResponseDto> GetByMedicalLicenseNumber(string medicalLicenseNumber)
    {
        var doctor = await doctorFinder.FindByMedicalLicenceNumber(medicalLicenseNumber);
        return DoctorResponseMapper.Map(doctor);
    }
}