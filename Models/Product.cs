using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace milk_backend.Models;

public class Product {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _id { get; set; }

    // Legacy id, not for sorting
    public string? id { get; set; }

    public string name { get; set; } = null!;
    public string type { get; set; } = null!;
    public int storage { get; set; } = 0;
}

public class ProductDTO {
    public string? id { get; set; }
    public string name { get; set; } = null!;
    public string type { get; set; } = null!;
    public int storage { get; set; } = 0;
}