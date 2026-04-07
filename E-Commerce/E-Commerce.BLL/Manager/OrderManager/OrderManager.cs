using E_Commerce.Common.GeneralResult;
using E_Commerce.DAL;


namespace E_Commerce.Bll
{
    public class OrderManager : IOrderManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GeneralResult<OrderReadDto>> PlaceOrder(Guid userId, OrderCreateDto orderCreateDto)
        {
            Cart cart = await _unitOfWork._cartRepository.GetCartWithItemsAsync(userId);
            if (cart is null)
            {
                return GeneralResult<OrderReadDto>.NotFound("User Cart not found");
            }
            var cartItem = cart.CartItems.Select(ci=>ci);
            if (cartItem is null)
            {
                return GeneralResult<OrderReadDto>.NotFound("Cart doesn't has items");
            }

            decimal sum = 0;
            foreach (var item in cartItem)
            {
                sum += (item.TotalPrice);
            }
            Order myOrder = new Order
            {

                OrderDate = DateTime.Now,
                TotalAmount = sum,
                Status = "Pending",
                ShippingAddress = orderCreateDto.ShippingAddress,
                City = orderCreateDto.City,
                Country = orderCreateDto.Country,
                UserId = userId,
                OrdersProducts = cartItem.Select(ci => new OrderProduct
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.TotalPrice,
                }).ToList()
            };
            _unitOfWork._orderRepository.Add(myOrder);
            await _unitOfWork.SaveAsync();
            OrderReadDto order = new OrderReadDto
            {
                Id= myOrder.Id,
                OrderDate = DateTime.Now,
                TotalAmount = sum,
                Status = "Pending",
                ShippingAddress = orderCreateDto.ShippingAddress,
                City = orderCreateDto.City,
                Country = orderCreateDto.Country,
                UserId=userId,
                Items = cartItem.Select(ci => new OrderProductReadDto
                {
                    Id=ci.Id,     
                    ProductId=ci.ProductId,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.TotalPrice,
                })
            };
           
            _unitOfWork._cartRepository.DeleteAsync(cart);
            await _unitOfWork.SaveAsync();
            return GeneralResult<OrderReadDto>.SuccessResult(order);
        }

        public async Task<GeneralResult<IEnumerable<OrderReadDto>>> GetOrders(Guid UserId)
        {
            var orders =await _unitOfWork._orderRepository.GetOrderWithProductsAsync(UserId);
            if(orders is  null)
            {
                return GeneralResult<IEnumerable<OrderReadDto>>.NotFound("User Order Not Found");
            }
            // var cartItem = order.TotalAmount
            // decimal sum = 0;
            // foreach (var item in cartItem)
            // {
            //    sum += item.TotalPrice;
            // }
            // if (cartItem is null)
            // {
            //    return GeneralResult<IEnumerable<OrderReadDto>>.NotFound("Cart doesn't has items");
            // }

            var myOrder =orders.Select (o=> new OrderReadDto
            {
                Id = o.Id,
                OrderDate = DateTime.Now,
                TotalAmount = o.TotalAmount,
                Status = "Pending",
                ShippingAddress = o.ShippingAddress,
                City = o.City,
                Country = o.Country,
                UserId = o.UserId,
                Items = o.OrdersProducts.Select(ci => new OrderProductReadDto
                {
                    Id = ci.Id,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.TotalPrice,
                })
            });


            return  GeneralResult<IEnumerable<OrderReadDto>>.SuccessResult(myOrder);
        }

        public async Task<GeneralResult<OrderReadDto>> GetOrderDetails(Guid orderId)
        {
            Order order = await _unitOfWork._orderRepository.GetOrderByIdWithProductsAsync(orderId);
            if (order is null)
            {
                return GeneralResult<OrderReadDto>.NotFound("order not found");
            }
   
            OrderReadDto myOrder = new OrderReadDto
            {
                Id = order.Id,
                OrderDate = DateTime.Now,
                TotalAmount = order.TotalAmount,
                Status = "Pending",
                ShippingAddress = order.ShippingAddress,
                City = order.City,
                Country = order.Country,
                UserId = order.UserId,
                Items = order.OrdersProducts.Select(ci => new OrderProductReadDto
                {
                    Id = ci.Id,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.TotalPrice,
                })
            };

            return GeneralResult<OrderReadDto>.SuccessResult(myOrder);
        }
    }
}
