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

        public object BuildInstance()
        {
            //return Activator.CreateInstance(typeof(T));

            return null;
        }
    }
}
