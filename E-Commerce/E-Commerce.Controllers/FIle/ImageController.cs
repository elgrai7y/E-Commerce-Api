using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

namespace E_Commerce.Controllers.FIle
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ImageController(IWebHostEnvironment env)
        {
            _webHostEnvironment = env;
        }

        // GET api/image/{fileName}
        [HttpGet("{fileName}")]
        public IActionResult Get(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return BadRequest();

            var filesDir = Path.Combine(_env.ContentRootPath, "Files");
            var filePath = Path.Combine(filesDir, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, contentType);
        }

        [HttpPost("product/{id:Guid}")]
        public async Task<ActionResult<GeneralResult<ImageResultDto>>> uplodPrroductImage([FromRoute] Guid id ,[FromForm] ImageDto imageUploadDto)
        {
            var result = await _productManager.UploadImageProduct(id, imageUploadDto.ImageURL);
            return Ok(result);
        }

        [HttpPost("category/{id:Guid}")]
        public async Task<ActionResult<GeneralResult<ImageResultDto>>> uplodCategoryImage([FromRoute] Guid id, [FromForm] ImageDto imageUploadDto)
        {
            var schema = Request.Scheme;
            var host = Request.Host.Value;
            var basePath = _env.ContentRootPath;

            var image = await _imageManager.UploadImage(imageUploadDto, basePath, schema, host);
            if (!image.Success)
            {
                return BadRequest(image);
            }
            var result = await _productManager.UploadImageProduct(id, image.Data.ImageURL);
            // Note: we should call category manager, but ProductManager was used as example. Replace with ICategoryManager if available.
            return Ok(result);
        }
    }
}
