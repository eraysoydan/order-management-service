using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Common.Validators;
using OrderManagement.API.Core.Services.Interface;
using OrderManagement.API.Models;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetOrdersResponseModel>> GetOrders()
        {
            return await _orderService.GetOrdersAsync();
        }

        [HttpPost]
        public async Task<CreateOrderResponseModel> CreateOrder([FromBody] CreateOrderRequestModel orderRequest)
        {
            var orderResponse = await _orderService.AddOrdersAsync(orderRequest);
            return orderResponse;
        }

        [HttpPost]
        public async Task<OrderStatusUpdateResponseModel> UpdateOrderStatus([FromBody] OrderStatusUpdateRequestModel orderStatusUpdateRequest)
        {
            var orderStatusUpdateResponse = await _orderService.UpdateOrderStatusAsync(orderStatusUpdateRequest);
            return orderStatusUpdateResponse;
        }
    }
}
