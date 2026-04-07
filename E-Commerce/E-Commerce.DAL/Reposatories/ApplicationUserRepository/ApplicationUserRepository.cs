using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL
{
    public class ApplicationUserRepository : GeniricRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(AppDbContext db) : base(db)
        {
        }
        public async Task<IEnumerable<ApplicationUser>> GetUserWithOrdersAsync()
        {
            return await _db.Set<ApplicationUser>().Include(u => u.Orders).ThenInclude(o => o.OrdersProducts).ToListAsync();
        }
    }
}
