using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoCContainer.Interfaces;
using NUnit.Framework;

namespace IoCContainer.Test
{
    [TestFixture]
    public class ContainerTests
    {
        private interface IBoundTo { }

        private class ParameterlessTarget : IBoundTo { }

        private Container _iocContainer;

        [SetUp]
        public void Setup()
        {
            _iocContainer = new Container();
        }

        [Test]
        public void Resolve_DefaultLifeCycleParameterlessConstructor_ReturnsInstance()
        {
            Resolve_TransientLifeCycleParameterlessConstructor_ReturnsInstance();
        }

        [Test]
        public void Resolve_TransientLifeCycleParameterlessConstructor_ReturnsInstance()
        {
            // Arrange
            _iocContainer.Register<IBoundTo, ParameterlessTarget>();

            // Act
            var result1 = _iocContainer.Resolve<IBoundTo>();
            var result2 = _iocContainer.Resolve<IBoundTo>();

            // Assert
            Assert.IsInstanceOf<ParameterlessTarget>(result1);
            Assert.IsInstanceOf<ParameterlessTarget>(result2);
            Assert.AreNotSame(result1, result2);
        }
    }
}
