using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DAL 
{
    public class OrderRepository : GeniricRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext db) : base(db)
        {
        }
        public async Task<IEnumerable<Order> > GetOrderWithProductsAsync(Guid userID)
        {
            return await _db.Set<Order>().Where(o => o.UserId == userID).Include(o => o.OrdersProducts)
                .ThenInclude(op => op.Product).ToListAsync();                             
        }
        public async Task<Order> GetOrderByIdWithProductsAsync(Guid orderId)
        {
            return await _db.Set<Order>().Include(o => o.OrdersProducts)
                .ThenInclude(op => op.Product).FirstOrDefaultAsync(o => o.Id == orderId);
        }

    }
}