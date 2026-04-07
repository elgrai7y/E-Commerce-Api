using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DAL
{
    public class CartItemRepository : GeniricRepository<CartItem>, ICartItemRepository
    {

        public CartItemRepository(AppDbContext db) : base(db)
        {
        }
        public async Task<IEnumerable<CartItem>> GetCartItemWithProductAsync()
        {
            return await _db.Set<CartItem>().Include(c => c.Product).ToListAsync();
        }

    }
}
