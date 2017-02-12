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

        private TransientInstanceBuilder<SomeClass> _transientInstanceBuilder;
        private Mock<IInstanceCreator> _instanceCreatorMock;

        [SetUp]
        public void Setup()
        {
            _instanceCreatorMock = new Mock<IInstanceCreator>();
            _transientInstanceBuilder = new TransientInstanceBuilder<SomeClass>(_instanceCreatorMock.Object);
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
    }
}
