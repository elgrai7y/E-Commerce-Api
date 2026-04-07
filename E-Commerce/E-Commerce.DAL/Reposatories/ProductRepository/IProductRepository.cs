using E_Commerce.Common;
using E_Commerce.Common.Pagination;

namespace E_Commerce.DAL
{
    public interface IProductRepository : IGeniricRepository<Product>
    {
        Task<Product?> GetProductByIdWithCategoryAsync(Guid id);
        Task<IEnumerable<Product>> GetProductWithCategoryAsync();
        Task<PageResult<Product>> GetProductsWithPaginationAsync(
            PaginationParameters paginationParameters,
                        ProductFiltersParameters? filterParameters = null

            );
    }
}