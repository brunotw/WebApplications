using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace IW.Eims.SamplingPlan.Models
{
    public class LocalAuthority : ModelBase
    {
        public readonly string LogicalName = "account";
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CountWSZ { get; set; }
        public List<WSZSamplingPlan> WSZSamplingPlans { get; set; } = new List<WSZSamplingPlan>();

        public LocalAuthority(Guid id)
        {
            Entity la = Service.Retrieve(LogicalName, id, new ColumnSet(true));
            PopulateProperties(la);
        }

        public LocalAuthority(Entity la)
        {
            PopulateProperties(la);
        }

        private void PopulateProperties(Entity la)
        {
            Id = la.Id;
            Name = la.GetAttributeValue<string>("name");
        }
    }
}