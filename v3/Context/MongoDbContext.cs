using MongoDB.Driver;
using v3.Customers.Domain;
using v3.Doctors.Domain;
using v3.MedicalSlots.Domain;
using v3.People.Domain;

namespace v3.Context;

public class MongoDbContext
{
    private IMongoDatabase Database { get; }
    
    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");
        var mongoUrl = new MongoUrl(connectionString);
        var mongoClient = new MongoClient(mongoUrl);
        Database = mongoClient.GetDatabase(mongoUrl.DatabaseName);   
    }
    
    public IMongoCollection<Person> PeopleCollection => Database.GetCollection<Person>("People");
    
    public IMongoCollection<Customer> CustomersCollection => Database.GetCollection<Customer>("Customers");
    
    public IMongoCollection<Doctor> DoctorsCollection => Database.GetCollection<Doctor>("Doctors");
    
    public IMongoCollection<DoctorAuditTrail> DoctorAuditTrailCollection => Database.GetCollection<DoctorAuditTrail>("DoctorAuditTrail");

    public IMongoCollection<MedicalSlot> MedicalSlotCollection => Database.GetCollection<MedicalSlot>("MedicalSlots");

}