using System;
using Microsoft.Xrm.Sdk;

namespace IW.Eims.SamplingPlan.Models
{
    public class MonthlySamplingPlan
    {
        public Guid Id { get; set; }
        public int GroupAChecks { get; set; }
        public int GroupAAdditional { get; set; }
        public int GroupBAudits { get; set; }
        public int GroupBAdditional { get; set; }
        public DateTime ReportingDate { get; set; }


        public MonthlySamplingPlan(Guid id)
        {
            Id = id;
        }

        public MonthlySamplingPlan(Entity monthlySamplingPlan)
        {
            PopulateProperties(monthlySamplingPlan);
        }

        private void PopulateProperties(Entity monthlySamplingPlan)
        {
            Id = monthlySamplingPlan.Id;
            GroupAChecks = monthlySamplingPlan.GetAttributeValue<int>("eims_groupa_checks");
            GroupAAdditional = monthlySamplingPlan.GetAttributeValue<int>("eims_groupa_additional");
            GroupBAudits= monthlySamplingPlan.GetAttributeValue<int>("eims_groupb_audits");
            GroupBAdditional= monthlySamplingPlan.GetAttributeValue<int>("eims_groupb_additional");
            ReportingDate = monthlySamplingPlan.GetAttributeValue<DateTime>("eims_reportingdate");
        }

        public enum EMonth
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }
    }
}