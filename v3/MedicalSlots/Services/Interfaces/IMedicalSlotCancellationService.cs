namespace v3.MedicalSlots.Services.Interfaces;

public interface IMedicalSlotCancellationService
{
    Task Cancel(string medicalLicenseNumber, string medicalSlotId);
}