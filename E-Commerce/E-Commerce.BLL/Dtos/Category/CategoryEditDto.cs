using System;

namespace E_Commerce.BLL
{
    public class CategoryEditDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string UrlImage { get; set; } = string.Empty;
    }
}