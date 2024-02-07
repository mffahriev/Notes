namespace Core.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> Get(Guid Id);

        Task Insert(T entity);

        Task Delete(Guid Id);

        Task Update(T entity);
    }
}
