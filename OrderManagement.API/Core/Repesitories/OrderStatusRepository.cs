using OrderManagement.API.Core.Repesitories.Base;
using OrderManagement.API.Core.Repesitories.Interface;
using OrderManagement.API.Models.Entity;

namespace OrderManagement.API.Core.Repesitories
{
    public class OrderStatusRepository : Repository<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(OrderManagementContext context) : base(context)
        {

        }
    }
}
