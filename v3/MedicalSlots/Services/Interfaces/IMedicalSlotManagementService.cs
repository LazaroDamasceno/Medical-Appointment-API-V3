namespace v3.MedicalSlots.Services.Interfaces;

public interface IMedicalSlotManagementService
{
    Task Cancel(string medicalLicenseNumber, string medicalSlotId);
    Task Complete(string medicalLicenseNumber, string medicalSlotId);
}