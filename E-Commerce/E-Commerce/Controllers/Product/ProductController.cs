using E_Commerce.BLL;
using E_Commerce.Common;
using E_Commerce.Common.Pagination;
using E_Commerce.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetProduct()
        {
            var result = await _productManager.GetAllProduct();
            return Ok(result);
        }
        [HttpGet("/pagination") ]
        public async Task<ActionResult> GetProductWitPagination(
            [FromQuery] PaginationParameters paginationParameters
            , [FromQuery] ProductFiltersParameters filtersParameters
            )
        {
            var result = await _productManager.GetAllProductWithPagination(paginationParameters, filtersParameters);
            return Ok(result);
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetProductById([FromRoute]Guid id)
        {
            var result = await _productManager.GetProductById(id);
            return Ok(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] ProductCreateDto product)
        {
            var result = await _productManager.CreateProduct(product);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
            return Ok(result);
        }


        [Authorize(Policy = "AdminOnly")]
        [HttpPut]
        public async Task<ActionResult> EditProduct([FromBody] ProductEditDto product)
        {
            var result = await _productManager.EditeProduct(product);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [Authorize(Policy = "AdminOnly")]
        [HttpDelete ("{id:Guid}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var result = await _productManager.DeleteProduct(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
