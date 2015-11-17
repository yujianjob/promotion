using Dianda.AP.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Learn.Controllers
{
    public class MyCreditController : Controller
    {
        // 我的学分
        // GET: /Learn/MyCredit/

        public ActionResult Index()
        {
            LearnMyCreditBLL bll = new LearnMyCreditBLL();
            Code.SiteCache cache = Code.SiteCache.Instance;
            int accountId = cache.LoginInfo.UserId;
            int partitionId = cache.LoginInfo.PartitionId;
            int planId = cache.PlanId;
            int organId = cache.ManageOrganId;
            ViewBag.BaseCredit = bll.GetBaseCredit(planId, organId);//标准学分
            ViewBag.ReceivedCredit = bll.GetReceivedCredit(accountId, planId, partitionId);//已获得学分(非实践)
            ViewBag.ReceivedPracticeCredit = bll.GetReceivedPracticeCredit(accountId, planId);//已获得学分(实践)
            ViewBag.ExemptionCredit = bll.GetExemptionCredit(planId, accountId);//免修学分
            ViewBag.TrainingCredit = bll.GetTrainingCredit(accountId, planId, partitionId);//正在培训的学分(非实践)
            ViewBag.PracticeCredit = bll.GetPracticeCredit(accountId, planId);//正在培训的学分(实践)
            return View(bll.GetTrainingField());
        }

    }
}
