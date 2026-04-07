using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL
{ 
    public class OrderProductRepository : GeniricRepository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(AppDbContext db) : base(db)
        {
        }
        public async Task<IEnumerable<OrderProduct>> GetOrderWithProductsAsync()
        {
            return await _db.Set<OrderProduct>().Include(op => op.Product)
                                        .ToListAsync();
        }

    }
}
