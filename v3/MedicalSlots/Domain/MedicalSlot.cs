using MongoDB.Bson.Serialization.Attributes;
using v3.Doctors.Domain;

namespace v3.MedicalSlots.Domain;

public class MedicalSlot
{
    
    [BsonId]
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public Doctor Doctor { get; private set; }
    public DateTime AvailableAt { get; private set; }
    public DateTime? CanceledAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    private MedicalSlot(Doctor doctor, DateTime availableAt)
    {
        Doctor = doctor;
        AvailableAt = availableAt;
    }

    public static MedicalSlot Create(Doctor doctor, DateTime availableAt)
    {
        return new MedicalSlot(doctor, availableAt);
    }
}