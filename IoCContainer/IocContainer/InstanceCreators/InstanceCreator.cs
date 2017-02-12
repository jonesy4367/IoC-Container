using System;

namespace IoCContainer.InstanceCreators
{
    public class InstanceCreator : IInstanceCreator
    {
        public object CreateInstance<T>(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }
    }
}
