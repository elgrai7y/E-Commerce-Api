using System;

namespace E_Commerce.BLL
{
    public class ApplicationRoleReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}