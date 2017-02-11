namespace IocContainer.Interfaces
{
    public interface IInstanceFactory
    {
        T BuildInstance<T>();
    }
}
