using MongoDB.Bson.Serialization.Attributes;
using v3.People.DTOs;
using v3.People.Enums;

namespace v3.People.Domain;

public class Person
{

    [BsonId] 
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    private string FirstName { get; set; }
    private string MiddleName { get; set; }
    private string LastName { get; set; }
    public DateOnly BirthDate { get; private set; }
    public string Ssn { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public Gender Gender { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

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
}