using System;
using IoCContainer.InstanceBuilders;

namespace IoCContainer.InstanceBuilderFactories
{
    public class InstanceBuilderFactory : IInstanceBuilderFactory
    {
        public IInstanceBuilder GetInstanceBuilder<T>(LifecycleType lifecycleType)
        {
            switch (lifecycleType)
            {
                case LifecycleType.Transient:
                    return new TransientInstanceBuilder<T>();
                case LifecycleType.Singleton:
                    return new SingletonInstanceBuilder<T>();
                // TODO: handle this better
                default:
                    throw new Exception();
            }
        }
    }
}
