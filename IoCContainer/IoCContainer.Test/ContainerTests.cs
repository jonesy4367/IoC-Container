using IoCContainer.Exceptions;
using NUnit.Framework;
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

        private interface IDefaultConstructorInterface { }

        private interface IMiddleLayerInterface { }

        private interface ITopLayerInterface { }

        private class ClassWithDefaultConstructor : IDefaultConstructorInterface { }

        private class MiddleLayerClass : IMiddleLayerInterface
        {
            public IDefaultConstructorInterface Dependency { get; }

            public MiddleLayerClass(IDefaultConstructorInterface dependency)
            {
                Dependency = dependency;
            }
        }

        private class TopLayerClass : ITopLayerInterface
        {
            public IMiddleLayerInterface Dependency { get; }

            public TopLayerClass(IMiddleLayerInterface dependency)
            {
                Dependency = dependency;
            }
        }
        
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
                new TransientInstanceBuilder<ClassWithDefaultConstructor>(new InstanceCreator());

            _instanceBuilderFactory
                .Setup(i => i.GetInstanceBuilder<ClassWithDefaultConstructor>(LifecycleType.Transient))
                .Returns(expectedInstanceBuilder);

            // Act
            _container.Register<IDefaultConstructorInterface, ClassWithDefaultConstructor>();

            // Assert
            var actualInstanceBuilder = _container.Bindings[typeof(IDefaultConstructorInterface)];
            Assert.AreSame(expectedInstanceBuilder, actualInstanceBuilder);
        }

        [Test]
        public void Register_LifecycleTypeIsProvided_TypeIsRegistered()
        {
            // Arrange
            const LifecycleType lifecycleType = LifecycleType.Singleton;
            var expectedInstanceBuilder =
                new SingletonInstanceBuilder<ClassWithDefaultConstructor>(new InstanceCreator());

            _instanceBuilderFactory
                .Setup(i => i.GetInstanceBuilder<ClassWithDefaultConstructor>(lifecycleType))
                .Returns(expectedInstanceBuilder);

            // Act
            _container.Register<IDefaultConstructorInterface, ClassWithDefaultConstructor>(lifecycleType);

            // Assert
            var actualInstanceBuilder = _container.Bindings[typeof(IDefaultConstructorInterface)];
            Assert.AreSame(expectedInstanceBuilder, actualInstanceBuilder);
        }

        #endregion

        #region Resolve() Tests

        [Test]
        public void Resolve_NoConstructor_ReturnsInstance()
        {
            // Act
            var expectedInstance = new ClassWithDefaultConstructor();
            var instanceBuilderMock = new Mock<IInstanceBuilder>();

            instanceBuilderMock
                .Setup(i => i.BuildInstance())
                .Returns(expectedInstance);

            instanceBuilderMock
                .Setup(i => i.GetInstanceType())
                .Returns(typeof (ClassWithDefaultConstructor));

            _container.Bindings.Add(typeof (IDefaultConstructorInterface), instanceBuilderMock.Object);

            // Arrange
            var actualInstance = _container.Resolve<IDefaultConstructorInterface>();

            // Assert
            Assert.AreSame(expectedInstance, actualInstance);
        }

        [Test]
        public void Resolve_ConstructorHasParameters_ReturnsInstance()
        {
            // Arrange
            var expectedBottomLayerClass = new ClassWithDefaultConstructor();
            var expectedMiddleLayerClass = new MiddleLayerClass(expectedBottomLayerClass);
            var expectedTopLayerClass = new TopLayerClass(expectedMiddleLayerClass);

            var bottomLayerBuilderMock = new Mock<IInstanceBuilder>();
            var middleLayerBuilderMock = new Mock<IInstanceBuilder>();
            var topLayerBuilderMock = new Mock<IInstanceBuilder>();

            bottomLayerBuilderMock
                .Setup(b => b.BuildInstance())
                .Returns(expectedBottomLayerClass);

            bottomLayerBuilderMock
                .Setup(b => b.GetInstanceType())
                .Returns(typeof (ClassWithDefaultConstructor));

            middleLayerBuilderMock
                .Setup(m => m.BuildInstance(It.Is<object[]>(o => o.Length == 1 && o[0] == expectedBottomLayerClass)))
                .Returns(expectedMiddleLayerClass);

            middleLayerBuilderMock
                .Setup(m => m.GetInstanceType())
                .Returns(typeof (MiddleLayerClass));

            topLayerBuilderMock
                .Setup(t => t.BuildInstance(It.Is<object[]>(o => o.Length == 1 && o[0] == expectedMiddleLayerClass)))
                .Returns(expectedTopLayerClass);

            topLayerBuilderMock
                .Setup(t => t.GetInstanceType())
                .Returns(typeof (TopLayerClass));

            _container.Bindings.Add(typeof (ITopLayerInterface), topLayerBuilderMock.Object);
            _container.Bindings.Add(typeof (IMiddleLayerInterface), middleLayerBuilderMock.Object);
            _container.Bindings.Add(typeof (IDefaultConstructorInterface), bottomLayerBuilderMock.Object);

            // Act
            var actualTopLayerClass = _container.Resolve<ITopLayerInterface>();

            // Assert
            Assert.AreSame(expectedTopLayerClass, actualTopLayerClass);

            var actualMiddleLayerClass = ((TopLayerClass) actualTopLayerClass).Dependency;
            Assert.AreSame(expectedMiddleLayerClass, actualMiddleLayerClass);

            var actualBottomLayerClass = ((MiddleLayerClass) actualMiddleLayerClass).Dependency;
            Assert.AreSame(expectedBottomLayerClass, actualBottomLayerClass);
        }

        [Test]
        public void Resolve_TypeIsNotRegistered_ThrowsTypeNotRegisteredException()
        {
            // Arrange
            var expectedMessage = $"The type '{typeof (IDefaultConstructorInterface).FullName}' has not been registerd.";

            // Act, Assert
            var exception = Assert.Throws<TypeNotRegisteredException>(
                delegate { _container.Resolve<IDefaultConstructorInterface>(); });

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        #endregion
    }
}
