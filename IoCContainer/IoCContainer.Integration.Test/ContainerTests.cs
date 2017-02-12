using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoCContainer.Exceptions;
using IoCContainer.InstanceBuilderFactories;
using NUnit.Framework;

namespace IoCContainer.Integration.Test
{
    [TestFixture]
    public class ContainerTests
    {
        private Container _container;

        private interface IConstructorless { }

        private class Constructorless : IConstructorless { }

        private interface ITopLayer { }

        private class TopLayer : ITopLayer
        {
            public IMiddleLayer MiddleLayer { get; private set; }

            public IConstructorless Constructorless { get; private set; }

            public TopLayer(IMiddleLayer middleLayer, IConstructorless constructorless)
            {
                MiddleLayer = middleLayer;
                Constructorless = constructorless;
            }
        }

        private interface IMiddleLayer { }

        private class MiddleLayer : IMiddleLayer
        {
            public IBottomLayer BottomLayer { get; private set; }

            public MiddleLayer(IBottomLayer bottomLayer)
            {
                BottomLayer = bottomLayer;
            }
        }

        private interface IBottomLayer { }

        private class BottomLayer : IBottomLayer { }

        [SetUp]
        public void Setup()
        {
            var instanceBuilderFactory = new InstanceBuilderFactory();
            _container = new Container(instanceBuilderFactory);
        }

        [Test]
        public void Resolve_DefaultLifeCycleIsTransient()
        {
            // Arrange
            _container.Register<IConstructorless, Constructorless>();

            // Act
            var instance1 = _container.Resolve<IConstructorless>();
            var instance2 = _container.Resolve<IConstructorless>();

            // Assert
            Assert.IsInstanceOf<Constructorless>(instance1);
            Assert.AreNotSame(instance1, instance2);
        }

        [Test]
        [TestCase(LifecycleType.Transient)]
        [TestCase(LifecycleType.Singleton)]
        public void Resolve_LifeCycleIsCorrect(LifecycleType lifecycleType)
        {
            // Arrange
            _container.Register<IConstructorless, Constructorless>(lifecycleType);

            // Act
            var instance1 = _container.Resolve<IConstructorless>();
            var instance2 = _container.Resolve<IConstructorless>();

            // Assert
            Assert.IsInstanceOf<Constructorless>(instance1);

            switch (lifecycleType)
            {
                case LifecycleType.Transient:
                    Assert.AreNotSame(instance1, instance2);
                    break;
                case LifecycleType.Singleton:
                    Assert.AreSame(instance1, instance2);
                    break;
                default:
                    Assert.Fail("Lifecycle type is not handled");
                    break; 
            }
        }

        [Test]
        public void Resolve_TypeIsNotRegistered_ThrowsTypeNotRegisteredException()
        {
            // Act, Assert
            Assert.Throws<TypeNotRegisteredException>(delegate
            {
                _container.Resolve<IConstructorless>();
            });
        }

        [Test]
        public void Resolve_TypeHasDependencies_ReturnsInstanceTree()
        {
            // Arrange
            _container.Register<IBottomLayer, BottomLayer>();
            _container.Register<IMiddleLayer, MiddleLayer>();
            _container.Register<ITopLayer, TopLayer>();
            _container.Register<IConstructorless, Constructorless>();

            // Act
            var instance = _container.Resolve<ITopLayer>();

            // Assert
            Assert.IsInstanceOf<TopLayer>(instance);

            var topLayer = (TopLayer) instance;
            Assert.IsInstanceOf<MiddleLayer>(topLayer.MiddleLayer);
            Assert.IsInstanceOf<Constructorless>(topLayer.Constructorless);

            var middleLayerr = (MiddleLayer) topLayer.MiddleLayer;
            Assert.IsInstanceOf<BottomLayer>(middleLayerr.BottomLayer);
        }
    }
}
