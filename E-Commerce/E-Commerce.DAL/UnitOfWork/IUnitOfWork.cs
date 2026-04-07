namespace E_Commerce.DAL
{
    public interface IUnitOfWork
    {
        public IProductRepository _productRepository { get; }
        public  ICategoryRepository _categoryRepository { get; }
        public  ICartRepository _cartRepository { get; }
        public  ICartItemRepository _cartItemRepository { get; }
        public  IOrderRepository _orderRepository { get; }
        public  IOrderProductRepository _orderProductRepository { get; }
        Task SaveAsync();
    }
}