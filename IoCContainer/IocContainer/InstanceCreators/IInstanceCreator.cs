namespace IoCContainer.InstanceCreators
{
    public interface IInstanceCreator
    {
        object CreateInstance<T>();
    }
}
