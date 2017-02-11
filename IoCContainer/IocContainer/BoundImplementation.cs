using System;
using IocContainer.Interfaces;

namespace IocContainer
{
    public class BoundImplementation
    {
        public Type Type { get; set; }

        public IInstanceFactory InstanceFactory { get; set; }
    }
}
