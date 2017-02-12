using System;
using IoCContainer.InstanceBuilderFactories;
using IoCContainer.InstanceBuilders;
using NUnit.Framework;

namespace IoCContainer.Test.InstanceBuilderFactories
{
    [TestFixture]
    public class InstanceBuilderFactoryTests
    {
        private InstanceBuilderFactory _instanceBuilderFactory;

        [SetUp]
        public void Setup()
        {
            _instanceBuilderFactory = new InstanceBuilderFactory();
        }

        [Test]
        public void GetInstanceBuilder_LifeCycleTypeIsTransient_ReturnsTransientInstanceBuilder()
        {
            // Act
            var result = _instanceBuilderFactory.GetInstanceBuilder<string>(LifecycleType.Transient);

            // Assert
            Assert.IsInstanceOf<TransientInstanceBuilder<string>>(result);
        }

        [Test]
        public void GetInstanceBuilder_LifeCycleTypeIsSingleton_ReturnsSingletonInstanceBuilder()
        {
            // Act
            var result = _instanceBuilderFactory.GetInstanceBuilder<string>(LifecycleType.Singleton);

            // Assert
            Assert.IsInstanceOf<SingletonInstanceBuilder<string>>(result);
        }

        [Test]
        public void GetInstanceBuilder_LifeCycleTypeIsNotHandled_ThrowsArgumentException()
        {
            // Arrange
            const LifecycleType lifecycleType = (LifecycleType) 999;
            var expectedMessage = $"Could not find an InstanceBuilder for {lifecycleType}";

            // Act, Assert
            var exception = Assert.Throws<ArgumentException>(delegate
            {
                _instanceBuilderFactory.GetInstanceBuilder<string>(lifecycleType);
            });

            Assert.AreEqual(expectedMessage, exception.Message);
        }
    }
}