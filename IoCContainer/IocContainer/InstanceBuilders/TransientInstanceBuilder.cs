using System;

namespace IoCContainer.InstanceBuilders
{
    public class TransientInstanceBuilder<T> : IInstanceBuilder
    {
        public object BuildInstance()
        {
            return Activator.CreateInstance(typeof(T));
        }
    }
}
