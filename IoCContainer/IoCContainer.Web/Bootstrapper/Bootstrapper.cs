using IoCContainer.Web.Controllers;

namespace IoCContainer.Web.Bootstrapper
{
    public static class Bootstrapper
    {
        public static void Configure(Container container)
        {
            container.Register<HomeController, HomeController>();
        }
    }
}