using E_Commerce.Common;
using E_Commerce.Common.GeneralResult;
using E_Commerce.Common.Pagination;
using E_Commerce.DAL;

namespace E_Commerce.BLL
{
    public interface ICategoryManager
    {
        Task<GeneralResult<CategoryCreateDto>> CreateCategory(CategoryCreateDto categoryCreateDto);
        Task<GeneralResult> DeleteCategory(Guid id);
        Task<GeneralResult> EditCategory(CategoryEditDto categoryEditDto);
        Task<GeneralResult<IEnumerable<CategoryReadDto>>> GetAllCategory();
        Task<GeneralResult<CategoryReadDto>> GetCategoryById(Guid id);
        Task<GeneralResult<IEnumerable<ProductReadDto>>> GetCategorysProductsById(Guid id);
        Task<GeneralResult<PageResult<Category>>> GetAllCategoryWithPagination(PaginationParameters paginationParameters);
        Task<GeneralResult<ImageResultDto>> UploadImageCategory(Guid id, string urlImage);

    }
}