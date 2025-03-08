namespace v3.Doctors.Services.Interfaces;

public interface IDoctorTerminationService
{
    Task Terminate(string medicalLicenseNumber);
}