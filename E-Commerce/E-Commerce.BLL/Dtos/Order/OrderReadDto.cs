using System;
using System.Collections.Generic;

namespace E_Commerce.Bll
{
    public class OrderReadDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<OrderProductReadDto>? Items { get; set; }
    }
}