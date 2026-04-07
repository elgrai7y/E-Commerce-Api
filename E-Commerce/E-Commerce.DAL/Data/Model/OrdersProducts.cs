using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL
{
    public class OrderProduct : IAuditableEntity
    {
        public Guid Id { get; set; }

        [ForeignKey("Order")]

        public Guid OrderId{ get;set; }
        public Order Order { get; set; }


        [ForeignKey("Product")]
        public Guid ProductId{ get;set; }
        public Product Product { get;set; }
        public decimal TotalPrice { get; set; }

        public int Quantity { get; set; }

       

       

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
