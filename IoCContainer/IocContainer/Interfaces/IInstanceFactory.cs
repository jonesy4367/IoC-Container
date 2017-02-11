using System;

namespace IoCContainer.Interfaces
{
    public interface IInstanceFactory
    {
        object BuildInstance(Type type);
    }
}
