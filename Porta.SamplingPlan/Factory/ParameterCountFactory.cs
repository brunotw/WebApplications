using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using IW.Eims.SamplingPlan.Models.CustomModels;
using IW.Eims.SamplingPlan.Models;
using static IW.Eims.SamplingPlan.Models.SamplingPlanParameter;
using Microsoft.Xrm.Sdk.Query;

namespace IW.Eims.SamplingPlan.Factory
{
    public class ParameterCountFactory : ModelBase
    {
        public readonly Guid DWSamplingPlanId;
        public List<ParameterCount> ParametersCount { get; private set; } = new List<ParameterCount>();
        private List<SamplingPlanParameter> SamplingPlanParameters = new List<SamplingPlanParameter>();
        private List<MonthlySamplingPlan> MonthlySamplingPlans = new List<MonthlySamplingPlan>();

        public ParameterCountFactory(Guid dwId)
        {
            DWSamplingPlanId = dwId;
        }

        public List<ParameterCount> GetParameterCount()
        {
            EntityCollection parameters = RetrieveSamplingPlanParameters();
            EntityCollection msp = RetrieveMonthlySamplingPlan();

            foreach (Entity p in parameters.Entities)
            {
                SamplingPlanParameters.Add(new SamplingPlanParameter(p));
            }

            foreach (Entity m in msp.Entities)
            {
                MonthlySamplingPlans.Add(new MonthlySamplingPlan(m));
            }


            foreach (var p in SamplingPlanParameters.GroupBy(p => p.Name))
            {
                var spp = p.ToList();

                var groupA = spp.Where(pr => pr.GroupType == EGroupType.ACheck).FirstOrDefault();
                var groupB = spp.Where(pr => pr.GroupType == EGroupType.BAudit).FirstOrDefault();

                ParameterCount pc = new ParameterCount(p.Key, MonthlySamplingPlans, groupA != null, groupB != null);
                ParametersCount.Add(pc);
            }

            return ParametersCount;
        }

        private EntityCollection RetrieveSamplingPlanParameters()
        {
            string query = @"
            <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                <entity name='eims_samplingplanparameter'>
                    <attribute name='eims_samplingplanparameterid' />
                    <attribute name='eims_name' />
                    <attribute name='eims_grouptype' />
                    <attribute name='eims_included' />

                    <filter type='and'>
                        <condition attribute='eims_grouptype' operator='in'>
                            <value>447920000</value>
                            <value>447920001</value>
                        </condition>
                        <condition attribute='eims_included' operator='eq' value='1' />
                    </filter>
                    
                    <link-entity name='eims_wszsamplingplan' from='eims_wszsamplingplanid' to='eims_wszsamplingplanid' alias='ac'>
                        <filter type='and'>
                            <condition attribute='eims_dwsamplingplanid' operator='eq' uiname='DWRSP2018-0034' uitype='eims_dw_samplingplan' value='" + DWSamplingPlanId + @"' />
                        </filter>
                    </link-entity>
                </entity>
            </fetch>";

            EntityCollection parameters = Service.RetrieveMultiple(new FetchExpression(query));

            return parameters;
        }

        private EntityCollection RetrieveMonthlySamplingPlan()
        {
            string query = @"
            <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                <entity name='eims_monthlysampleplan'>
                <attribute name='eims_monthlysampleplanid' />
                <attribute name='eims_name' />
                <attribute name='eims_reportingdate' />
                <attribute name='eims_groupa_checks' />
                <attribute name='eims_groupa_additional' />
                <attribute name='eims_groupb_audits' />
                <attribute name='eims_groupb_additional' />

                <link-entity name='eims_wszsamplingplan' from='eims_wszsamplingplanid' to='eims_wszsamplingplanid' alias='ae'>
                    <filter type='and'>
                        <condition attribute='eims_dwsamplingplanid' operator='eq' uiname='DWRSP2018-0035' uitype='eims_dw_samplingplan' value='" + DWSamplingPlanId + @"' />
                    </filter>
                </link-entity>
                </entity>
            </fetch>";

            EntityCollection monthlySamplingPlan = Service.RetrieveMultiple(new FetchExpression(query));

            return monthlySamplingPlan;
        }

      
    }
}
