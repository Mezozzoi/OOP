using MongoDB.Driver;

public class DB
{
    private const string connectionString = "mongodb://localhost:27017";
    
    private static MongoClient Client = new MongoClient(connectionString);
    private static IMongoDatabase database = Client.GetDatabase("course_shop");

    public static IMongoCollection<T> GetCollection<T>(string name)
    {
        return database.GetCollection<T>(name);
    }
}