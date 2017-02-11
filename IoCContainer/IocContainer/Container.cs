using System;
using System.Collections.Generic;
using IoCContainer.InstanceFactories;
using IoCContainer.Interfaces;

namespace IoCContainer
{
    public class Container
    {
        private readonly Dictionary<Type, BoundImplementation> _bindings = new Dictionary<Type, BoundImplementation>();
        private static readonly TransientInstanceFactory TransientInstanceFactory = new TransientInstanceFactory();

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
                    instanceFactory = TransientInstanceFactory;
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
