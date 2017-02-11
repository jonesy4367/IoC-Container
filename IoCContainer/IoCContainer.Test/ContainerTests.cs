using NUnit.Framework;

namespace IoCContainer.Test
{
    [TestFixture]
    public class ContainerTests
    {
        private interface IBoundTo { }

        private class ParameterlessTarget : IBoundTo { }

        private Container _iocContainer;

        [SetUp]
        public void Setup()
        {
            _iocContainer = new Container();
        }

        [Test]
        public void Resolve_DefaultLifeCycleParameterlessConstructor_ReturnsInstance()
        {
            // Arrange
            _iocContainer.Register<IBoundTo, ParameterlessTarget>();

            // Act
            var result1 = _iocContainer.Resolve<IBoundTo>();
            var result2 = _iocContainer.Resolve<IBoundTo>();

            // Assert
            Assert.IsInstanceOf<ParameterlessTarget>(result1);
            Assert.IsInstanceOf<ParameterlessTarget>(result2);
            Assert.AreNotSame(result1, result2);
        }

        [Test]
        [TestCase(LifecycleType.Transient)]
        [TestCase(LifecycleType.Singleton)]
        public void Resolve_ParameterlessConstructor_ReturnsInstance(LifecycleType lifecycleType)
        {
            // Arrange
            _iocContainer.Register<IBoundTo, ParameterlessTarget>(lifecycleType);

            // Act
            var result1 = _iocContainer.Resolve<IBoundTo>();
            var result2 = _iocContainer.Resolve<IBoundTo>();

            // Assert
            Assert.IsInstanceOf<ParameterlessTarget>(result1);
            Assert.IsInstanceOf<ParameterlessTarget>(result2);

            switch (lifecycleType)
            {
                case LifecycleType.Transient:
                    Assert.AreNotSame(result1, result2);
                    break;
                case LifecycleType.Singleton:
                    Assert.AreSame(result1, result2);
                    break;
            }
        }
    }
}
