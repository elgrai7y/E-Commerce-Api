using E_Commerce.DAL;
using System;

namespace E_Commerce.Bll
{
    public class OrderCreateDto
    {
        public string ShippingAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

    }
}