using E_Commerce.Common;
using E_Commerce.Common.GeneralResult;
using E_Commerce.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_Commerce.DAL
{
    public class ProductRepository : GeniricRepository<Product>, IProductRepository
    {

        public ProductRepository(AppDbContext db) : base(db)
        {
        }
        public async Task<IEnumerable<Product>> GetProductWithCategoryAsync()
        {
            return await _db.Set<Product>().Include(p => p.Category).ToListAsync();
        }
        public async Task<Product?> GetProductByIdWithCategoryAsync(Guid id)
        {
            return await _db.Set<Product>()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id)!;
        }
        
        public async Task<PageResult<Product>> GetProductsWithPaginationAsync( 
            PaginationParameters paginationParameters,
            ProductFiltersParameters? filterParameters=null
            )
        {
            IQueryable<Product> query = _db.Set<Product>().AsQueryable();
            query.Include(p => p.Category);
            query = AddFilters(filterParameters, query);

            var totalCount = await query.CountAsync();
            var pageNumber = paginationParameters?.PageNumber ?? 1;
            var pageSize = paginationParameters?.PageSize ?? totalCount;

            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 50);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var hasNext = pageNumber < totalPages;
            var hasPrevious = pageNumber > 1;

            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            PaginationMetaData paginationMetaData = new PaginationMetaData
            {
                TotalCount = totalCount,    
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = totalPages,
                HasNext = hasNext,
                HasPrev = hasPrevious
            };
            return new PageResult<Product> { Items=items , MetaData=paginationMetaData} ;
        }

        private IQueryable<Product> AddFilters(ProductFiltersParameters filter, IQueryable<Product> query)
        {
            if(filter.MaxPrice > 0)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice);
            }
            if(filter.MinPrice > 0)
            {
                query = query.Where(p => p.Price >= filter.MinPrice);
            }
            if(filter.Search!=null)
            {
                query = query.Where(p => p.Title.Contains(filter.Search) || p.Description.Contains(filter.Search));
            }
         return query;
        }
    }
}
