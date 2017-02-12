using IoCContainer.Web.Business;
using IoCContainer.Web.Controllers;
using IoCContainer.Web.Data;

namespace IoCContainer.Web.Bootstrapper
{
    public static class Bootstrapper
    {
        public static void Configure(Container container)
        {
            container.Register<HomeController, HomeController>();
            container.Register<TestController, TestController>();

            container.Register<IStaticBusiness, StaticBusiness>();
            container.Register<ITransientBusiness, TransientBusiness>();

            container.Register<IStaticData, StaticData>();
            container.Register<ITransientData, TransientData>();
        }
    }
}