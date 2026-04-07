using E_Commerce.Common;
using E_Commerce.Common.Pagination;

namespace E_Commerce.DAL
{
    public interface IGeniricRepository<T>
    {
        void Add(T entity);
        void DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<PageResult<T>> GetWithPaginationAsync(PaginationParameters paginationParameters
            );

    }
}