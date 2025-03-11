using v3.Doctors.Domain;
using v3.Doctors.DTOs;

namespace v3.Doctors.Utils;

public static class DoctorResponseMapper
{
    public static DoctorResponseDto Map(Doctor doctor)
    {
        return new DoctorResponseDto(
            doctor.MedicalLicenseNumber,
            doctor.Person.FullName()
        );
    }
}