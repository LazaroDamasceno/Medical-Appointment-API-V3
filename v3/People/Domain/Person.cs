using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using v3.People.DTOs;

namespace v3.People.Domain;

public class Person
{

    [BsonId] 
    public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();
    private string FirstName { get; set; }
    private string MiddleName { get; set; }
    private string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Ssn { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Gender { get; private set; }

    public string FullName()
    {
        if (MiddleName == "")
        {
            return $"{FirstName} {LastName}";
        }
        return $"{FirstName} {MiddleName} {LastName}";
    }

    private Person(PersonRegistrationDto registrationDto)
    {
        FirstName = registrationDto.FirstName;
        MiddleName = registrationDto.MiddleName;
        LastName = registrationDto.LastName;
        BirthDate = registrationDto.BirthDate;
        Ssn = registrationDto.Ssn;
        Email = registrationDto.Email;
        PhoneNumber = registrationDto.PhoneNumber;
        Gender = registrationDto.Gender;
    }

    public static Person Create(PersonRegistrationDto registrationDto)
    {
        return new Person(registrationDto);
    }
    
    public void Modify(PersonModificationDto modificationDto)
    {
        FirstName = modificationDto.FirstName;
        MiddleName = modificationDto.MiddleName;
        LastName = modificationDto.LastName;
        BirthDate = modificationDto.BirthDate;
        Email = modificationDto.Email;
        PhoneNumber = modificationDto.PhoneNumber;
        Gender = modificationDto.Gender;
    }
}