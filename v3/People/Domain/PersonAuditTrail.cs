using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace v3.People.Domain;

public class PersonAuditTrail
{
    
    [BsonId] 
    public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();
    public Person Person { get; private set; }

    private PersonAuditTrail(Person person)
    {
        Person = person;
    }

    public static PersonAuditTrail Create(Person person)
    {
        return new PersonAuditTrail(person);
    }
}