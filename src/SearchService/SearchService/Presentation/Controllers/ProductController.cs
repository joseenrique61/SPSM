using Microsoft.AspNetCore.Mvc;
using SearchService.Domain.Models;
using SearchService.Domain.Repositories;

namespace SearchService.Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductController(IProductRepository productRepository)
{
    [HttpGet]
    [Route("all")]
    public async Task<List<Product>> GetAll()
    {
        return await productRepository.GetAll();
    }

    [HttpGet]
    [Route("id/{id}")]
    public async Task<Product?> GetById(int id)
    {
        return await productRepository.GetByIdAsync(id);
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<List<Product>> GetByName(string name)
    {
        return await productRepository.GetByName(name);
    }

    [HttpGet]
    [Route("category/{category}")]
    public async Task<List<Product>> GetByCategory(string category)
    {
        return await productRepository.GetByCategory(category);
    }
}