using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.DAL
{
    public class Product : IAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;

        public string UrlImage { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public int Reviews { get; set; }
        public int Stock { get; set; }
        public float Rate { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }


        public virtual ICollection<OrderProduct> OrdersProducts { get; set; } = new HashSet<OrderProduct>();
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();


        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

