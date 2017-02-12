using NUnit.Framework;
using System.Linq;
using IoCContainer.InstanceBuilderFactories;
using IoCContainer.InstanceBuilders;
using Moq;

namespace IoCContainer.Test
{
    [TestFixture]
    public class ContainerTests
    {
        private Container _container;
        private Mock<IInstanceBuilderFactory> _instanceBuilderFactory;

        private interface ISomeInterface { }
        private class SomeClass : ISomeInterface { }
        
        [SetUp]
        public void Setup()
        {
            _instanceBuilderFactory = new Mock<IInstanceBuilderFactory>();
            _container = new Container(_instanceBuilderFactory.Object);
        }

        #region Register() Tests

        [Test]
        public void Register_TypeIsRegistered()
        {
            // Arrange
            const LifecycleType lifecycleType = LifecycleType.Singleton;
            var expectedInstanceBuilder = new TransientInstanceBuilder<SomeClass>();

            _instanceBuilderFactory
                .Setup(i => i.GetInstanceBuilder<SomeClass>(lifecycleType))
                .Returns(expectedInstanceBuilder);

            // Act
            _container.Register<ISomeInterface, SomeClass>(lifecycleType);

            // Assert
            var actualInstanceBuilder = _container.Bindings[typeof(ISomeInterface)];
            Assert.AreSame(expectedInstanceBuilder, actualInstanceBuilder);
        }

        #endregion

        //[Test]
        //public void Resolve_DefaultLifeCycleParameterlessConstructor_ReturnsInstance()
        //{
        //    // Arrange
        //    _iocContainer.Register<IBoundTo, ParameterlessTarget>();

        //    // Act
        //    var result1 = _iocContainer.Resolve<IBoundTo>();
        //    var result2 = _iocContainer.Resolve<IBoundTo>();

        //    // Assert
        //    Assert.IsInstanceOf<ParameterlessTarget>(result1);
        //    Assert.IsInstanceOf<ParameterlessTarget>(result2);
        //    Assert.AreNotSame(result1, result2);
        //}

        //[Test]
        //[TestCase(LifecycleType.Transient)]
        //[TestCase(LifecycleType.Singleton)]
        //public void Resolve_ParameterlessConstructor_ReturnsInstance(LifecycleType lifecycleType)
        //{
        //    // Arrange
        //    _iocContainer.Register<IBoundTo, ParameterlessTarget>(lifecycleType);

        //    // Act
        //    var result1 = _iocContainer.Resolve<IBoundTo>();
        //    var result2 = _iocContainer.Resolve<IBoundTo>();

        //    // Assert
        //    Assert.IsInstanceOf<ParameterlessTarget>(result1);
        //    Assert.IsInstanceOf<ParameterlessTarget>(result2);

        //    switch (lifecycleType)
        //    {
        //        case LifecycleType.Transient:
        //            Assert.AreNotSame(result1, result2);
        //            break;
        //        case LifecycleType.Singleton:
        //            Assert.AreSame(result1, result2);
        //            break;
        //    }
        //}
    }
}
