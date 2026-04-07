namespace E_Commerce.DAL 
{
    public interface IOrderProductRepository
    {
        Task<IEnumerable<OrderProduct>> GetOrderWithProductsAsync();
    }
}