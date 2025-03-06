using MongoDB.Bson;
using v3.People.Domain;

namespace v3.Doctors.Domain;

public class Doctor
{

    public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();
    public string MedicalLicenseNumber { get; private set; }
    public Person Person { get; private set; }

    private Doctor(string medicalLicenseNumber, Person person)
    {
        MedicalLicenseNumber = medicalLicenseNumber;
        Person = person;
    }

    public static Doctor Create(string medicalLicenseNumber, Person person)
    {
        return new Doctor(medicalLicenseNumber, person);
    }
}