using milk_backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;

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

    public async Task<ProductDTO> GetByLegacyId(string id) 
    { 
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("id", id);
        Product prod = await _productCollection.Find(filter).FirstAsync();
        return ProductToDTO(prod);
    }

    public async Task CreateAsync(Product product) 
    {
        await _productCollection.InsertOneAsync(product);
        return;
    }
    // public async Task UpdateQuantityAsync(string id, int orderSize) 
    // {
    //     FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("id", id);
    //     UpdateDefinition<Product> update = Builders<Product>.Update.Set<Product>("storage",);
    //     await _productCollection.UpdateOneAsync(filter, update);
    //     return;
    // }

    public async Task DeleteAsync(string id) 
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("_id", id);
        await _productCollection.DeleteOneAsync(filter);
        return;
    }

    private ProductDTO ProductToDTO(Product product)
    {
        return new ProductDTO
            {
                id = product.id,
                name = product.name,
                type = product.type,
                storage = product.storage
            };
    }

}