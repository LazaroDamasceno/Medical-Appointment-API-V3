using MongoDB.Bson.Serialization.Attributes;
using v3.Common;
using v3.People.Domain;

namespace v3.Customers.Domain;

public class Customer
{

    [BsonId] 
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public Address Address { get; private set; }
    public Person Person { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

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