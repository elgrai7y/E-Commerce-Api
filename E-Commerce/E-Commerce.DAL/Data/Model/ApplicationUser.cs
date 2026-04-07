using Microsoft.AspNetCore.Identity;

namespace E_Commerce.DAL
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Country { get; set; }
        public string City { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual Cart Cart { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
