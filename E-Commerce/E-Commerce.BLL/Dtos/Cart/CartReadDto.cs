using E_Commerce.Bll;
using System;
using System.Collections.Generic;

namespace E_Commerce.BLL
{
    public class CartReadDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public int ItemsCount { get; set; } = 0;
        public List<CartItemReadDto> CartItems { get; set; } = new List<CartItemReadDto>();

    }
}