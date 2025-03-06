namespace v3.Doctors.Exceptions;

public class DuplicatedMedicalLicenseNumberException()
    : Exception("Given medical license number is already in use.");