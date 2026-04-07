using E_Commerce.Common.GeneralResult;

namespace E_Commerce.Bll
{
    public interface IOrderManager
    {
        Task<GeneralResult<IEnumerable<OrderReadDto> >> GetOrders(Guid UserId);
        Task<GeneralResult<OrderReadDto>> GetOrderDetails(Guid orderId);


        Task<GeneralResult<OrderReadDto>> PlaceOrder(Guid userId, OrderCreateDto orderCreateDto);
    }
}