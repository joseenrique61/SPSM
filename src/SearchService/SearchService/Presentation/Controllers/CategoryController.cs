using Microsoft.AspNetCore.Mvc;
using SearchService.Domain.Repositories;

namespace SearchService.Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoryController(ICategoryRepository categoryRepository) : ControllerBase
{
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await categoryRepository.GetAll());
    }

    [HttpGet]
    [Route("id/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var category = await categoryRepository.GetByNameAsync(name);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }
}