using E_Commerce.Bll;
using E_Commerce.BLL;
using E_Commerce.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers.Cart
{


    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartManager _cartManager;
        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }
        [HttpGet]
        public async Task<ActionResult> GetCartByUserId()
        {
            var userId = User.GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized();
            }
            var result = await _cartManager.GetCartByUserId(userId);
            return Ok(result);

        }
        [HttpPost("add-to-cart")]
        public async Task<ActionResult> AddToCart([FromBody] CartItemCreateDto cartItemDto)
        {
            var userId = User.GetUserId();


            var result = await _cartManager.AddToCart(cartItemDto, userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("delete-from-cart/{cartItemId:Guid}")]
        public async Task<ActionResult> DeleteFromCart([FromRoute] Guid cartItemId)
        {
            var userId = User.GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized();
            }
            var result = await _cartManager.RemoveFromCart(cartItemId, userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<ActionResult> UpdateCartQuantity([FromBody] CartItemEditDto cartItem)
        {
            var userId = User.GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized();
            }
            var result = await _cartManager.UpdateQuantity(cartItem, userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
