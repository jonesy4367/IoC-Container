using System.Collections.Generic;
using IoCContainer.Web.Data;

namespace IoCContainer.Web.Business
{
    public class TransientBusiness : ITransientBusiness
    {
        private readonly IStaticData _staticData1;
        private readonly IStaticData _staticData2;
        private readonly ITransientData _transientData1;
        private readonly ITransientData _transientData2;

        public TransientBusiness(IStaticData staticData1, IStaticData staticData2, ITransientData transientData1,
            ITransientData transientData2)
        {
            _staticData1 = staticData1;
            _staticData2 = staticData2;
            _transientData1 = transientData1;
            _transientData2 = transientData2;
        }

        public string SomeData { get; set; }

        public List<string> GetFromDataLayer()
        {
            _staticData1.SomeCoolData = "staticDataValue1";
            _staticData2.SomeCoolData = "staticDataValue2";

            _transientData1.SomeData = "transientDataValue1";
            _transientData2.SomeData = "transientDataValue2";

            return new List<string>
            {
                $"_staticData1 => {_staticData1.SomeCoolData}",
                $"_staticData2 => {_staticData2.SomeCoolData}",
                $"_transientData1 => {_transientData1.SomeData}",
                $"_transientData2 => {_transientData2.SomeData}"
            };
        }
    }
}