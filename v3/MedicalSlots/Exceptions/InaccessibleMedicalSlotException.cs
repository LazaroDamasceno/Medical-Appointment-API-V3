namespace v3.MedicalSlots.Exceptions;

public class InaccessibleMedicalSlotException(string medicalLicenseNumber)
    : Exception($"Doctor whose medical license number is {medicalLicenseNumber} is not medical slot's doctor.");