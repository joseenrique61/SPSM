using MongoDB.Driver;
using ShoppingCartService.Domain.Models;
using ShoppingCartService.Domain.Repositories;

namespace ShoppingCartService.Infrastructure.Repositories;

public class PurchaseOrderRepository : IPurchaseOrderRepository
{
    private readonly IMongoCollection<PurchaseOrder> _ordersCollection;

    public PurchaseOrderRepository(IConfiguration mongoSettings)
    {
        var client = new MongoClient(mongoSettings["MongoDb:ConnectionString"]);
        var database = client.GetDatabase(mongoSettings["MongoDb:Database"]);
        _ordersCollection = database.GetCollection<PurchaseOrder>("PurchaseOrders");
    }

    // Get order by ID
    public async Task<PurchaseOrder> GetByUserIdAsync(int id)
    {
        var purchaseOrder = await _ordersCollection.Find(o => o.UserId == id).FirstOrDefaultAsync();
        return purchaseOrder ?? new PurchaseOrder()
        {
            UserId = id,
        };
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

    public async Task<bool> UpsertAsync(PurchaseOrder order)
    {
        if (await _ordersCollection.Find(o => o.UserId == order.UserId).FirstOrDefaultAsync() == null)
        {
            await CreateAsync(order);
            return true;
        }
        
        await UpdateAsync(order.Id, order);
        return false;
    }

    // Delete an order
    public async Task DeleteAsync(string id)
    {
        await _ordersCollection.DeleteOneAsync(o => o.Id == id);
    }
}