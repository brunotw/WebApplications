using Microsoft.Xrm.Sdk;
using Porta.SamplingPlan.Factory;
using System;

namespace IW.Eims.SamplingPlan.Models
{
    public abstract class ModelBase 
    {
        public IOrganizationService Service { get; private set; }

        public ModelBase()
        {
            Service = Connection.Service;
        }

        public IOrganizationService ImpersonatedService(Guid userId)
        {
            return Connection.GetImpersonatedService(userId);
        }
    }
}