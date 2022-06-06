using OrderManagement.API.Core.Repesitories.Base;
using OrderManagement.API.Core.Repesitories.Interface;
using OrderManagement.API.Models.Entity;

namespace OrderManagement.API.Core.Repesitories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(OrderManagementContext context) : base(context)
        {

        }
    }
}
