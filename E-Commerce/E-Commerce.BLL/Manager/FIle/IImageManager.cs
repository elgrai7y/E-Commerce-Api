namespace E_Commerce.BLL.Manager
{
    public interface IImageManager
    {
        Task<Common.GeneralResult.GeneralResult<ImageResultDto>> UploadImage(ImageDto imageUploadDto, string basePath, string? schema, string? host);
    }
}