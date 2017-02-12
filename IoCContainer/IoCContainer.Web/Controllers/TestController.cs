using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IoCContainer.Web.Business;
using IoCContainer.Web.Models;

namespace IoCContainer.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IStaticBusiness _staticBusiness1;
        private readonly IStaticBusiness _staticBusiness2;
        private readonly ITransientBusiness _transientBusiness1;
        private readonly ITransientBusiness _transientBusiness2;

        public TestController(IStaticBusiness staticBusiness1, IStaticBusiness staticBusiness2,
            ITransientBusiness transientBusiness1, ITransientBusiness transientBusiness2)
        {
            _staticBusiness1 = staticBusiness1;
            _staticBusiness2 = staticBusiness2;
            _transientBusiness1 = transientBusiness1;
            _transientBusiness2 = transientBusiness2;
        }

        public ActionResult Index()
        {
            _staticBusiness1.SomeData = "staticBusinessValue1";
            _staticBusiness2.SomeData = "staticBusinessValue2";

            _transientBusiness1.SomeData = "transientBusinessValue1";
            _transientBusiness2.SomeData = "transientBusinessValue2";

            var testModel = new TestModel
            {
                StaticBusinessData1 = $"staticBusiness1 => {_staticBusiness1.SomeData}",
                StaticBusinessDataLayersData1 = _staticBusiness1.GetFromDataLayer(),

                StaticBusinessData2 = $"staticBusiness2 => {_staticBusiness2.SomeData}",
                StaticBusinessDataLayersData2 = _staticBusiness2.GetFromDataLayer(),

                TransientBusinessData1 = $"transientBusiness1 => {_transientBusiness1.SomeData}",
                TransientBusinessDataLayersData1 = _transientBusiness1.GetFromDataLayer(),

                TransientBusinessData2 = $"transientBusiness2 => {_transientBusiness2.SomeData}",
                TransientBusinessDataLayersData2 = _transientBusiness2.GetFromDataLayer()
            };

            return View(testModel);
        }
    }
}