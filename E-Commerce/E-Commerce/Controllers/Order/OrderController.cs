using E_Commerce.Bll;
using E_Commerce.Common;
using E_Commerce.Common.GeneralResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        public OrderController(IOrderManager orderManager)
        {
            _orderManager=orderManager;

        }
        [HttpPost]
        public  async Task<ActionResult> PlaceOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            var userId = User.GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized();
            }
            var results =await _orderManager.PlaceOrder(userId, orderCreateDto);
            if(!results.Success)
            {
                return BadRequest(results);
            }
            return Ok(results);
        }

        [HttpGet]

        public async Task<ActionResult> GetOrders()
        {
            var userId = User.GetUserId();
            if(userId == Guid.Empty)
            {
                return Unauthorized();
            }
            var results = await _orderManager.GetOrders(userId);
           
            return Ok(results);
        }
        [HttpGet("Details/{orderId:Guid}")]

        public async Task<ActionResult> GetOrderDetails([FromRoute] Guid orderId)
        {
            var results = await _orderManager.GetOrderDetails(orderId);

            return Ok(results);
        }
    }
}
