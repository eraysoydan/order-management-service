using OrderManagement.API.Core.Services.Interface;
using OrderManagement.API.Core.UnitOfWork;
using OrderManagement.API.Models;
using OrderManagement.API.Models.Entity;

namespace OrderManagement.API.Core.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderStatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderStatus>> GetOrderStatusAsync()
        {
            return await _unitOfWork.OrderStatusRepository.GetAllAsync();
        }
    }
}
