namespace E_Commerce.DAL 
{
    public interface IOrderRepository: IGeniricRepository<Order>
    {
        Task< IEnumerable<Order>> GetOrderWithProductsAsync(Guid userId);
        Task<Order> GetOrderByIdWithProductsAsync(Guid orderId);

    }
}