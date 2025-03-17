using MongoDB.Bson.Serialization.Attributes;

namespace v3.Doctors.Domain;

public class DoctorAuditTrail
{

    [BsonId]
    public Guid Id { get; private set; } =  Guid.NewGuid();
    public Doctor Doctor { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    private DoctorAuditTrail(Doctor doctor)
    {
        Doctor = doctor;
    }

    public static DoctorAuditTrail Create(Doctor doctor)
    {
        return new DoctorAuditTrail(doctor);
    }
}