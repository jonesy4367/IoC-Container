using NUnit.Framework;
using System.Linq;
using IoCContainer.InstanceBuilderFactories;
using IoCContainer.InstanceBuilders;
using IoCContainer.InstanceCreators;
using Moq;

namespace IoCContainer.Test
{
    [TestFixture]
    public class ContainerTests
    {
        private Container _container;
        private Mock<IInstanceBuilderFactory> _instanceBuilderFactory;

        private interface ISomeInterface { }
        private class ClassWithParameterlessConstructor : ISomeInterface { }
        
        [SetUp]
        public void Setup()
        {
            _instanceBuilderFactory = new Mock<IInstanceBuilderFactory>();
            _container = new Container(_instanceBuilderFactory.Object);
        }

        #region Register() Tests

        [Test]
        public void Register_LifecycleTypeIsNotProvided_TypeIsRegistered()
        {
            // Arrange
            var expectedInstanceBuilder =
                new TransientInstanceBuilder<ClassWithParameterlessConstructor>(new InstanceCreator());

            _instanceBuilderFactory
                .Setup(i => i.GetInstanceBuilder<ClassWithParameterlessConstructor>(LifecycleType.Transient))
                .Returns(expectedInstanceBuilder);

            // Act
            _container.Register<ISomeInterface, ClassWithParameterlessConstructor>();

            // Assert
            var actualInstanceBuilder = _container.Bindings[typeof(ISomeInterface)];
            Assert.AreSame(expectedInstanceBuilder, actualInstanceBuilder);
        }

        [Test]
        public void Register_LifecycleTypeIsProvided_TypeIsRegistered()
        {
            // Arrange
            const LifecycleType lifecycleType = LifecycleType.Singleton;
            var expectedInstanceBuilder =
                new SingletonInstanceBuilder<ClassWithParameterlessConstructor>(new InstanceCreator());

            _instanceBuilderFactory
                .Setup(i => i.GetInstanceBuilder<ClassWithParameterlessConstructor>(lifecycleType))
                .Returns(expectedInstanceBuilder);

            // Act
            _container.Register<ISomeInterface, ClassWithParameterlessConstructor>(lifecycleType);

            // Assert
            var actualInstanceBuilder = _container.Bindings[typeof(ISomeInterface)];
            Assert.AreSame(expectedInstanceBuilder, actualInstanceBuilder);
        }

        #endregion

        #region Resolve() Tests

        [Test]
        public void Resolve_ConstructorIsParameterless_ReturnsInstance()
        {
            // Act
            var expectedInstance = new ClassWithParameterlessConstructor();
            var instanceBuilderMock = new Mock<IInstanceBuilder>();

            instanceBuilderMock
                .Setup(i => i.BuildInstance())
                .Returns(expectedInstance);

            _container.Bindings.Add(typeof (ISomeInterface), instanceBuilderMock.Object);

            // Arrange
            var actualInstance = _container.Resolve<ISomeInterface>();

            // Assert
            Assert.AreSame(expectedInstance, actualInstance);
        }

        #endregion
    }
}
