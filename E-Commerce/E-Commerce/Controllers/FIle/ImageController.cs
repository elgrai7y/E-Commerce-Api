using E_Commerce.BLL;
using E_Commerce.BLL.Manager;

using E_Commerce.Common.GeneralResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers.FIle
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class ImageController : ControllerBase
    {
        /*------------------------------------------------------------------*/
        private readonly IProductManager _productManager;
        private readonly ICategoryManager _categotryManager;

        private readonly IImageManager _imageManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        /*------------------------------------------------------------------*/
        public ImageController(ICategoryManager categoryManager , IProductManager productManager,IImageManager imageManager, IWebHostEnvironment webHostEnvironment)
        {
            _imageManager = imageManager;
            _webHostEnvironment = webHostEnvironment;
            _productManager = productManager;
            _categotryManager=categoryManager;
        }
        /*------------------------------------------------------------------*/
        [HttpPost]
        public async Task<ActionResult<GeneralResult<ImageResultDto>>> uplodImage([FromForm] ImageDto imageUploadDto)
        {
            var schema = Request.Scheme;
            var host = Request.Host.Value;
            var basePath = _webHostEnvironment.ContentRootPath;



            var result = await _imageManager.UploadImage(imageUploadDto, basePath, schema, host);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("product/{id:Guid}")]
        public async Task<ActionResult<GeneralResult<ImageResultDto>>> uplodPrroductImage([FromForm] Guid id ,[FromForm] ImageDto imageUploadDto)
        {
            var schema = Request.Scheme;
            var host = Request.Host.Value;
            var basePath = _webHostEnvironment.ContentRootPath;



            var image = await _imageManager.UploadImage(imageUploadDto, basePath, schema, host);
            if (!image.Success)
            {
                return BadRequest(image);
            }
            var result = await _productManager.UploadImageProduct(id, image.Data.ImageURL);
            return Ok(result);
        }
        [HttpPost("category/{id:Guid}")]
        public async Task<ActionResult<GeneralResult<ImageResultDto>>> uplodCategoryImage([FromForm] Guid id, [FromForm] ImageDto imageUploadDto)
        {
            var schema = Request.Scheme;
            var host = Request.Host.Value;
            var basePath = _webHostEnvironment.ContentRootPath;

            var image = await _imageManager.UploadImage(imageUploadDto, basePath, schema, host);
            if (!image.Success)
            {
                return BadRequest(image);
            }
            var result = await _categotryManager.UploadImageCategory(id, image.Data.ImageURL);
             return Ok(result);
        }

    }
}
