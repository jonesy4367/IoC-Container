using System.Collections.Generic;

namespace IoCContainer.Web.Business
{
    public interface ITransientBusiness
    {
        string SomeData { get; set; }

        List<string> GetFromDataLayer();
    }
}