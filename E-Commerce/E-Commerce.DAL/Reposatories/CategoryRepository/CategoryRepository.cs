using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL 
{
    public class CategoryRepository : GeniricRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(AppDbContext db) : base(db)
        {
        }
        public async Task<IEnumerable<Category>> GetCategoryWithPoductsAsync()
        {
            return await _db.Set<Category>().Include(c => c.Products).ToListAsync();
        }
        public async Task<Category> GetCategoryWithPoductsByIdAsync(Guid id)
        {
            return await _db.Set<Category>().Include(c => c.Products).FirstOrDefaultAsync(c=>c.Id==id)!;
        }

    }
}
