using E_Commerce.BLL;
using E_Commerce.Common.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet("pagination/")]
        public async Task<ActionResult> GetProductWitPagination([FromQuery] PaginationParameters paginationParameters)
        {
            var result = await _categoryManager.GetAllCategoryWithPagination(paginationParameters);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var result = await _categoryManager.GetAllCategory();
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]

        public async Task<ActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var result = await _categoryManager.GetCategoryById(id);
            return Ok(result);
        }
        [HttpGet("products/{id:Guid}")]
        public async Task<ActionResult> GetCategoryProductsById([FromRoute] Guid id)
        {
            var result = await _categoryManager.GetCategorysProductsById(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> CreateCategory([FromBody] CategoryCreateDto category)
        {
            var result = await _categoryManager.CreateCategory(category);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> EditCategory([FromBody] CategoryEditDto category)
        {
            var result = await _categoryManager.EditCategory(category);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var result = await _categoryManager.DeleteCategory(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
