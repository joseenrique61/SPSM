using InventoryService.Application.DTOs;
using InventoryService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> AddProduct([FromBody] ProductDTO productDTO)
        {
            var status = await _productService.AddProductAsync(productDTO);

            if (!status)
                return BadRequest();

            return Ok(status);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var status = await _productService.DeleteProductAsync(id);

            if (!status)
                return NotFound($"Product with ID: {id} not found.");

            return NoContent();
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductDTO productDTO)
        {
            var status = await _productService.UpdateProductAsync(id, productDTO);

            if (!status)
                return NotFound($"Product with ID: {id} not found.");

            return NoContent();
        }

        [HttpPut]
        [Route("reduce")]
        public async Task<ActionResult> ReduceStock([FromBody] List<ReduceDTO> reduceDTOs)
        {
            var status = await _productService.ReduceStockAsync(reduceDTOs);

            if (!status)
                return BadRequest();

            return NoContent();
        }
    }
}
