﻿using System;
using IoCContainer.InstanceBuilders;
using IoCContainer.InstanceCreators;

namespace IoCContainer.InstanceBuilderFactories
{
    public class InstanceBuilderFactory : IInstanceBuilderFactory
    {
        public IInstanceBuilder GetInstanceBuilder<T>(LifecycleType lifecycleType)
        {
            switch (lifecycleType)
            {
                case LifecycleType.Transient:
                    return new TransientInstanceBuilder<T>(new InstanceCreator());
                case LifecycleType.Singleton:
                    return new SingletonInstanceBuilder<T>(new InstanceCreator());
                default:
                    throw new NotSupportedException($"Could not find an InstanceBuilder for {lifecycleType}");
            }
        }
    }
}
