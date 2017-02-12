using IoCContainer.InstanceCreators;

namespace IoCContainer.InstanceBuilders
{
    internal class TransientInstanceBuilder<T> : IInstanceBuilder
    {
        private readonly IInstanceCreator _instanceCreator;

        public TransientInstanceBuilder(IInstanceCreator instanceCreator)
        {
            _instanceCreator = instanceCreator;
        }

        public object BuildInstance()
        {
            return _instanceCreator.CreateInstance<T>();
        }
    }
}
