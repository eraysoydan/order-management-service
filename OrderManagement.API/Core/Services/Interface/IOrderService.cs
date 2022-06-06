using OrderManagement.API.Models;
using OrderManagement.API.Models.Entity;

namespace OrderManagement.API.Core.Services.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<GetOrdersResponseModel>> GetOrdersAsync();

        Task<CreateOrderResponseModel> AddOrdersAsync(CreateOrderRequestModel orderRequests);

        Task<OrderStatusUpdateResponseModel> UpdateOrderStatusAsync(OrderStatusUpdateRequestModel orderStatusUpdateRequest);
    }
}
