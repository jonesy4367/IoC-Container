using System;
using System.Collections.Generic;
using IoCContainer.Interfaces;

namespace IoCContainer.InstanceFactories
{
    public class SingletonInstanceFactory : IInstanceFactory
    {
        private readonly Dictionary<Type, object> _instantiatedTypes;

        public SingletonInstanceFactory()
        {
            _instantiatedTypes = new Dictionary<Type, object>();
        }

        public object BuildInstance(Type type)
        {
            if (_instantiatedTypes.ContainsKey(type))
            {
                return _instantiatedTypes[type];
            }

            var instance = Activator.CreateInstance(type);
            _instantiatedTypes.Add(type, instance);

            return instance;
        }
    }
}
