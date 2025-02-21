using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace v3.People;

public class Person
{

    [BsonId] 
    public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string LastName { get; private set; }
    public string SSN { get; private set; }
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

}