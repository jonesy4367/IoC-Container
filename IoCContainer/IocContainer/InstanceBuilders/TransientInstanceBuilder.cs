using System;

namespace IoCContainer.InstanceBuilders
{
    internal class TransientInstanceBuilder<T> : IInstanceBuilder
    {
        public object BuildInstance()
        {
            return Activator.CreateInstance(typeof(T));
        }
    }
}
