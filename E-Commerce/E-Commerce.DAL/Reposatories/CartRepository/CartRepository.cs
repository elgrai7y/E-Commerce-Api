using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DAL
{
    public class CartRepository : GeniricRepository<Cart>, ICartRepository
    {

        public CartRepository(AppDbContext db) : base(db)
        {
        }
        public async Task<Cart> GetCartWithItemsAsync(Guid userId)
        {
            return await _db.Set<Cart>().Include(c => c.CartItems).FirstOrDefaultAsync(c=>c.UserId==userId)!;
        }

    }
}
