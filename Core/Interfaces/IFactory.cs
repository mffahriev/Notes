namespace Core.Interfaces
{
    public interface IFactory<T> where T : class
    {
        T Create();
    }

    public interface IFactory<T1, T2> where T1 : class
    {
        T1 Create(T2 options);
    }
}
