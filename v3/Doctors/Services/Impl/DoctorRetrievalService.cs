using MongoDB.Driver;
using v3.Context;
using v3.Doctors.DTOs;
using v3.Doctors.Services.Interfaces;
using v3.Doctors.Utils;

namespace v3.Doctors.Services.Impl;

public class DoctorRetrievalService(
    MongoDbContext context,
    DoctorFinderUtil doctorFinderUtil
): IDoctorRetrievalService
{
    
    public async Task<List<DoctorResponseDto>> GetAll()
    {
        var all = await context
            .DoctorsCollection
            .FindAsync(_ => true)
            .Result
            .ToListAsync();
        return all.Select(DoctorResponseMapper.Map).ToList();
    }

    public async Task<DoctorResponseDto> GetByMedicalLicenseNumber(string medicalLicenseNumber)
    {
        var doctor = await doctorFinderUtil.FindByMedicalLicenceNumber(medicalLicenseNumber);
        return DoctorResponseMapper.Map(doctor);
    }
}