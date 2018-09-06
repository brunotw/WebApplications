using IW.Eims.SamplingPlan.App_Start;
using IW.Eims.SamplingPlan.Factory;
using IW.Eims.SamplingPlan.Models;
using IW.Eims.SamplingPlan.Models.CustomModels;
using Microsoft.Xrm.Sdk;
using Porta.SamplingPlan.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static IW.Eims.SamplingPlan.Models.CustomModels.MonthlySamplingPlanSummary;

namespace Porta.SamplingPlan.Controllers
{
    public class LocalAuthorityDetailController : Controller
    {
        public ActionResult Index(Guid id)
        {
            if (AppSetup.LoadMockData)
            {
                DWSamplingPlan dw = MockFactory.MockDWSamplingPlan();
                return View(dw);
            }
            else
            {
                DWSamplingPlan dw = new DWSamplingPlan(id);
                dw.RetrieveWSZSamplingPlan();
                dw.LoadParameterCountList();
                return View(dw);
            }
        }

        public ActionResult MonthlySamplingPlanSummary(Guid id)
        {
            if (AppSetup.LoadMockData)
            {
                List<MonthlySamplingPlanSummary> list = MockFactory.MockMonthlySamplingPlanSummary();
                return View(list);

            }
            else
            {
                List<MonthlySamplingPlanSummary> list = LoadSamplingPlanSummary(id);
                return View(list);
            }
        }

        private List<MonthlySamplingPlanSummary> LoadSamplingPlanSummary(Guid id)
        {
            WSZSamplingPlan wszSamplingPlan = new WSZSamplingPlan(id);
            List<MonthlySamplingPlan> monthlySamplingPlans = wszSamplingPlan.RetrieveMonthlySamplingPlans();

            List<MonthlySamplingPlanSummary> list = new List<MonthlySamplingPlanSummary>
            {
                new MonthlySamplingPlanSummary(EType.GroupA, monthlySamplingPlans),
                new MonthlySamplingPlanSummary(EType.GroupAAdditional, monthlySamplingPlans),
                new MonthlySamplingPlanSummary(EType.GroupB, monthlySamplingPlans),
                new MonthlySamplingPlanSummary(EType.GroupBAdditional, monthlySamplingPlans)
            };

            return list;
        }

        public ActionResult CrossBoundarySummary(Guid id)
        {
            if (AppSetup.LoadMockData)
            {
                List<CrossBoundary> list = MockFactory.MockCrossBoundaries();
                return View(list);
            }
            else
            {
                List<CrossBoundary> list = LoadCrossBoundary(id);
                return View(list);
            }
        }

        private List<CrossBoundary> LoadCrossBoundary(Guid id)
        {
            WSZSamplingPlan wszSamplingPlan = new WSZSamplingPlan(id);
            List<CrossBoundary> crossBoundaries = wszSamplingPlan.RetrieveCrossBoundaries();
            return crossBoundaries;
        }

        public ActionResult SamplingPlanParameters(Guid id)
        {
            if (AppSetup.LoadMockData)
            {
                List<SamplingPlanParameter> parameters = MockFactory.MockSamplingPlanParameters();
                return View(parameters);
            }
            else
            {
                List<SamplingPlanParameter> parameters = LoadParameters(id);
                return View(parameters);
            }
        }

        private List<SamplingPlanParameter> LoadParameters(Guid id)
        {
            WSZSamplingPlan wszSamplingPlan = new WSZSamplingPlan(id);
            List<SamplingPlanParameter> parameters = wszSamplingPlan.RetrieveParameters();

            return parameters;
        }

        public ActionResult Narrative(Guid id)
        {
            if (AppSetup.LoadMockData)
            {
                List<Annotation> annotations = MockFactory.MockAnnotations();
                return View(annotations);

            }
            else
            {
                DWSamplingPlan dw = new DWSamplingPlan { Id = id };
                dw.RetrieveAnnotations();
                return View(dw.Annotations);
            }
        }

        public ActionResult LAParameterCount(DWSamplingPlan dw)
        {
            return View(dw);
        }

        public ActionResult LATotals(DWSamplingPlan dw)
        {
            return View(dw);
        }

        [HttpPost]
        public ActionResult UpdateMonthlySamplingPlan(Guid id, string attribute, int value)
        {
            try
            {
                Entity m = new Entity("eims_monthlysampleplan");
                m.Id = id;
                m.Attributes[attribute] = value;
                Connection.Service.Update(m);

                return Json(new { @Success = true });
            }
            catch (Exception ex)
            {
                return Json(new { @Success = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateParameters(List<SamplingPlanParameter> parameters, Guid dwId, string reason)
        {
            try
            {
                foreach (SamplingPlanParameter p in parameters)
                {
                    p.UpdateIncluded();
                }

                //Create Note
                Annotation a = new Annotation()
                {
                    NoteText = reason,
                    ObjectId = new EntityReference("eims_dw_samplingplan", dwId)
                };

                a.Create(new Guid("0E94E8E4-6A12-E811-80F5-3863BB341A20"));

                return Json(new { @success = true, @message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { @success = false, @message = ex.Message });
            }
        }
    }
}