using OrderManagement.API.Models.Entity;

namespace OrderManagement.API.Core.UnitOfWork.Base
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        protected readonly OrderManagementContext dbContext;
        public BaseUnitOfWork(OrderManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CommitAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
                dbContext.Dispose();

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
