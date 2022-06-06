using Microsoft.EntityFrameworkCore;
using OrderManagement.API.Core.Repesitories.Base;
using OrderManagement.API.Core.Repesitories.Interface;
using OrderManagement.API.Models.Entity;

namespace OrderManagement.API.Core.Repesitories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderManagementContext context) : base(context)
        {

        }
    }
}
