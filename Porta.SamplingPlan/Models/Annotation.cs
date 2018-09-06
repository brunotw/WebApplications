using Microsoft.Xrm.Sdk;
using System;

namespace IW.Eims.SamplingPlan.Models
{
    public class Annotation : ModelBase
    {
        public readonly string LogicalName = "annotation";
        public Guid Id { get; set; }
        public EntityReference ObjectId { get; set; }
        public string Subject { get; set; } = "DW-Narrative";
        public string NoteText { get; set; }
        public DateTime CreateOn { get; set; }
        public EntityReference CreatedBy { get; set; }

        public Annotation()
        {

        }

        public Annotation(Entity annotation)
        {
            PopulateProperties(annotation);
        }

        public void PopulateProperties(Entity annotation)
        {
            Id = annotation.Id;
            Subject = annotation.GetAttributeValue<string>("subject");
            NoteText = annotation.GetAttributeValue<string>("notetext");
            CreateOn = annotation.GetAttributeValue<DateTime>("createdon");
            CreatedBy = annotation.GetAttributeValue<EntityReference>("createdby");
        }

        public void Create(Guid impersonateUser)
        {
            Entity annotation = new Entity(LogicalName);
            annotation["subject"] = Subject;
            annotation["objectid"] = ObjectId;
            annotation["notetext"] = NoteText;

            if (impersonateUser == Guid.Empty)
            {
                Id = Service.Create(annotation);
            }
            else
            {
                Id = ImpersonatedService(impersonateUser).Create(annotation);
            }

        }
    }
}