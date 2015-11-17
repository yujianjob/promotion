using Dianda.AP.BLL;
using Dianda.AP.Model.Learn.MyLearn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Learn.Controllers
{
    public class MyLearnController : Controller
    {
        // 我的课程
        // GET: /Learn/MyLearn/

        #region 学习中的课程
        public ActionResult InDate()
        {
            LearnMyLearnBLL bll = new LearnMyLearnBLL();
            
            Code.SiteCache cache = Code.SiteCache.Instance;
            int accountId = cache.LoginInfo.UserId;
            int partitionId = cache.LoginInfo.PartitionId;
            int recordCount = bll.GetInDateCount(accountId, partitionId);

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

            return View(bll.GetInDateList(pageSize, pageIndex, "A.Id desc", accountId, partitionId));
        }
        #endregion

        #region 未开始的课程
        public ActionResult BeforeDate()
        {
            LearnMyLearnBLL bll = new LearnMyLearnBLL();

            Code.SiteCache cache = Code.SiteCache.Instance;
            int accountId = cache.LoginInfo.UserId;
            int partitionId = cache.LoginInfo.PartitionId;
            int recordCount = bll.GetBeforeDateCount(accountId, partitionId);

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

            return View(bll.GetBeforeDateList(pageSize, pageIndex, "A.Id desc", accountId, partitionId));
        }
        #endregion

        #region 已结束的课程
        public ActionResult OutDate()
        {
            LearnMyLearnBLL bll = new LearnMyLearnBLL();

            Code.SiteCache cache = Code.SiteCache.Instance;
            int accountId = cache.LoginInfo.UserId;
            int partitionId = cache.LoginInfo.PartitionId;
            int recordCount = bll.GetOutDateCount(accountId, partitionId);

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

            return View(bll.GetOutDateList(pageSize, pageIndex, "A.Id desc", accountId, partitionId));
        }
        #endregion

        //外部课程信息
        public ActionResult OutCourse (int trainingId)
        {
            LearnMyLearnBLL bll = new LearnMyLearnBLL();
            OutCourseInfo model = bll.GetOutCourse(trainingId);
            if (model == null)
                return Content("数据不存在，请刷新页面！");
            return View(model);
        }
    }
}
