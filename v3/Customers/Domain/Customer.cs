using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using v3.Common;
using v3.People.Domain;

namespace v3.Customers.Domain;

public class Customer
{
    
    [BsonId]
    public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();
    public Address Address { get; set; }
    public Person Person { get; set; }

    private Customer(Address address, Person person)
    {
        Address = address;
        Person = person;
    }

    public static Customer Create(Address address, Person person)
    {
        return new Customer(address, person);
    }
}