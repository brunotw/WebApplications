using IW.Eims.SamplingPlan.App_Start;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IW.Eims.SamplingPlan.Models
{
    public class WSZSamplingPlan : ModelBase
    {
        public Guid Id { get; set; }
        public WaterSupplyZone WaterSupplyZone { get; set; }
        public EntityReference WaterSupplyZoneRef { get; set; }
        public List<MonthlySamplingPlan> MonthlySamplingPlans { get; set; } = new List<MonthlySamplingPlan>();
        public List<CrossBoundary> CrossBoundaries { get; set; } = new List<CrossBoundary>();
        public List<SamplingPlanParameter> Parameters { get; set; } = new List<SamplingPlanParameter>();
        public bool DerrogationApproved { get; set; }
        public int PopulationServed { get; set; }
        public decimal VolumeDistributed { get; set; }
        public decimal PopulationVolumeRatio { get; set; }
        public ESampleNumberCriteria SampleNumberCriteria { get; set; }
        public decimal GroupANumberSafetyFactor { get; set; }
        public decimal GroupBNumberSafetyFactor { get; set; }
        public int FinalNumberOfGroupA { get; set; }
        public int FinalNumberOfGroupB { get; set; }
        public ESamplingPlanStatus SamplingPlanStatus { get; set; }
        public string SamplingPlanStatusName
        {
            get
            {
                switch (SamplingPlanStatus)
                {
                    case ESamplingPlanStatus.Approved:
                        return "Approved";
                    case ESamplingPlanStatus.Draft:
                        return "Draft";
                    case ESamplingPlanStatus.Inactive:
                        return "Inactive";
                    case ESamplingPlanStatus.Review:
                        return "Review";
                    default:
                        return "Option not configured";
                }
            }
        }

        public enum ESampleNumberCriteria
        {
            Volume = 447920000,
            Population = 447920001
        }

        public enum ESamplingPlanStatus
        {
            Approved = 447920001,
            Draft = 1,
            Inactive = 2,
            Review = 447920000
        }

        public WSZSamplingPlan(Guid id)
        {
            Entity wszSamplingPlan = Service.Retrieve("eims_wszsamplingplan", id, new ColumnSet(true));
            PopulateProperties(wszSamplingPlan);
        }

        //private void LoadMock()
        //{
        //    Random random = new Random();
        //    Id = Guid.Empty;
        //    DerrogationApproved = random.Next(100) <= 20 ? true : false;
        //    PopulationServed = random.Next(10000);
        //    VolumeDistributed = random.Next(10000);
        //    PopulationVolumeRatio = random.Next(10000);
        //    GroupANumberSafetyFactor = random.Next(10000);
        //    GroupBNumberSafetyFactor = random.Next(10000);
        //    SamplingPlanStatus = (ESamplingPlanStatus)random.Next(447920000, 447920001);
        //    SampleNumberCriteria = (ESampleNumberCriteria)random.Next(447920000, 447920001);
        //    WaterSupplyZone = new WaterSupplyZone(Guid.Empty);
        //}

        public WSZSamplingPlan(Entity wszSamplingPlan)
        {
            PopulateProperties(wszSamplingPlan);
        }

        private void PopulateProperties(Entity wszSamplingPlan)
        {
            Id = wszSamplingPlan.Id;
            DerrogationApproved = wszSamplingPlan.GetAttributeValue<bool>("eims_derrogationapproved");
            PopulationServed = wszSamplingPlan.GetAttributeValue<int>("eims_populationserved");
            VolumeDistributed = wszSamplingPlan.GetAttributeValue<decimal>("eims_volumedistributed");
            PopulationVolumeRatio = wszSamplingPlan.GetAttributeValue<decimal>("eims_populationvolumeratio");
            GroupANumberSafetyFactor = wszSamplingPlan.GetAttributeValue<decimal>("eims_checknumbersafetyfactor");
            GroupBNumberSafetyFactor = wszSamplingPlan.GetAttributeValue<decimal>("eims_auditnumbersafetyfactor");

            OptionSetValue samplingPlanStatus = wszSamplingPlan.GetAttributeValue<OptionSetValue>("statuscode");
            if (samplingPlanStatus != null)
            {
                SamplingPlanStatus = (ESamplingPlanStatus)samplingPlanStatus.Value;
            }

            OptionSetValue sampleNumberCriteria = wszSamplingPlan.GetAttributeValue<OptionSetValue>("eims_samplenumbercriteria");
            if (sampleNumberCriteria != null)
            {
                SampleNumberCriteria = (ESampleNumberCriteria)sampleNumberCriteria.Value;
            }

            WaterSupplyZoneRef = wszSamplingPlan.GetAttributeValue<EntityReference>("eims_watersupplyzoneid");
            if (WaterSupplyZoneRef != null)
            {
                WaterSupplyZone = new WaterSupplyZone(WaterSupplyZoneRef.Id);
            }
        }

        public List<SamplingPlanParameter> RetrieveParameters()
        {
            string query = @"
            <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
              <entity name='eims_samplingplanparameter'>
                <attribute name='eims_samplingplanparameterid' />
                <attribute name='eims_name' />
                <attribute name='eims_included' />
                <attribute name='eims_grouptype' />
                <filter type='and'>
                  <condition attribute='eims_wszsamplingplanid' operator='eq' uitype='eims_wszsamplingplan' value='" + Id + @"' />
                </filter>
              </entity>
            </fetch>";

            EntityCollection parameters = Service.RetrieveMultiple(new FetchExpression(query));

            foreach (Entity p in parameters.Entities)
            {
                Parameters.Add(new SamplingPlanParameter(p));
            }

            return Parameters;
        }
        public List<CrossBoundary> RetrieveCrossBoundaries()
        {
            string query = @"
            <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                <entity name='eims_crossboundary'>
                    <attribute name='eims_crossboundaryid' />
                    <attribute name='eims_name' />
                    <attribute name='eims_wszsamplingplanid' />
                    <attribute name='eims_watersupplyzoneid' />
                    <attribute name='eims_localauthorityid' />
                    <attribute name='eims_finalno_groupb' />
                    <attribute name='eims_finalno_groupa' />

                    <filter type='and'>
                        <condition attribute='eims_wszsamplingplanid' operator='eq' uitype='eims_wszsamplingplan' value='" + Id + @"' />
                    </filter>

                    </entity>
            </fetch>";

            EntityCollection collection = Service.RetrieveMultiple(new FetchExpression(query));

            foreach (var item in collection.Entities)
            {
                CrossBoundaries.Add(new CrossBoundary(item));
            }

            return CrossBoundaries;
        }
        public List<MonthlySamplingPlan> RetrieveMonthlySamplingPlans()
        {
            string query = @"
            <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                <entity name='eims_monthlysampleplan'>
                    <attribute name='eims_monthlysampleplanid' />
                    <attribute name='eims_groupa_checks' />
                    <attribute name='eims_groupa_additional' />
                    <attribute name='eims_groupb_audits' />
                    <attribute name='eims_groupb_additional' />
                    <attribute name='eims_reportingdate' />

                    <filter type='and'>
                        <condition attribute='eims_wszsamplingplanid' operator='eq' uitype='eims_wszsamplingplan' value='" + Id + @"' />
                    </filter>
                </entity>
            </fetch>";

            EntityCollection collection = Service.RetrieveMultiple(new FetchExpression(query));

            foreach (var item in collection.Entities)
            {
                MonthlySamplingPlans.Add(new MonthlySamplingPlan(item));
            }

            return MonthlySamplingPlans;
        }
    }
}