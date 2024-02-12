using Core.Entities;

namespace Core.Interfaces
{
    public interface IPathManager<T>
    {
        Task<T> Get(string fullPath, string userId, CancellationToken cancellationToken);
    }
}
