using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace IoCContainer.Web.Factories
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        private readonly Container _container;

        public CustomControllerFactory(Container container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            return (IController) typeof (Container)
                .GetMethod("Resolve")
                .MakeGenericMethod(controllerType)
                .Invoke(_container, null);
        }
    }
}