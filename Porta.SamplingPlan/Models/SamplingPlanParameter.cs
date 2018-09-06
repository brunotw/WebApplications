using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IW.Eims.SamplingPlan.Models
{
    public class SamplingPlanParameter : ModelBase
    {
        public readonly string LogicalName = "eims_samplingplanparameter";
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Included { get; set; }
        public EGroupType GroupType { get; set; }

        public enum EGroupType
        {
            ACheck = 447920000,
            BAudit = 447920001,
            Pesticide = 447920002,
            Radiological = 447920004,
            TreatmentPlant = 447920003
        }

        public SamplingPlanParameter()
        {

        }

        public SamplingPlanParameter(Entity p)
        {
            PopulateProperties(p);
        }

        private void PopulateProperties(Entity p)
        {
            Id = p.Id;
            Name = p.GetAttributeValue<string>("eims_name");
            Included = p.GetAttributeValue<bool>("eims_included");

            OptionSetValue groupType = p.GetAttributeValue<OptionSetValue>("eims_grouptype");
            if(groupType != null)
            {
                GroupType = (EGroupType)groupType.Value;
            }
        }

        public void UpdateIncluded()
        {
            if (Id == Guid.Empty)
                return;

            Entity parameter = new Entity(LogicalName);
            parameter.Id = Id;
            parameter.Attributes["eims_included"] = Included;
            Service.Update(parameter);
        }
    }
}