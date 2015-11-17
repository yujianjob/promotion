using Dianda.AP.BLL;
using Dianda.AP.Model.Learn.MyLearn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Learn.Controllers
{
    public class MyCourseController : Controller
    {
        // 我的选课
        // GET: /Learn/MyCourse/

        #region 已批准
        public ActionResult Passed()
        {
            LearnMyCourseBLL bll = new LearnMyCourseBLL();

            Code.SiteCache cache = Code.SiteCache.Instance;
            int accountId = cache.LoginInfo.UserId;
            int partitionId = cache.LoginInfo.PartitionId;
            int recordCount = bll.GetMyCourseCount(accountId, partitionId, "4");

            int pageSize = 8, pageIndex;
            int pageCount = (int)Math.Ceiling((double)recordCount / pageSize);
            int.TryParse(Request["PageIndex"], out pageIndex);
            if (pageCount == 0)
            {
                pageIndex = 0;
            }
            else
            {
                if (pageIndex < 1)
                    pageIndex = 1;
                else if (pageIndex > pageCount)
                    pageIndex = pageCount;
            }
            ViewBag.RecordCount = recordCount;
            ViewBag.PageCount = pageCount;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;

            return View(bll.GetMyCourseList(pageSize, pageIndex, "A.Id desc", accountId, partitionId, "4"));
        }
        #endregion

        #region 待批准
        public ActionResult ForPassed()
        {
            LearnMyCourseBLL bll = new LearnMyCourseBLL();

            Code.SiteCache cache = Code.SiteCache.Instance;
            int accountId = cache.LoginInfo.UserId;
            int partitionId = cache.LoginInfo.PartitionId;

            int recordCount = bll.GetMyCourseCount(accountId, partitionId, "1,2");

            int pageSize = 8, pageIndex;
            int pageCount = (int)Math.Ceiling((double)recordCount / pageSize);
            int.TryParse(Request["PageIndex"], out pageIndex);
            if (pageCount == 0)
            {
                pageIndex = 0;
            }
            else
            {
                if (pageIndex < 1)
                    pageIndex = 1;
                else if (pageIndex > pageCount)
                    pageIndex = pageCount;
            }
            ViewBag.RecordCount = recordCount;
            ViewBag.PageCount = pageCount;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;

            return View(bll.GetMyCourseList(pageSize, pageIndex, "A.Id desc", accountId, partitionId, "1,2"));
        }
        #endregion

        #region 未批准
        public ActionResult UnPassed()
        {
            LearnMyCourseBLL bll = new LearnMyCourseBLL();

            Code.SiteCache cache = Code.SiteCache.Instance;
            int accountId = cache.LoginInfo.UserId;
            int partitionId = cache.LoginInfo.PartitionId;
            int recordCount = bll.GetMyCourseCount(accountId, partitionId, "3,5");

            int pageSize = 8, pageIndex;
            int pageCount = (int)Math.Ceiling((double)recordCount / pageSize);
            int.TryParse(Request["PageIndex"], out pageIndex);
            if (pageCount == 0)
            {
                pageIndex = 0;
            }
            else
            {
                if (pageIndex < 1)
                    pageIndex = 1;
                else if (pageIndex > pageCount)
                    pageIndex = pageCount;
            }
            ViewBag.RecordCount = recordCount;
            ViewBag.PageCount = pageCount;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;

            return View(bll.GetMyCourseList(pageSize, pageIndex, "A.Id desc", accountId, partitionId, "3,5"));
        }
        #endregion


        #region ajax
        //放弃课程
        public ActionResult AjaxCourseDelete(int id)
        {
            LearnMyCourseBLL bll = new LearnMyCourseBLL();
            MyCourseInfo model = bll.GetMyCourse(id);
            bool result;
            string msg;

            if (model == null)
            {
                result = false;
                msg = "课程不存在，请刷新页面！";
            }
            else
            {
                if (model.Result == 1)
                {
                    result = false;
                    msg = "该课程已结束，不能放弃！";
                }
                else if (model.StatusClass == 5)
                {
                    result = false;
                    msg = "该课程已开班，不能放弃！";
                }
                else if (model.StatusClass == 6)
                {
                    result = false;
                    msg = "该课程已结业，不能放弃！";
                }
                else
                {
                    using (TransactionScope trans = new TransactionScope())
                    {
                        try
                        {
                            bll.DeleteCourse(id);
                            trans.Complete();
                            result = true;
                            msg = "操作成功！";
                        }
                        catch (Exception)
                        {
                            result = false;
                            msg = "操作失败！";
                        }
                    }
                }
            }
            return Json(new { Result = result, Msg = msg }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
