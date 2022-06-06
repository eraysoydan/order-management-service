using OrderManagement.API.Models;
using OrderManagement.API.Models.Entity;

namespace OrderManagement.API.Core.Services.Interface
{
    public interface IOrderStatusService
    {
        Task<IEnumerable<OrderStatus>> GetOrderStatusAsync();
    }
}