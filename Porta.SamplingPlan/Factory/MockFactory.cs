using IW.Eims.SamplingPlan.Models;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using IW.Eims.SamplingPlan.Models.CustomModels;
using static IW.Eims.SamplingPlan.Models.MonthlySamplingPlan;
using System.Linq;
using static IW.Eims.SamplingPlan.Models.SamplingPlanParameter;

namespace IW.Eims.SamplingPlan.Factory
{
    public abstract class MockFactory
    {
        private static Random random = new Random();

        public static List<DWSamplingPlan> ListDWSamplingPlan()
        {
            List<DWSamplingPlan> dws = new List<DWSamplingPlan>();

            dws.Add(new DWSamplingPlan
            {
                Id = Guid.NewGuid(),
                LocalAuthority = MockEntityReference("account", "Carlow County Concil"),
                WSZSamplingPlans = new List<WSZSamplingPlan>
                 {
                     new WSZSamplingPlan(WSZSamplingPlan()),
                     new WSZSamplingPlan(WSZSamplingPlan2())
                 }
            });

            dws.Add(new DWSamplingPlan
            {
                Id = Guid.NewGuid(),
                LocalAuthority = MockEntityReference("account", "Limerick County Concil"),
                WSZSamplingPlans = new List<WSZSamplingPlan>
                 {
                     new WSZSamplingPlan(WSZSamplingPlan()),
                     new WSZSamplingPlan(WSZSamplingPlan2())
                 }
            });

            return dws;
        }

        public static DWSamplingPlan MockDWSamplingPlan()
        {
            List<WSZSamplingPlan> wszSamplingPlans = new List<WSZSamplingPlan>
            {
                new WSZSamplingPlan(WSZSamplingPlan()),
                new WSZSamplingPlan(WSZSamplingPlan2())
            };

            List<Annotation> annotations = new List<Annotation>
            {
                new Annotation(Annotation()),
                new Annotation(Annotation())
            };

            DWSamplingPlan dw = new DWSamplingPlan
            {
                Id = Guid.NewGuid(),
                WSZSamplingPlans = wszSamplingPlans,
                Annotations = annotations,
                ParameterCount = MockParameterCount(),
                LocalAuthority = MockEntityReference("account", "Cork County Concil")
            };

            return dw;
        }

        public static Entity WSZSamplingPlan()
        {
            Entity sp1 = new Entity("eims_wszsamplingplan");
            sp1.Id = Guid.NewGuid();
            sp1["eims_derrogationapproved"] = false;
            sp1["eims_populationserved"] = 25000;
            sp1["eims_volumedistributed"] = 40000m;
            sp1["eims_populationvolumeratio"] = 50000m;
            sp1["eims_checknumbersafetyfactor"] = 20m;
            sp1["eims_auditnumbersafetyfactor"] = 30m;
            sp1["statuscode"] = new OptionSetValue(447920001);//Approved
            sp1["eims_samplenumbercriteria"] = new OptionSetValue(447920000);//Volume
            sp1["eims_watersupplyzoneid"] = new EntityReference("eims_watersupplyzone", Guid.NewGuid());

            return sp1;
        }

        public static List<MonthlySamplingPlanSummary> MockMonthlySamplingPlanSummary()
        {
            List<MonthlySamplingPlan> monthlySamplingPlans = MockMonthlySamplinPlans();

            List<MonthlySamplingPlanSummary> monthlySamplingPlanSummaryCollection = new List<MonthlySamplingPlanSummary>();
            monthlySamplingPlanSummaryCollection.Add(new MonthlySamplingPlanSummary(MonthlySamplingPlanSummary.EType.GroupA, monthlySamplingPlans));
            monthlySamplingPlanSummaryCollection.Add(new MonthlySamplingPlanSummary(MonthlySamplingPlanSummary.EType.GroupAAdditional, monthlySamplingPlans));
            monthlySamplingPlanSummaryCollection.Add(new MonthlySamplingPlanSummary(MonthlySamplingPlanSummary.EType.GroupB, monthlySamplingPlans));
            monthlySamplingPlanSummaryCollection.Add(new MonthlySamplingPlanSummary(MonthlySamplingPlanSummary.EType.GroupBAdditional, monthlySamplingPlans));

            return monthlySamplingPlanSummaryCollection;
        }

        public static List<Annotation> MockAnnotations()
        {
            List<Annotation> annotations = new List<Annotation>();
            annotations.Add(new Annotation
            {
                Subject = "Annotation 1",
                NoteText = " have got also the same issue and also reported by Nikola Nikolov http://stackoverflow.com/questions/42921672/suppress-warnings-errors-in-visual-studio-2017-for-certain-file",
                CreateOn = DateTime.Now,
            });

            annotations.Add(new Annotation
            {
                Subject = "Annotation 2",
                NoteText = "Thanks for the help, and also very quickly how would you do this depend on the length of the string from a user input? if you get what I mean",
                CreateOn = DateTime.Now
            });

            annotations.Add(new Annotation
            {
                Subject = "Annotation 3",
                NoteText = "This issue has been fixed and is now available in our latest update. You can download the update via the in-product notification or from here",
                CreateOn = DateTime.Now
            });

            return annotations;
        }

