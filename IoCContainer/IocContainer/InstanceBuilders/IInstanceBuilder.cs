using System;

namespace IoCContainer.InstanceBuilders
{
    public interface IInstanceBuilder
    {
        object BuildInstance();
    }
}
