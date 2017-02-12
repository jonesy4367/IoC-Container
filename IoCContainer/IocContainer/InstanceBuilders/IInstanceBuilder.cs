using System;

namespace IoCContainer.InstanceBuilders
{
    public interface IInstanceBuilder
    {
        Type GetInstanceType();

        object BuildInstance(params object[] args);
    }
}
