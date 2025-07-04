using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingCartService.Domain.Models;

public class PurchaseOrder
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("userId")]
    public int UserId { get; set; }

    [BsonElement("products")] 
    public List<Product> Products { get; set; } = [];
}