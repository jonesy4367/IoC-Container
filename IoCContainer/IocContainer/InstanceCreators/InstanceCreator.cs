using System;

namespace IoCContainer.InstanceCreators
{
    public class InstanceCreator : IInstanceCreator
    {
        public object CreateInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
