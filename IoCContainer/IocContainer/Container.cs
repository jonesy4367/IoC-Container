using System;
using System.Collections.Generic;
using System.Linq;
using IoCContainer.Exceptions;
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
            return (T) Resolve(typeof (T));
        }

        private object Resolve(Type type)
        {
            if (!Bindings.ContainsKey(type))
            {
                throw new TypeNotRegisteredException($"The type '{type.FullName}' has not been registerd.");
            }
            
            var instanceBuilder = Bindings[type];
            var instanceType = instanceBuilder.GetInstanceType();

            var constructors = instanceType.GetConstructors();
            var parameters = constructors.First().GetParameters();

            if (!parameters.Any())
            {
                return instanceBuilder.BuildInstance();
            }

            var args = parameters
                .Select(p => Resolve(p.ParameterType))
                .ToList();

            return instanceBuilder.BuildInstance(args.ToArray());
        }
    }
}
