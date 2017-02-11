using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}