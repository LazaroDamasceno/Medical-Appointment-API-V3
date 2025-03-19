using MongoDB.Bson.Serialization.Attributes;
using v3.People.Domain;

namespace v3.Doctors.Domain;

public class Doctor
{

    [BsonId] 
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public string MedicalLicenseNumber { get; private set; }
    public Person Person { get; private set; }
    public DateTime HiredAt { get; private set; } = DateTime.UtcNow;
    public DateTime? TerminatedAt { get; private set; }

    private Doctor(string medicalLicenseNumber, Person person)
    {
        MedicalLicenseNumber = medicalLicenseNumber;
        Person = person;
    }

    public static Doctor Create(string medicalLicenseNumber, Person person)
    {
        return new Doctor(medicalLicenseNumber, person);
    }

    public void MarkAsTerminated() => TerminatedAt = DateTime.Now;

    public void MarkAsHired() => TerminatedAt = null;
}