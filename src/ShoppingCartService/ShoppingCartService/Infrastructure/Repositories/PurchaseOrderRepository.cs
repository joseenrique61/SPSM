using MongoDB.Driver;
using ShoppingCartService.Domain.Models;
using ShoppingCartService.Domain.Repositories;

namespace ShoppingCartService.Infrastructure.Repositories;

public class PurchaseOrderRepository : IPurchaseOrderRepository
{
    private readonly IMongoCollection<PurchaseOrder> _ordersCollection;

    public PurchaseOrderRepository(IConfiguration mongoSettings, ILogger<PurchaseOrderRepository> logger)
    {
        logger.LogError(mongoSettings["MongoDb:ConnectionString"]);
        logger.LogError(mongoSettings["MongoDb:Database"]);
        
        var client = new MongoClient(mongoSettings["MongoDb:ConnectionString"]);
        var database = client.GetDatabase(mongoSettings["MongoDb:Database"]);
        _ordersCollection = database.GetCollection<PurchaseOrder>("PurchaseOrders");
    }

    // Get order by ID
    public async Task<PurchaseOrder?> GetByUserIdAsync(int id)
    {
        return await _ordersCollection.Find(o => o.UserId == id).FirstOrDefaultAsync();
    }

    // Create an order
    public async Task CreateAsync(PurchaseOrder order)
    {
        await _ordersCollection.InsertOneAsync(order);
    }

    // Update an order
    public async Task UpdateAsync(string id, PurchaseOrder order)
    {
        await _ordersCollection.ReplaceOneAsync(o => o.Id == id, order);
    }

    // Delete an order
    public async Task DeleteAsync(string id)
    {
        await _ordersCollection.DeleteOneAsync(o => o.Id == id);
    }
}