using IoCContainer.Web.Business;
using IoCContainer.Web.Controllers;
using IoCContainer.Web.Data;

namespace IoCContainer.Web.Bootstrapper
{
    public static class Bootstrapper
    {
        public static void Configure(Container container)
        {
            container.Register<TestController, TestController>();

            container.Register<IStaticBusiness, StaticBusiness>(LifecycleType.Singleton);
            container.Register<ITransientBusiness, TransientBusiness>(LifecycleType.Transient);

            container.Register<IStaticData, StaticData>(LifecycleType.Singleton);
            container.Register<ITransientData, TransientData>(LifecycleType.Transient);
        }
    }
}