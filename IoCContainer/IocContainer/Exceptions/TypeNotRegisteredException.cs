using System;

namespace IoCContainer.Exceptions
{
    public class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException() { }

        public TypeNotRegisteredException(string message)
            : base(message) { }
    }
}
