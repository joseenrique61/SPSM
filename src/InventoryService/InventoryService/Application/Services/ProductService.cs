using InventoryService.Application.DTOs;
using InventoryService.Application.Interfaces;
using InventoryService.Domain.Models;
using InventoryService.Domain.Repositories;
using InventoryService.Infrastructure.Repositories;

namespace InventoryService.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;
        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
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

                // Mapping Category from DTO
                var product = new Product { 
                    Name = productDTO.Name,
                    Description = productDTO.Description,
                    Price = productDTO.Price,
                    CategoryId = productDTO.CategoryId,
                    ImagePath = productDTO.ImagePath
                };

                await _productRepository.AddProductAsync(product);

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
                    _logger.LogInformation($"Category with ID: {id} cannot be updated. Verify the ID");
                    return false;
                }

                // Mapping of Category from DTO
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
