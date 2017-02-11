using IoCContainer.InstanceBuilders;

namespace IoCContainer.InstanceBuilderFactories
{
    public interface IInstanceBuilderFactory
    {
        IInstanceBuilder GetInstanceBuilder<T>(LifecycleType lifecycleType);
    }
}
