using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using IoCContainer.InstanceBuilderFactories;
using IoCContainer.Web.Factories;

namespace IoCContainer.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var instanceBuilderFactory = new InstanceBuilderFactory();
            var container = new Container(instanceBuilderFactory);
            Bootstrapper.Bootstrapper.Configure(container);

            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory(container));
        }
    }
}
