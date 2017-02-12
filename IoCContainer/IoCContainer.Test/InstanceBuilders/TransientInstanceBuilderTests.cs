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
    public class TransientInstanceBuilderTests
    {
        private class SomeClass { }
        private class AnotherClass { }

        private class SomeClassWithConstructorArgs
        {
            public SomeClassWithConstructorArgs(SomeClass someClass, AnotherClass anotherClass) { }
        }

        private TransientInstanceBuilder<SomeClass> _transientInstanceBuilder;
        private TransientInstanceBuilder<SomeClassWithConstructorArgs> _constructorArgsTransientInstanceBuilder;
        private Mock<IInstanceCreator> _instanceCreatorMock;

        [SetUp]
        public void Setup()
        {
            _instanceCreatorMock = new Mock<IInstanceCreator>();
            _transientInstanceBuilder = new TransientInstanceBuilder<SomeClass>(_instanceCreatorMock.Object);

            _constructorArgsTransientInstanceBuilder =
                new TransientInstanceBuilder<SomeClassWithConstructorArgs>(_instanceCreatorMock.Object);
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
            var actualInstance = _transientInstanceBuilder.BuildInstance();

            // Assert
            Assert.AreSame(expectedInstance, actualInstance);
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
            var actualInstance = _constructorArgsTransientInstanceBuilder.BuildInstance(someClass, anotherClass);

            // Assert
            Assert.AreSame(expectedInstance, actualInstance);
        }
    }
}
