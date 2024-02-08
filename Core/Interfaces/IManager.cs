using Core.Entities;

namespace Core.Interfaces
{
    public interface IManager<T>
    {
        Task<T> Get(string fullPath, string userId, CancellationToken cancellationToken);

        Task<bool> IsExist(string fullPath, string userId, CancellationToken cancellationToken);
    }
}
