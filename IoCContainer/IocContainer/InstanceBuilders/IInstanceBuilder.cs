namespace IoCContainer.InstanceBuilders
{
    public interface IInstanceBuilder
    {
        object BuildInstance(params object[] args);
    }
}
