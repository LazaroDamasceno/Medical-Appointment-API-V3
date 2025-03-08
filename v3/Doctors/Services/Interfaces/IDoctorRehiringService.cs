namespace v3.Doctors.Services.Interfaces;

public interface IDoctorRehiringService
{
    Task Rehire(string medicalLicenseNumber);
}