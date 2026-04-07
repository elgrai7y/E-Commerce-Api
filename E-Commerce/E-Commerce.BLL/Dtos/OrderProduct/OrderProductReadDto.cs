using System;

namespace E_Commerce.Bll
{
    public class OrderProductReadDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}