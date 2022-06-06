using OrderManagement.API.Core.Repesitories.Interface;
using OrderManagement.API.Core.UnitOfWork.Base;

namespace OrderManagement.API.Core.UnitOfWork
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IOrderRepository OrderRepository { get; }
        IItemRepository ItemRepository { get; }
        IOrderStatusRepository OrderStatusRepository { get; }
    }
}
