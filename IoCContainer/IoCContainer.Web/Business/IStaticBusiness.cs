using System.Collections.Generic;

namespace IoCContainer.Web.Business
{
    public interface IStaticBusiness
    {
        string SomeData { get; set; }

        List<string> GetFromDataLayer();
    }
}