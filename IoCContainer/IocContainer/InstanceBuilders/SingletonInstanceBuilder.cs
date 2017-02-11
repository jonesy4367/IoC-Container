using System;

namespace IoCContainer.InstanceBuilders
{
    public class SingletonInstanceBuilder<T> : IInstanceBuilder
    {
        private T _instance;

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
