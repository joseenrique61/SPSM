﻿using InventoryService.Application.DTOs;
using InventoryService.Application.Interfaces;
using InventoryService.Domain.Models;
using InventoryService.Domain.Repositories;
using InventoryService.Infrastructure.Interfaces.Producers;
using InventoryService.Infrastructure.Repositories;

namespace InventoryService.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private string exchange => "inventory.exchange";

        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;
        private readonly IProducer _notificationProducer;

        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger, IProducer notificationProducer)
        {
            _categoryRepository = categoryRepository;
            _notificationProducer = notificationProducer;
            _logger = logger;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var success = await _categoryRepository.DeleteCategoryAsync(id);

                if (!success)
                {
                    _logger.LogInformation($"Category with ID: {id} cannot be deleted. Verify the ID");
                    return false;
                }

                var categoryToDelete = await _categoryRepository.GetByIdAsync(id);

                // Publishing a message to RabbitMQ
                await _notificationProducer.PublishAsync(categoryToDelete, exchange, "inventory.category.deleted");

                _logger.LogInformation($"Category with ID: {id} deleted sucessfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> AddCategoryAsync(CategoryDTO categoryDTO)
        {
            try
            {
                var existingCategory = await _categoryRepository.GetByNameAsync(categoryDTO.Name);

                if (existingCategory != null)
                {
                    _logger.LogInformation($"Category with Name: {categoryDTO.Name} cannot be created. Duplicates are not allowed.");
                    return false;
                }

                // Mapping Category from DTO
                var category = new Category
                {
                    Name = categoryDTO.Name
                };

                await _categoryRepository.AddCategoryAsync(category);

                // Publishing a message to RabbitMQ
                await _notificationProducer.PublishAsync(category, exchange, "inventory.category.added");

                _logger.LogInformation($"Category with ID: {category.Id} created sucessfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                _logger.LogInformation($"Category with Name: {categoryDTO.Name} cannot be added. Internal Error");
                return false;
            }
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryDTO categoryDTO)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);

                if (category == null)
                {
                    _logger.LogInformation($"Category with ID: {id} cannot be updated. Verify the ID");
                    return false;
                }

                // Mapping of Category from DTO
                category.Name = categoryDTO.Name;

                var success = await _categoryRepository.UpdateCategoryAsync(id, category);

                if (!success)
                {
                    _logger.LogInformation($"Category with ID: {id} cannot be updated. Verify the ID");
                    return false;
                }

                // Publishing a message to RabbitMQ
                await _notificationProducer.PublishAsync(category, exchange, "inventory.category.updated");

                _logger.LogInformation($"Category with ID: {id} updated sucessfully.");
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
