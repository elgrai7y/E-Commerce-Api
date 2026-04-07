using System;

namespace E_Commerce.BLL
{
    public class ApplicationUserEditDto
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}