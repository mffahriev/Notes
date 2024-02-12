namespace Core.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> Get(Guid Id, CancellationToken token);

        Task Insert(T entity, CancellationToken token);

        Task Delete(T entity, CancellationToken token);

        Task Update(T entity, CancellationToken token);
    }
}
