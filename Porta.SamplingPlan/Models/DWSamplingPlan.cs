using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using IW.Eims.SamplingPlan.App_Start;
using IW.Eims.SamplingPlan.Factory;
using IW.Eims.SamplingPlan.Models.CustomModels;

namespace IW.Eims.SamplingPlan.Models
{
    public class DWSamplingPlan : ModelBase
    {
        private readonly string LogicalName = "eims_dw_samplingplan";
        public Guid Id { get; set; }
        public EntityReference LocalAuthority { get; set; }
        public List<WSZSamplingPlan> WSZSamplingPlans { get; set; } = new List<WSZSamplingPlan>();
        public List<Annotation> Annotations { get; set; } = new List<Annotation>();
        public List<ParameterCount> ParameterCount { get; set; } = new List<ParameterCount>();

        public DWSamplingPlan()
        {

        }

        public DWSamplingPlan(Guid id)
        {
            Entity dw = Service.Retrieve(LogicalName, id, new ColumnSet("eims_localauthorityid"));
            PopulateProperties(dw);
        }

        public DWSamplingPlan(Entity dw)
        {
            PopulateProperties(dw);
        }

        private void PopulateProperties(Entity dw)
        {
            Id = dw.Id;
            LocalAuthority = dw.GetAttributeValue<EntityReference>("eims_localauthorityid");
        }

        public void RetrieveWSZSamplingPlan()
        {
            string query = @"
            <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                <entity name='eims_wszsamplingplan'>
                    <attribute name='eims_wszsamplingplanid' />
                    <attribute name='eims_derrogationapproved' />
                    <attribute name='eims_watersupplyzoneid' />
                    <attribute name='eims_populationserved' />
                    <attribute name='eims_volumedistributed' />
                    <attribute name='eims_populationvolumeratio' />
                    <attribute name='eims_checknumbersafetyfactor' />
                    <attribute name='eims_auditnumbersafetyfactor' />
                    <attribute name='statuscode' />
                    <attribute name='eims_samplenumbercriteria' />
                    <filter type='and'>
                        <condition attribute='eims_dwsamplingplanid' operator='eq' uitype='eims_dw_samplingplan' value='" + Id + @"' />
                    </filter>
                </entity>
            </fetch>";

            EntityCollection collection = Service.RetrieveMultiple(new FetchExpression(query));

            foreach (var wszSamplingPlan in collection.Entities)
            {
                WSZSamplingPlans.Add(new WSZSamplingPlan(wszSamplingPlan));
            }
        }

        public void RetrieveAnnotations()
        {
            string query = @"
            <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                <entity name='annotation'>
                    <attribute name='subject' />
                    <attribute name='notetext' />
                    <attribute name='annotationid' />
                    <attribute name='createdon' />

                    <filter type='and'>
                        <condition attribute='subject' operator='eq' value='DW-Narrative' />
                    </filter>
                    
                    <link-entity name='eims_dw_samplingplan' from='eims_dw_samplingplanid' to='objectid' alias='ah'>
                        <filter type='and'>
                            <condition attribute='eims_dw_samplingplanid' operator='eq' uitype='eims_dw_samplingplan' value='" + Id + @"' />
                        </filter>
                    </link-entity>
                </entity>
            </fetch>";

            EntityCollection annotations = Service.RetrieveMultiple(new FetchExpression(query));

            foreach (Entity annotation in annotations.Entities)
            {
                Annotations.Add(new Annotation(annotation));
            }
        }

        public void LoadParameterCountList()
        {
            ParameterCountFactory factory = new ParameterCountFactory(Id);
            ParameterCount = factory.GetParameterCount();
        }
    }
}