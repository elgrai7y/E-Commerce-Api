using E_Commerce.Common;
using E_Commerce.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Commerce.DAL
{
    public class GeniricRepository<T> : IGeniricRepository<T> where T : class
    {
        protected readonly AppDbContext _db;

        public GeniricRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<T>> GetAllWithException( 
            Expression<Func<T, bool>>? exceptionExpression=null,
            bool isTracking= false
            )
        {
            IQueryable<T> query = _db.Set<T>();

            if (exceptionExpression is not null)
            {
                query = query.Where(exceptionExpression);
            }
            if(!isTracking)
            {
                query= query.AsNoTracking();

            }
            return await query.ToListAsync();

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _db.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<T> GetById(Guid id)
        {
            return await _db.Set<T>().FindAsync(id);
        }
        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }
        public void DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
        }


        public async Task<PageResult<T>> GetWithPaginationAsync(
            PaginationParameters paginationParameters
            )
        {
            IQueryable<T> query = _db.Set<T>().AsQueryable();

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
            return new PageResult<T> { Items = items, MetaData = paginationMetaData };
        }


    }
}
