using System;
using IoCContainer.Interfaces;

namespace IoCContainer
{
    public class BoundImplementation
    {
        public Type Type { get; set; }

        public IInstanceFactory InstanceFactory { get; set; }
    }
}
