using IW.Eims.SamplingPlan.App_Start;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IW.Eims.SamplingPlan.Models
{
    public class WaterSupplyZone : ModelBase
    {
        public string Name { get; set; }
        public string WSZEDENCode { get; set; }

        public WaterSupplyZone(Guid id)
        {
            if (AppSetup.LoadMockData)
            {
                LoadMock();
            }
            else
            {
                Entity wsz = Service.Retrieve("eims_watersupplyzone", id, new ColumnSet(true));
                PopulateProperties(wsz);
            }
        }

        private void LoadMock()
        {
            Entity wsz = new Entity("eims_watersupplyzone");
            wsz.Id = Guid.NewGuid();
            wsz["eims_wzsname"] = "Random Name - " + DateTime.Now.Millisecond.ToString();
            wsz["eims_wszedencode"] = DateTime.Now.Millisecond.ToString();
            PopulateProperties(wsz);
        }

        private void PopulateProperties(Entity wsz)
        {
            Name = wsz.GetAttributeValue<string>("eims_wzsname");
            WSZEDENCode = wsz.GetAttributeValue<string>("eims_wszedencode");
        }
    }
}