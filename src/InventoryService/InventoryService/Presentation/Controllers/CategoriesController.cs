﻿using InventoryService.Application.DTOs;
using InventoryService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Presentation.Controllers
{
    [ApiController]
    [Route("inventory/category")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> AddCategory([FromBody] CategoryDTO categoryDTO)
        {
            var status = await _categoryService.AddCategoryAsync(categoryDTO);

            if (!status)
                return BadRequest();

            return NoContent();
        }

        //[HttpDelete]
        //[Route("delete/{id}")]
        //public async Task<ActionResult> DeleteCategory(int id)
        //{
        //    var status = await _categoryService.DeleteCategoryAsync(id);

        //    if (!status)
        //        return NotFound($"Category with ID: {id} not found.");

        //    return NoContent();
        //}

        [HttpPut]
        [Route("update/{id}")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
        {
            var status = await _categoryService.UpdateCategoryAsync(id, categoryDTO);

            if (!status)
                return NotFound($"Category with ID: {id} not found.");

            return NoContent();
        }
    }
}