        private static List<MonthlySamplingPlan> MockMonthlySamplinPlans()
        {
            List<MonthlySamplingPlan> monthlySamplingPlans = new List<MonthlySamplingPlan>();
            monthlySamplingPlans.Add(new MonthlySamplingPlan(MockMonthlySamplinPlan(EMonth.January)));
            monthlySamplingPlans.Add(new MonthlySamplingPlan(MockMonthlySamplinPlan(EMonth.February)));
            monthlySamplingPlans.Add(new MonthlySamplingPlan(MockMonthlySamplinPlan(EMonth.March)));
            monthlySamplingPlans.Add(new MonthlySamplingPlan(MockMonthlySamplinPlan(EMonth.April)));
            monthlySamplingPlans.Add(new MonthlySamplingPlan(MockMonthlySamplinPlan(EMonth.May)));
            monthlySamplingPlans.Add(new MonthlySamplingPlan(MockMonthlySamplinPlan(EMonth.June)));
            monthlySamplingPlans.Add(new MonthlySamplingPlan(MockMonthlySamplinPlan(EMonth.July)));
            monthlySamplingPlans.Add(new MonthlySamplingPlan(MockMonthlySamplinPlan(EMonth.August)));
            monthlySamplingPlans.Add(new MonthlySamplingPlan(MockMonthlySamplinPlan(EMonth.September)));
            monthlySamplingPlans.Add(new MonthlySamplingPlan(MockMonthlySamplinPlan(EMonth.October)));
            monthlySamplingPlans.Add(new MonthlySamplingPlan(MockMonthlySamplinPlan(EMonth.November)));
            monthlySamplingPlans.Add(new MonthlySamplingPlan(MockMonthlySamplinPlan(EMonth.December)));

            return monthlySamplingPlans;
        }

        public static List<ParameterCount> MockParameterCount()
        {
            //List<SamplingPlanParameter> SamplingPlanParameters = MockSamplingPlanParametersForParameterCount();
            List<SamplingPlanParameter> SamplingPlanParameters = MockSamplingPlanParameters();
            List<MonthlySamplingPlan> MonthlySamplingPlans = MockMonthlySamplinPlans();

            List<ParameterCount> list = new List<ParameterCount>();

            foreach (var p in SamplingPlanParameters.Where(p => p.GroupType == EGroupType.ACheck || p.GroupType == EGroupType.BAudit).GroupBy(p => p.Name))
            {
                var spp = p.ToList();

                var groupA = spp.Where(pr => pr.GroupType == EGroupType.ACheck).FirstOrDefault();
                var groupB = spp.Where(pr => pr.GroupType == EGroupType.BAudit).FirstOrDefault();

                ParameterCount pc = new ParameterCount(p.Key, MonthlySamplingPlans, groupA != null, groupB != null);
                list.Add(pc);
            }

            return list;
        }

