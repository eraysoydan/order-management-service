namespace OrderManagement.API.Core.UnitOfWork.Base
{
    public interface IBaseUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
    }
}
