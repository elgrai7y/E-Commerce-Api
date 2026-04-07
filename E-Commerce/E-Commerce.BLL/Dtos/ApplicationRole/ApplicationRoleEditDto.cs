using System;

namespace E_Commerce.BLL
{
    public class ApplicationRoleEditDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}