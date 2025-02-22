using MongoDB.Driver;
using v3.People.Domain;

namespace v3.Context;

public class MongoDbContext
{
    private IMongoDatabase Database { get; set; }
    
    private MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");
        var mongoUrl = new MongoUrl(connectionString);
        var mongoClient = new MongoClient(mongoUrl);
        Database = mongoClient.GetDatabase(mongoUrl.DatabaseName);   
    }
    
    public IMongoCollection<Person> PeopleCollection => Database.GetCollection<Person>("People");
    
    public IMongoCollection<Person> CustomersCollection => Database.GetCollection<Person>("Customers");
}