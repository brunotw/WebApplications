using System;
using static IW.Eims.SamplingPlan.Models.MonthlySamplingPlan;
using static IW.Eims.SamplingPlan.Models.SamplingPlanParameter;

namespace IW.Eims.SamplingPlan.Models.CustomModels
{
    public class MonthlyCount
    {
        public int Total { get; set; }
        public EGroupType GroupType { get; set; }
        public EMonth Month { get; set; }
        public DateTime Date
        {
            get
            {
                return new DateTime(DateTime.Now.Year, (int)Month, 1);
            }
        }

        public MonthlyCount(int total, EGroupType groupType, EMonth month)
        {
            Total = total;
            GroupType = groupType;
            Month = month;
        }
    }
}