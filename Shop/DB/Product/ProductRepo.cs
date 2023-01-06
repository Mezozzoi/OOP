using System;
using System.Collections;
using MongoDB.Bson;
using MongoDB.Driver;

class ProductRepo : IRepository<ProductDto>
{
    private static IMongoCollection<ProductDto> collection = DB.GetCollection<ProductDto>("products");

    public ProductDto GetById(ObjectId id)
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
    
    public ProductDto GetByTitle(string title)
    {
        try
        {
            var filter = new BsonDocument { { "Title", title } };
            return collection.FindSync(filter).First();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public IEnumerable List()
    {
        return collection.Find(_ => true).ToList();
    }

    public ProductDto Add(ProductDto product)
    {
        collection.InsertOne(product);
        BsonDocument filter = new BsonDocument { { "_id", product.Id } };
        return collection.FindSync(filter).First();
    }

    public void Delete(ObjectId productId)
    {
        BsonDocument filter = new BsonDocument { { "_id", productId } };
        collection.DeleteOne(filter);
    }

    public ProductDto Update(ProductDto product)
    {
        BsonDocument filter = new BsonDocument { { "_id", product.Id } };
        collection.ReplaceOne(filter, product);
        return collection.FindSync(filter).First();
    }
}