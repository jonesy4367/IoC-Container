using System;
using System.Collections.Generic;
using IoCContainer.InstanceFactories;

namespace IoCContainer
{
    public class Container
    {
        //private static readonly Dictionary<Type, Delegate> test;

        private static readonly Dictionary<Type, BoundImplementation> Bindings = new Dictionary<Type, BoundImplementation>();
        private static readonly TransientInstanceFactory TransientInstanceFactory = new TransientInstanceFactory();

        public void Register<TBindTo, TBindFrom>() where TBindFrom : TBindTo
        {
            //var d = Delegate.CreateDelegate(typeof(TBindFrom), TransientInstanceFactory.BuildInstance)
        
            var boundImplementation = new BoundImplementation
            {
                Type = typeof (TBindFrom),
                InstanceFactory = TransientInstanceFactory
            };

            Bindings.Add(typeof (TBindTo), boundImplementation);
        }

        public T Resolve<T>() where T : class
        {
            var bindToType = typeof (T);
            var boundImplementation = Bindings[bindToType];
            var boundType = boundImplementation.Type;
            var instanceFactory = boundImplementation.InstanceFactory;

            return (T) instanceFactory.BuildInstance(boundType);
        }
    }
}
