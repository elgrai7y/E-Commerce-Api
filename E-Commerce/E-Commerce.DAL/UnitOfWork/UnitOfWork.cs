namespace E_Commerce.DAL
{
    public class UnitOfWork : IUnitOfWork
    {

        private   AppDbContext _db;
        public IProductRepository _productRepository { get; }
        public ICategoryRepository _categoryRepository { get; }
        public ICartRepository _cartRepository { get; }
        public ICartItemRepository _cartItemRepository { get; }
        public IOrderRepository _orderRepository { get; }
        public IOrderProductRepository _orderProductRepository { get; }

        public UnitOfWork(AppDbContext db, IProductRepository product, ICategoryRepository category,
            ICartRepository cart, ICartItemRepository cartItem,
            IOrderRepository order, IOrderProductRepository orderProduct
            )
        {
            _db = db;
            _productRepository = product;
            _categoryRepository = category;
            _cartRepository = cart;
            _cartItemRepository = cartItem;
            _orderRepository = order;
            _orderProductRepository = orderProduct;
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
