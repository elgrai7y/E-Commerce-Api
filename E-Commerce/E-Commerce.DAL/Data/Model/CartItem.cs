using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL

{
    public class CartItem : IAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Cart")]
        public Guid CartId { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public int Quantity { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
