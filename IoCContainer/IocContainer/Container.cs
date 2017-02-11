using System;
using System.Collections.Generic;
using IoCContainer.InstanceFactories;
using IoCContainer.Interfaces;

namespace IoCContainer
{
    public class Container
    {
        private readonly Dictionary<Type, BoundImplementation> _bindings;
        private readonly TransientInstanceFactory _transientInstanceFactory;
        private readonly SingletonInstanceFactory _singletonInstanceFactory;

        public Container()
        {
            _bindings = new Dictionary<Type, BoundImplementation>();
            _transientInstanceFactory = new TransientInstanceFactory();
            _singletonInstanceFactory = new SingletonInstanceFactory();
        }

        public void Register<TBindTo, TBindFrom>() where TBindFrom : TBindTo
        {
            Register<TBindTo, TBindFrom>(LifecycleType.Transient);
        }

        public void Register<TBindTo, TBindFrom>(LifecycleType lifecycleType) where TBindFrom : TBindTo
        {
            IInstanceFactory instanceFactory;

            switch (lifecycleType)
            {
                case LifecycleType.Transient:
                    instanceFactory = _transientInstanceFactory;
                    break;
                case LifecycleType.Singleton:
                    instanceFactory = _singletonInstanceFactory;
                    break;
                // TODO: handle this better
                default:
                    throw new Exception();
            }

            var boundImplementation = new BoundImplementation
            {
                Type = typeof(TBindFrom),
                InstanceFactory = instanceFactory
            };

            _bindings.Add(typeof(TBindTo), boundImplementation);
        }

        public T Resolve<T>() where T : class
        {
            var bindToType = typeof (T);
            var boundImplementation = _bindings[bindToType];
            var boundType = boundImplementation.Type;
            var instanceFactory = boundImplementation.InstanceFactory;

            return (T) instanceFactory.BuildInstance(boundType);
        }
    }
}
