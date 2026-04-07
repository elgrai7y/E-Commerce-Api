namespace E_Commerce.DAL
{
    public interface ICartItemRepository
    {
        Task<IEnumerable<CartItem>> GetCartItemWithProductAsync();
    }
}