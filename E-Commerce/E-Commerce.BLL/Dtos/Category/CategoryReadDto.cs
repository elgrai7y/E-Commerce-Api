using E_Commerce.DAL;
using System;

namespace E_Commerce.BLL
{
    public class CategoryReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } =string.Empty;
        public int ProductsCount { get; set; } 
        public IEnumerable<Product>? Products { get; set; } 
        public string UrlImage { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}