using System.Collections.Generic;

namespace IoCContainer.Web.Models
{
    public class TestModel
    {
        public string StaticBusinessData1 { get; set; }

        public List<string> StaticBusinessDataLayersData1 { get; set; }

        public string StaticBusinessData2 { get; set; }

        public List<string> StaticBusinessDataLayersData2 { get; set; }

        public string TransientBusinessData1 { get; set; }

        public List<string> TransientBusinessDataLayersData1 { get; set; }

        public string TransientBusinessData2 { get; set; }

        public List<string> TransientBusinessDataLayersData2 { get; set; }
    }
}