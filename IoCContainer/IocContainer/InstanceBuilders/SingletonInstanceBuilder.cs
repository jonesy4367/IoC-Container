﻿using System;
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

            var instance = _instanceCreator.CreateInstance<T>();
            _instance = (T) instance;
            return instance;
        }
    }
}
