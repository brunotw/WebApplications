using IW.Eims.SamplingPlan.App_Start;
using IW.Eims.SamplingPlan.Factory;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Porta.SamplingPlan.Factory;
using System;
using System.Collections.Generic;

namespace IW.Eims.SamplingPlan.Models
{
    public class LandingPage : ModelBase
    {
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        public List<DWSamplingPlan> DWSamplingPlans { get; private set; } = new List<DWSamplingPlan>();

        public LandingPage()
        {
            if (AppSetup.LoadMockData)
            {
                DWSamplingPlans = MockFactory.ListDWSamplingPlan();
            }
            else
            {
                RetrieveDWSamplingPlanForCurrentYear();
                PopulateWSZSamplingPlans();
            }
        }

        private void RetrieveDWSamplingPlanForCurrentYear()
        {
            string query = @"
            <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                <entity name='eims_dw_samplingplan'>
                    <attribute name='eims_dw_samplingplanid' />
                    <attribute name='eims_name' />
                    <attribute name='eims_localauthorityid' />
                    <filter type='and'>
                        <condition attribute='eims_sampleyear' operator='this-year' />
                    </filter>
                </entity>
            </fetch>";

            EntityCollection dwSamplingPlans = Service.RetrieveMultiple(new FetchExpression(query));

            foreach (var dw in dwSamplingPlans.Entities)
            {
                DWSamplingPlans.Add(new DWSamplingPlan(dw));
            }
        }

        private void PopulateWSZSamplingPlans()
        {
            foreach (DWSamplingPlan dw in DWSamplingPlans)
            {
                dw.RetrieveWSZSamplingPlan();
            }
        }
    }
}