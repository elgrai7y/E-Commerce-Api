using E_Commerce.Bll;
using E_Commerce.Common.GeneralResult;
using E_Commerce.DAL;

namespace E_Commerce.BLL
{
    public class CartManager : ICartManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GeneralResult<CartReadDto>> GetCartByUserId(Guid userId)
        {
            Cart cart = await _unitOfWork._cartRepository.GetCartWithItemsAsync(userId);
            if (cart is null)
            {
                return GeneralResult<CartReadDto>.NotFound("Cart not found");
            }
            decimal sum = 0;
            foreach (var item in cart.CartItems)
            {
                sum += item.TotalPrice;

            }
            var cartDto = new CartReadDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CreatedAt = cart.CreatedAt,
                UpdatedAt = cart.UpdatedAt,
                TotalPrice = sum,
                ItemsCount = cart.CartItems.Count(),
                CartItems = cart.CartItems.Select(ci => new CartItemReadDto
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice,
                    TotalPrice = ci.TotalPrice
                }).ToList()
            };
            return GeneralResult<CartReadDto>.SuccessResult(cartDto);
        }

        public async Task<GeneralResult<CartItemReadDto>> AddToCart(CartItemCreateDto cartItemDto, Guid userId)
        {
            if (cartItemDto is null)
            {
                return GeneralResult<CartItemReadDto>.NotFound("Product not found");
            }
            Product product = await _unitOfWork._productRepository.GetById(cartItemDto.ProductId);

            if (product is null)
            {
                return GeneralResult<CartItemReadDto>.FailResult("Product not found");
            }


            var cart = await _unitOfWork._cartRepository.GetCartWithItemsAsync(userId);

            if (cart is null)
            {

                cart = new Cart
                {
                    UserId = userId,
                };

                _unitOfWork._cartRepository.Add(cart);
                await _unitOfWork.SaveAsync();
            }
            var cartProduct = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartItemDto.ProductId);
            if (cartProduct is not null)
            {
                cartProduct.Quantity += cartItemDto.Quantity;
                cartProduct.TotalPrice = product.Price * cartProduct.Quantity;

            }

            else
            {
                cartProduct = new CartItem();
                cart.CartItems.Add(new CartItem
                {
                    CartId = cart.Id,
                    ProductId = product.Id,
                    Quantity = cartItemDto.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice =  cartItemDto.Quantity * product.Price,

                });
            }



            await _unitOfWork.SaveAsync();

            var cartItemReadDto = new CartItemReadDto
            {
                ProductId = cartItemDto.ProductId,
                Quantity = cartItemDto.Quantity,
                UnitPrice = product is not null ? product.Price : 0,
                TotalPrice = product is not null ? cartItemDto.Quantity*product.Price : 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return GeneralResult<CartItemReadDto>.SuccessResult(cartItemReadDto, "Product added to cart successfully");
        }

        public async Task<GeneralResult> RemoveFromCart(Guid cartItemId, Guid userId)
        {
            var cart = await _unitOfWork._cartRepository.GetCartWithItemsAsync(userId);
            if (cart is null)
            {
                return GeneralResult.NotFound("Cart not found");
            }
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem is null)
            {
                return GeneralResult.NotFound("Cart item not found");
            }
            cart.CartItems.Remove(cartItem);
            await _unitOfWork.SaveAsync();
            return GeneralResult.SuccessResult("Product removed from cart successfully");
        }
        public async Task<GeneralResult> UpdateQuantity(CartItemEditDto cartItemDto, Guid userId)
        {
            var cart = await _unitOfWork._cartRepository.GetCartWithItemsAsync(userId);
            if (cart is null)
            {
                return GeneralResult.NotFound("Cart not found");
            }
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemDto.Id);
            if (cartItem is null)
            {
                return GeneralResult.NotFound("Cart item not found");
            }
            cartItem.Quantity = cartItemDto.Quantity;
            cartItem.TotalPrice=cartItemDto.Quantity*cartItem.UnitPrice;
            await _unitOfWork.SaveAsync();
            return GeneralResult.SuccessResult("Cart item updated successfully");
        }
    }
}
