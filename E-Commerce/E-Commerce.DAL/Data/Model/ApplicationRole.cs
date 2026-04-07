using Microsoft.AspNetCore.Identity;

namespace E_Commerce.DAL
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }

    }
}
