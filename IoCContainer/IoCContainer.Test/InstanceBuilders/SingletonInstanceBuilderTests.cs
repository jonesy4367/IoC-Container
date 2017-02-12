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

        private SingletonInstanceBuilder<SomeClass> _singletonInstanceBuilder;
        private Mock<IInstanceCreator> _instanceCreatorMock;

        [SetUp]
        public void Setup()
        {
            _instanceCreatorMock = new Mock<IInstanceCreator>();
            _singletonInstanceBuilder = new SingletonInstanceBuilder<SomeClass>(_instanceCreatorMock.Object);
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
    }
}
