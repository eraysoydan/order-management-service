using OrderManagement.API.Core.Repesitories;
using OrderManagement.API.Core.Repesitories.Interface;
using OrderManagement.API.Core.UnitOfWork.Base;
using OrderManagement.API.Models.Entity;

namespace OrderManagement.API.Core.UnitOfWork
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        public UnitOfWork(OrderManagementContext dbContext) : base(dbContext)
        {

        }

        public IOrderRepository OrderRepository { get { return new OrderRepository(dbContext); } }
        public IItemRepository ItemRepository { get { return new ItemRepository(dbContext); } }
        public IOrderStatusRepository OrderStatusRepository { get { return new OrderStatusRepository(dbContext); } }
    }
}
