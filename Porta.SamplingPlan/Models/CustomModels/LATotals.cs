using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IW.Eims.SamplingPlan.Models.CustomModels
{
    public class LATotals
    {
        public EntityReference  LocalAuthorityRef { get; set; }
        public int Approved { get; set; }
        public int Submitted { get; set; }
        public int Draft { get; set; }
        public int TotalNumberOfPlans { get; set; }
        List<MonthlyCount> MonthlyCounts { get; set; } = new List<MonthlyCount>();
    }
}