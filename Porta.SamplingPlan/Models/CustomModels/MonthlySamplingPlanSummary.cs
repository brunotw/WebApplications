using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IW.Eims.SamplingPlan.Models.CustomModels
{
    public class MonthlySamplingPlanSummary
    {
        public List<MonthlySamplingPlan> MonthlySamplingPlans = new List<MonthlySamplingPlan>();
        public EType Type { get; }
        public string TypeName { get; private set; }
        //public int January { get; private set; }
        //public int February { get; private set; }
        //public int March { get; private set; }
        //public int April { get; private set; }
        //public int May { get; private set; }
        //public int June { get; private set; }
        //public int July { get; private set; }
        //public int August { get; private set; }
        //public int September { get; private set; }
        //public int October { get; private set; }
        //public int November { get; private set; }
        //public int December { get; private set; }

        public MonthlySamplingPlanSummary(EType type, List<MonthlySamplingPlan> monthlySamplingPlans)
        {
            MonthlySamplingPlans = monthlySamplingPlans;
            Type = type;
            PopulateTypeName();
        }

        private void PopulateTypeName()
        {
            switch (Type)
            {
                case EType.GroupA:
                    TypeName = "Group A";
                    break;
                case EType.GroupAAdditional:
                    TypeName = "Group A Additional";
                    break;
                case EType.GroupB:
                    TypeName = "Group B";
                    break;
                case EType.GroupBAdditional:
                    TypeName = "Group B Additional";
                    break;
                default:
                    break;
            }
        }

        public enum EType
        {
            GroupA = 1,
            GroupAAdditional = 2,
            GroupB = 3,
            GroupBAdditional = 4
        }
    }
}