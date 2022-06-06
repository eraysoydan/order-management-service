using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Core.Services.Interface;
using OrderManagement.API.Models.Entity;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusService _orderStatusService;
        public OrderStatusController(IOrderStatusService orderStatusService)
        {
            _orderStatusService = orderStatusService;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderStatus>> GetOrderStatus()
        {
            return await _orderStatusService.GetOrderStatusAsync();
        }
    }
}
