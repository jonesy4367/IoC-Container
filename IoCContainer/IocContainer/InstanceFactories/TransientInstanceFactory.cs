using System;
using IoCContainer.Interfaces;

namespace IoCContainer.InstanceFactories
{
    public class TransientInstanceFactory : IInstanceFactory
    {
        public object BuildInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
