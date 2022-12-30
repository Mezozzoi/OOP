using System;
using System.Collections;
using MongoDB.Bson;
using MongoDB.Driver;

public class CustomerRepo : IRepository<CustomerDto>
{
    private static IMongoCollection<CustomerDto> collection = DB.GetCollection<CustomerDto>("customers");

    public CustomerDto GetById(ObjectId id)
    {
        try
        {
            var filter = new BsonDocument { { "_id", id } };
            return collection.FindSync(filter).First();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public CustomerDto GetByLogin(string login)
    {
        try
        {
            var filter = new BsonDocument { { "Login", login } };
            return collection.FindSync(filter).First();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public IEnumerable List()
    {
        return collection.Find("").ToList();
    }

    public CustomerDto Add(CustomerDto customer)
    {
        collection.InsertOne(customer);
        BsonDocument filter = new BsonDocument { { "_id", customer.Id } };
        return collection.FindSync(filter).First();
    }

    public void Delete(CustomerDto customer)
    {
        BsonDocument filter = new BsonDocument { { "_id", customer.Id } };
        collection.DeleteOne(filter);
    }

    public CustomerDto Update(CustomerDto customer)
    {
        BsonDocument filter = new BsonDocument { { "_id", customer.Id } };
        collection.ReplaceOne(filter, customer);
        return collection.FindSync(filter).First();
    }
}