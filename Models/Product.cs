using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

public class Product {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _id { get; set; }

    // Legacy id
    public string? id { get; set; }

    public string name { get; set; } = null!;
    public string type { get; set; } = null!;
    public int storage { get; set; } = 0;
}