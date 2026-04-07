using System;

namespace E_Commerce.BLL
{
    public class CategoryCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string UrlImage { get; set; } = string.Empty;
    }
}