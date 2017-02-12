using System;
using IoCContainer.InstanceCreators;

namespace IoCContainer.InstanceBuilders
{
    internal class TransientInstanceBuilder<T> : IInstanceBuilder
    {
        private readonly IInstanceCreator _instanceCreator;

        public TransientInstanceBuilder(IInstanceCreator instanceCreator)
        {
            _instanceCreator = instanceCreator;
        }

        public Type GetInstanceType()
        {
            return typeof (T);
        }

        public object BuildInstance(params object[] args)
        {
            return _instanceCreator.CreateInstance<T>(args);
        }
    }
}
