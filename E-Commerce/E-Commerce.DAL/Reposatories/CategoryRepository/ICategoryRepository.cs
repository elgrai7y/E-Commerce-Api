namespace E_Commerce.DAL 
{
    public interface ICategoryRepository : IGeniricRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoryWithPoductsAsync();
        Task<Category> GetCategoryWithPoductsByIdAsync(Guid id);

    }
}