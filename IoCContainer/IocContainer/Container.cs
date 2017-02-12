using System;
using System.Collections.Generic;
using IoCContainer.InstanceBuilderFactories;
using IoCContainer.InstanceBuilders;

namespace IoCContainer
{
    public class Container
    {
        internal Dictionary<Type, IInstanceBuilder> Bindings { get; set; }

        private readonly IInstanceBuilderFactory _instanceBuilderFactory;

        public Container(IInstanceBuilderFactory instanceBuilderFactory)
        {
            _instanceBuilderFactory = instanceBuilderFactory;
            Bindings = new Dictionary<Type, IInstanceBuilder>();
        }

        public void Register<TBindTo, TBindFrom>() where TBindFrom : TBindTo
        {
            Register<TBindTo, TBindFrom>(LifecycleType.Transient);
        }

        public void Register<TBindTo, TBindFrom>(LifecycleType lifecycleType) where TBindFrom : TBindTo
        {
            var instanceBuilder = _instanceBuilderFactory.GetInstanceBuilder<TBindFrom>(lifecycleType);
            Bindings.Add(typeof (TBindTo), instanceBuilder);
        }

        public T Resolve<T>() where T : class
        {
            // TODO: handle the type is not in the dictionary

            var instanceBuilder = Bindings[typeof (T)];
            return (T) instanceBuilder.BuildInstance();
        }
    }
}
