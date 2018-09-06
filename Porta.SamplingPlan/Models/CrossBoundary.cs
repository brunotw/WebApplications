using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ComponentModel;

namespace IW.Eims.SamplingPlan.Models
{
    public class CrossBoundary : ModelBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public EntityReference WSZSamplingPlan { get; set; }
        [DisplayName("Water Supply Zone")]
        public EntityReference WaterSupplyZone { get; set; }
        public EntityReference LocalAuthority { get; set; }
        [DisplayName("Final Number Of Group A")]
        public int FinalNumberOfGroupA { get; set; }
        [DisplayName("Final Number Of Group B")]
        public int FinalNumberOfGroupB { get; set; }

        public CrossBoundary(Guid id)
        {
            Entity crossBoundary = Service.Retrieve("eims_crossboundary", id, new ColumnSet(true));
            PopulateProperties(crossBoundary);
        }

        public CrossBoundary(Entity crossBoundary)
        {
            PopulateProperties(crossBoundary);
        }

        public void PopulateProperties(Entity crossBoundary)
        {
            Id = crossBoundary.Id;
            Name = crossBoundary.GetAttributeValue<string>("eims_name");
            WSZSamplingPlan = crossBoundary.GetAttributeValue<EntityReference>("eims_wszsamplingplanid");
            WaterSupplyZone = crossBoundary.GetAttributeValue<EntityReference>("eims_watersupplyzoneid");
            LocalAuthority = crossBoundary.GetAttributeValue<EntityReference>("eims_localauthorityid");
            FinalNumberOfGroupA = crossBoundary.GetAttributeValue<int>("eims_finalno_groupa");
            FinalNumberOfGroupB = crossBoundary.GetAttributeValue<int>("eims_finalno_groupb");
        }
    }
}