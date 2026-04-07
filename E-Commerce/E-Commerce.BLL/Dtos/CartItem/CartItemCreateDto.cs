using System;

namespace E_Commerce.BLL
{
    public class CartItemCreateDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }


    }
}