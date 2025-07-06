using InventoryService.Application.Interfaces;
using InventoryService.Domain.Models;
using InventoryService.Domain.Repositories;

namespace InventoryService.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;
        public ProductService(IProductRepository productRepository, ILogger logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var success = await _productRepository.DeleteProductAsync(id);

                if (!success)
                {
                    _logger.LogInformation($"Product with ID: {id} cannot be deleted. Verify the ID");
                    return false;
                }

                _logger.LogInformation($"Product with ID: {id} deleted sucessfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> AddProductAsync(Product product)
        {


            try
            {
                await _productRepository.AddProductAsync(product);

                _logger.LogInformation($"Product with ID: {product.Id} created sucessfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                _logger.LogInformation($"Product with ID: {product.Id} cannot be added. Internal Error");
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            try
            {
                var success = await _productRepository.UpdateProductAsync(id, product);

                if (!success)
                {
                    _logger.LogInformation($"Product with ID: {id} cannot be updated. Verify the ID");
                    return false;
                }

                _logger.LogInformation($"Product with ID: {id} updated sucessfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
