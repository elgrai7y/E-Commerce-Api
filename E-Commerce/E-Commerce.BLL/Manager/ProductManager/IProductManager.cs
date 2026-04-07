using E_Commerce.Common;
using E_Commerce.Common.GeneralResult;
using E_Commerce.Common.Pagination;
using E_Commerce.DAL;

namespace E_Commerce.BLL
{
    public interface IProductManager
    {
        Task<GeneralResult<IEnumerable<ProductReadDto>>> GetAllProduct();
        Task<GeneralResult<ProductReadDto>> GetProductById(Guid id);
        Task<GeneralResult<ProductCreateDto>> CreateProduct(ProductCreateDto productCreateDto);
        Task<GeneralResult<ProductEditDto>> EditeProduct(ProductEditDto productEditDto);
        Task<GeneralResult<ProductReadDto>> DeleteProduct(Guid id);
        Task<GeneralResult<PageResult<Product>>> GetAllProductWithPagination(PaginationParameters paginationParameters, ProductFiltersParameters ProductFiltersParameters);
        Task<GeneralResult<ImageResultDto>> UploadImageProduct(Guid id, String urlImage);


    }
}