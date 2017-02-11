using System;
using IoCContainer.InstanceBuilders;

namespace IoCContainer
{
    public class BoundImplementation
    {
        public Type Type { get; set; }

        public IInstanceBuilder InstanceFactory { get; set; }
    }
}