        //private static List<SamplingPlanParameter> MockSamplingPlanParametersForParameterCount()
        //{
        //    List<SamplingPlanParameter> list = new List<SamplingPlanParameter>();
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Aluminium", true, EGroupType.BAudit)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Aluminium", true, EGroupType.ACheck)));

        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Colour", false, EGroupType.BAudit)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Iron", true, EGroupType.BAudit)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Taste", true, EGroupType.BAudit)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Nickel", true, EGroupType.BAudit)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Odour", true, EGroupType.BAudit)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Cyanide", false, EGroupType.BAudit)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("E. Coli", true, EGroupType.BAudit)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Nitrite", true, EGroupType.BAudit)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("DOC", true, EGroupType.ACheck)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Conductivity", true, EGroupType.ACheck)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Fluoride", true, EGroupType.ACheck)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Ammonium", true, EGroupType.ACheck)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Lead", true, EGroupType.ACheck)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Colony Co", true, EGroupType.ACheck)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Mercury", true, EGroupType.ACheck)));
        //    list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Turbidity", true, EGroupType.ACheck)));

        //    return list;
        //}

        public static List<SamplingPlanParameter> MockSamplingPlanParameters()
        {
            List<SamplingPlanParameter> list = new List<SamplingPlanParameter>();
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Aluminium", true, EGroupType.BAudit)));
            //list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Aluminium", true, EGroupType.ACheck)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Colour", false, EGroupType.ACheck)));

            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Iron", true, EGroupType.Pesticide)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Taste", true, EGroupType.Radiological)));

            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Nickel", true, EGroupType.BAudit)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Odour", true, EGroupType.BAudit)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Cyanide", false, EGroupType.BAudit)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("E. Coli", true, EGroupType.BAudit)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Nitrite", true, EGroupType.Pesticide)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("DOC", true, EGroupType.Pesticide)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Conductivity", true, EGroupType.Pesticide)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Fluoride", true, EGroupType.Pesticide)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Ammonium", true, EGroupType.Pesticide)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Lead", true, EGroupType.Pesticide)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Colony Co", true, EGroupType.Pesticide)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Mercury", true, EGroupType.Pesticide)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Turbidity", true, EGroupType.Pesticide)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("DOC", true, EGroupType.ACheck)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Conductivity", true, EGroupType.ACheck)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Fluoride", true, EGroupType.ACheck)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Ammonium", true, EGroupType.ACheck)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Lead", true, EGroupType.ACheck)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Colony Co", true, EGroupType.ACheck)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Mercury", true, EGroupType.ACheck)));
            list.Add(new SamplingPlanParameter(MockSamplingPlanParameter("Turbidity", true, EGroupType.ACheck)));
            return list;
        }

        private static Entity MockSamplingPlanParameter(string parameterName, bool included, EGroupType groupType)
        {
            Entity parameter = new Entity("eims_samplingplanparameter");
            parameter.Id = Guid.NewGuid();
            parameter["eims_name"] = parameterName;
            parameter["eims_included"] = included;
            parameter["eims_grouptype"] = new OptionSetValue((int)groupType);
            return parameter;
        }

        public static List<CrossBoundary> MockCrossBoundaries()
        {
            List<CrossBoundary> list = new List<CrossBoundary>();
            list.Add(new CrossBoundary(MockCrossBoundary()));
            list.Add(new CrossBoundary(MockCrossBoundary()));
            list.Add(new CrossBoundary(MockCrossBoundary()));

            return list;
        }

        public static Entity MockCrossBoundary()
        {
            Entity cb = new Entity("eims_crossboundary");
            cb.Id = Guid.NewGuid();

            cb["eims_name"] = "Name - " + DateTime.Now.Millisecond;
            cb["eims_wszsamplingplanid"] = MockEntityReference("eims_wszsamplingplan", "WSZ1");
            cb["eims_watersupplyzoneid"] = MockEntityReference("eims_watersupplyzone", "Water Supply Zone");
            cb["eims_localauthorityid"] = MockEntityReference("account", "Carlow County Concil");
            cb["eims_finalno_groupa"] = random.Next(10, 100);
            cb["eims_finalno_groupb"] = random.Next(10, 100); ;

            return cb;
        }

        public static Entity MockMonthlySamplinPlan(EMonth month)
        {
            Entity monthlySamplingPlan = new Entity("eims_monthlysamplingplan");
            monthlySamplingPlan.Id = Guid.NewGuid();
            monthlySamplingPlan["eims_groupa_checks"] = random.Next(0, 2);
            monthlySamplingPlan["eims_groupa_additional"] = random.Next(0, 2);
            monthlySamplingPlan["eims_groupb_audits"] = random.Next(0, 2);
            monthlySamplingPlan["eims_groupb_additional"] = random.Next(0, 2);
            monthlySamplingPlan["eims_reportingdate"] = new DateTime(2018, (int)month, 1);
            return monthlySamplingPlan;
        }

        public static Entity Annotation()
        {
            Entity annotation = new Entity("annotation");
            annotation["subject"] = "Mocked Data";
            annotation["notetext"] = "The expression gen.Next(100) < 50 is boolean expression. No need to use the conditional operator here";
            annotation["createdby"] = MockEntityReference("systemuser", "Diogo Marques");
            annotation["createdon"] = DateTime.Now;

            return annotation;
        }

        public static Entity WSZSamplingPlan2()
        {
            Entity sp2 = new Entity("eims_wszsamplingplan");
            sp2.Id = Guid.NewGuid();
            sp2["eims_derrogationapproved"] = true;
            sp2["eims_populationserved"] = 30000;
            sp2["eims_volumedistributed"] = 20000m;
            sp2["eims_populationvolumeratio"] = 37000m;
            sp2["eims_checknumbersafetyfactor"] = 14m;
            sp2["eims_auditnumbersafetyfactor"] = 17m;
            sp2["statuscode"] = new OptionSetValue(447920001);//Approved
            sp2["eims_samplenumbercriteria"] = new OptionSetValue(447920000);//Volume
            sp2["eims_watersupplyzoneid"] = new EntityReference("eims_watersupplyzone", Guid.NewGuid());


            return sp2;
        }

        public static EntityReference MockEntityReference(string entity, string name)
        {
            EntityReference accountRef = new EntityReference(entity, Guid.NewGuid());
            accountRef.Name = name;
            return accountRef;
        }
    }
}