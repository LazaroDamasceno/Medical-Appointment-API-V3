namespace v3.Doctors.Exceptions;

public class NonExistentDoctorException(string medicalLicenseNumber)
    : Exception($"Doctor whose medical license number is {medicalLicenseNumber} was not found.");