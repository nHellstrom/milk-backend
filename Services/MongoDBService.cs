using milk_backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace milk_backend.Services;

public class MongoDBService {

    private readonly IMongoCollection<Product> _productCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _productCollection = database.GetCollection<Product>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<Product>> GetAsync() 
    { 
        return await _productCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<List<Product>> GetByName() 
    { 
        return await _productCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task CreateAsync(Product product) 
    {
        await _productCollection.InsertOneAsync(product);
        return;
    }
    public async Task UpdateAsync(string id, Product product) 
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("_id", id);
        UpdateDefinition<Product> update = Builders<Product>.Update.AddToSet<Product>("movieIds", product);
        await _productCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id) 
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("_id", id);
        await _productCollection.DeleteOneAsync(filter);
        return;
    }

}