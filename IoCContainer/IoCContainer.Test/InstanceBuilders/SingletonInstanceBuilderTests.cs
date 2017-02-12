using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoCContainer.InstanceBuilders;
using IoCContainer.InstanceCreators;
using Moq;
using NUnit.Framework;

namespace IoCContainer.Test.InstanceBuilders
{
    [TestFixture]
    public class SingletonInstanceBuilderTests
    {
        private class SomeClass { }
        private class AnotherClass { }

        private class SomeClassWithConstructorArgs
        {
            public SomeClassWithConstructorArgs(SomeClass someClass, AnotherClass anotherClass) { }
        }

        private SingletonInstanceBuilder<SomeClass> _singletonInstanceBuilder;
        private SingletonInstanceBuilder<SomeClassWithConstructorArgs> _constructorArgsSingletonInstanceBuilder;
        private Mock<IInstanceCreator> _instanceCreatorMock;

        [SetUp]
        public void Setup()
        {
            _instanceCreatorMock = new Mock<IInstanceCreator>();
            _singletonInstanceBuilder = new SingletonInstanceBuilder<SomeClass>(_instanceCreatorMock.Object);

            _constructorArgsSingletonInstanceBuilder =
                new SingletonInstanceBuilder<SomeClassWithConstructorArgs>(_instanceCreatorMock.Object);
        }

        [Test]
        public void BuildInstance_ReturnsInstance()
        {
            // Arrange
            var expectedInstance = new SomeClass();

            _instanceCreatorMock
                .Setup(i => i.CreateInstance<SomeClass>())
                .Returns(expectedInstance);

            // Act
            var actualInstance = _singletonInstanceBuilder.BuildInstance();

            // Assert
            Assert.AreSame(expectedInstance, actualInstance);
        }

        [Test]
        public void BuildInstance_ReturnsSameInstanceOnSecondCall()
        {
            // Arrange
            var expectedInstance = new SomeClass();
            var anotherInstance = new SomeClass();

            _instanceCreatorMock
                .Setup(i => i.CreateInstance<SomeClass>())
                .Returns(expectedInstance)
                .Callback(
                    () => _instanceCreatorMock
                        .Setup(i => i.CreateInstance<SomeClass>())
                        .Returns(anotherInstance));

            // Act
            var instance1 = _singletonInstanceBuilder.BuildInstance();
            var instance2 = _singletonInstanceBuilder.BuildInstance();

            // Assert
            Assert.AreSame(instance1, instance2);
        }

        [Test]
        public void BuildInstance_ConstructorHasArgs_ReturnsInstance()
        {
            // Arrange
            var someClass = new SomeClass();
            var anotherClass = new AnotherClass();
            var expectedInstance = new SomeClassWithConstructorArgs(someClass, anotherClass);

            _instanceCreatorMock
                .Setup(
                    i =>
                        i.CreateInstance<SomeClassWithConstructorArgs>(someClass, anotherClass))
                .Returns(expectedInstance);

            // Act
            var actualInstance = _constructorArgsSingletonInstanceBuilder.BuildInstance(someClass, anotherClass);

            // Assert
            Assert.AreSame(expectedInstance, actualInstance);
        }
    }
}
