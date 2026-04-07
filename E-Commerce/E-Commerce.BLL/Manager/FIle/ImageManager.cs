using E_Commerce.Common.GeneralResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static E_Commerce.BLL.Manager.ImageManager;

namespace E_Commerce.BLL.Manager
{

    public class ImageManager :  IImageManager
    {
        public ImageManager()
        {
        }

        public async Task<GeneralResult<ImageResultDto>> UploadImage(ImageDto imageUploadDto, string basePath,
            string? schema,
            string? host)
        {
            if (string.IsNullOrWhiteSpace(schema) || string.IsNullOrWhiteSpace(host))
            {
                return GeneralResult<ImageResultDto>.FailResult("Missing schema or host");
            }

            var file = imageUploadDto.File;
            var fileExtention = Path.GetExtension(file.FileName).ToLower();
            var cleanName = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "-").ToLower();
            var newFileName = $"{cleanName}-{Guid.NewGuid()}-{fileExtention}";
            var directoryPath = Path.Combine(basePath, "Files");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fullFilePath = Path.Combine(directoryPath, newFileName);
            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var url = $"{schema}://{host}/Files/{newFileName}";
            var imageUploadResultDto = new ImageResultDto(url);
            return GeneralResult<ImageResultDto>.SuccessResult(imageUploadResultDto);


        }
    }

}
