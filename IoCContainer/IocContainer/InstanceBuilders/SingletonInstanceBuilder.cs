using System;
using IoCContainer.InstanceCreators;

namespace IoCContainer.InstanceBuilders
{
    internal class SingletonInstanceBuilder<T> : IInstanceBuilder
    {
        private T _instance;

        private readonly IInstanceCreator _instanceCreator;

        public SingletonInstanceBuilder(IInstanceCreator instanceCreator)
        {
            _instanceCreator = instanceCreator;
        }
        
        public object BuildInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }

            _instance = (T) Activator.CreateInstance(typeof(T));
            return _instance;
        }
    }
}
