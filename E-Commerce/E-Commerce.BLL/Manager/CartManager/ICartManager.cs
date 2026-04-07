using E_Commerce.Bll;
using E_Commerce.Common.GeneralResult;
using E_Commerce.DAL;

namespace E_Commerce.BLL
{
    public interface ICartManager
    {
        Task<GeneralResult<CartReadDto>> GetCartByUserId(Guid userId);
        Task<GeneralResult<CartItemReadDto>> AddToCart(CartItemCreateDto cartItem, Guid userId);
        Task<GeneralResult> RemoveFromCart(Guid cartItemId, Guid userId);
        Task<GeneralResult> UpdateQuantity(CartItemEditDto cartItemDto, Guid userId);


    }
}