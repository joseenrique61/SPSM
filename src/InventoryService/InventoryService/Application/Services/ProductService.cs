using InventoryService.Application.DTOs;
using InventoryService.Application.Interfaces;
using InventoryService.Domain.Models;
using InventoryService.Domain.Repositories;
using InventoryService.Infrastructure.Interfaces.Producers;
using InventoryService.Infrastructure.Repositories;

namespace InventoryService.Application.Services
{
    public class ProductService : IProductService
    {
        private string exchange => "inventory.exchange";

        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        private readonly IProducer _notificationProducer;
        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger, IProducer notificationProducer )
        {
            _productRepository = productRepository;
            _notificationProducer = notificationProducer;
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

                await _productRepository.DeleteProductAsync(id);
                var productToDelete = await _productRepository.GetByIdAsync(id);

                // Publishing a message to RabbitMQ
                await _notificationProducer.PublishAsync(productToDelete, exchange, "inventory.product.deleted");

                _logger.LogInformation($"Product with ID: {id} deleted sucessfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> AddProductAsync(ProductDTO productDTO)
        {
            try
            {
                var existingProduct = await _productRepository.GetByNameAsync(productDTO.Name);

                if (existingProduct != null)
                {
                    _logger.LogInformation($"Product with Name: {existingProduct.Name} cannot be created. Duplicates are not allowed.");
                    return false;
                }

                // Mapping Product from DTO
                var product = new Product { 
                    Name = productDTO.Name,
                    Description = productDTO.Description,
                    Price = productDTO.Price,
                    Stock = productDTO.Stock,
                    CategoryId = productDTO.CategoryId,
                    ImagePath = productDTO.ImagePath
                };

                await _productRepository.AddProductAsync(product);

                // Publishing a message to RabbitMQ
                await _notificationProducer.PublishAsync(product, exchange, "inventory.product.added");

                _logger.LogInformation($"Product with ID: {product.Id} created sucessfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                _logger.LogInformation($"Product with ID: {productDTO.Name} cannot be added. Internal Error");
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(int id, ProductDTO productDTO)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);

                if (product == null)
                {
                    _logger.LogInformation($"Product with ID: {id} cannot be updated. Verify the ID");
                    return false;
                }

                // Mapping of Product from DTO
                product.Name = productDTO.Name;
                product.Description = productDTO.Description;
                product.Price = productDTO.Price;
                product.CategoryId = productDTO.CategoryId;
                product.ImagePath = productDTO.ImagePath;

                var success = await _productRepository.UpdateProductAsync(id, product);

                if (!success)
                {
                    _logger.LogInformation($"Product with ID: {id} cannot be updated. Verify the ID");
                    return false;
                }

                // Publishing a message to RabbitMQ
                await _notificationProducer.PublishAsync(product, exchange, "inventory.product.updated");

                _logger.LogInformation($"Product with ID: {id} updated sucessfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> ReduceStockAsync(List<ReduceDTO> reduceDTOs)
        {
            try
            {
                List<Product> products = [];

                foreach (ReduceDTO reduceDTO in reduceDTOs)
                {
                    var product = await _productRepository.GetByIdAsync(reduceDTO.Id);
                    
                    if (product == null)
                    {
                        _logger.LogInformation($"Product with ID: {reduceDTO.Id} cannot be reduced. Verify the ID");
                        return false;
                    }

                    if (product.Stock < reduceDTO.Amount)
                    {
                        _logger.LogInformation($"Product with ID: {reduceDTO.Id} cannot be reduced. Not Enough Stock");
                        return false;
                    }

                    product.Stock -= reduceDTO.Amount;

                    products.Add(product);
                }

                foreach (Product product in products)
                {
                    var success = await _productRepository.UpdateProductAsync(product.Id, product);

                    if (!success)
                    {
                        _logger.LogInformation($"Product with ID: {product.Id} cannot be reduced. Verify the ID");
                        return false;
                    }

                    // Publishing a message to RabbitMQ
                    await _notificationProducer.PublishAsync(product, exchange, "inventory.product.reduced");

                    _logger.LogInformation($"Product with ID: {product.Id} reduced sucessfully.");
                }

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
