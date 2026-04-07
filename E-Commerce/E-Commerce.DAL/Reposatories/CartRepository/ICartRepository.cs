namespace E_Commerce.DAL
{
    public interface ICartRepository:IGeniricRepository<Cart>
    {
        Task<Cart> GetCartWithItemsAsync(Guid userId);
    }
}