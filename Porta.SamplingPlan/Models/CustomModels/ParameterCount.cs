using System;
using System.Collections.Generic;
using System.Linq;
using static IW.Eims.SamplingPlan.Models.SamplingPlanParameter;
using static IW.Eims.SamplingPlan.Models.MonthlySamplingPlan;

namespace IW.Eims.SamplingPlan.Models.CustomModels
{
    public class ParameterCount
    {
        private List<MonthlySamplingPlan> MonthlySamplingPlans { get; set; } = new List<MonthlySamplingPlan>();
        private bool ShouldCountGroupA { get;set; }
        private bool ShouldCountGroupB { get; set; }

        public string Name { get; set; }
        public int Total
        {
            get
            {
                return MonthlyCounts.Sum(m => m.Total);
            }
        }
        public int TotalGroupA
        {
            get
            {
                return MonthlyCounts.Where(m => m.GroupType == EGroupType.ACheck).Sum(m => m.Total);
            }
        }
        public int TotalGroupB
        {
            get
            {
                return MonthlyCounts.Where(m => m.GroupType == EGroupType.BAudit).Sum(m => m.Total);
            }
        }
        public List<MonthlyCount> MonthlyCounts { get; set; } = new List<MonthlyCount>();

        public ParameterCount(string key, List<MonthlySamplingPlan> monthlySamplingPlans, bool shouldCountGroupA, bool shouldCountGroupB)
        {
            Name = key;
            MonthlySamplingPlans = monthlySamplingPlans;
            ShouldCountGroupA = shouldCountGroupA;
            ShouldCountGroupB = shouldCountGroupB;

            PopulateTotals();
        }

        public void PopulateTotals()
        {
            foreach (EMonth item in Enum.GetValues(typeof(EMonth)))
            {
                AddMonthlyCount(item);
            }
        }

        private void AddMonthlyCount(EMonth month)
        {
            if(ShouldCountGroupA)
            {
                var total = MonthlySamplingPlans.Where(m => m.ReportingDate.Month == (int)month).Sum(m => m.GroupAChecks + m.GroupAAdditional);
                MonthlyCounts.Add(new MonthlyCount(total, EGroupType.ACheck, month));
            }

            if (ShouldCountGroupB)
            {
                var total = MonthlySamplingPlans.Where(m => m.ReportingDate.Month == (int)month).Sum(m => m.GroupBAudits + m.GroupBAdditional);
                MonthlyCounts.Add(new MonthlyCount(total, EGroupType.BAudit, month));
            }
        }
    }
}