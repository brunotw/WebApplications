using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace IW.Eims.SamplingPlan.App_Start
{
    public abstract class AppSetup
    {
        private static bool loadMockData = Convert.ToBoolean(ConfigurationManager.AppSettings["loadMockData"]);

        public static bool LoadMockData
        {
            get
            {
                return loadMockData;
            }
        }
    }
}