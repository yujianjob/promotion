using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Code;
using Dianda.AP.BLL;

namespace Web.Areas.ClassDomain.Controllers
{
    public class ClassInstructorController : Controller
    {
        public class KeyValue
        {
            public string Title { get; set; }
            public string Id { get; set; }
        }
        //班级列表
        public ActionResult ClassInstructorList(int? param_plan, int? param_subject, int? param_status, string param_searchTxt, int pageIndex = 1)
        {
            int totalPage;
            var fkBll = new Traning_InfoFkBLL();
            var traningBll = new Training_PlanBLL();
            ViewBag.PlanList = DataTableToListHelper<Dianda.AP.Model.Training_Plan>.ConvertToModel(traningBll.GetList(" IsOpen=1 and Delflag=0 and Display=1 ").Tables[0]);
            ViewBag.StatusList = new List<KeyValue> { new KeyValue { Title = "111", Id = "1" } };
            ViewBag.SubjectList = fkBll.GetList(" CategoryType=3 and Delflag=0 and Display=1 ", "Sort desc");
            //为审核通过，已开班，已结业，已暂停。Display=1，delflag=0.
            string where = " Display=1 and  Delflag=0 and status in (3,5,6) ";
            where += " and Instructor=" + 3;
            if (param_plan.HasValue)//学期计划
            {
                where += " and PlanId =" + param_plan.Value;
            }
            if (param_subject.HasValue)//学科
            {
                where += " and TraningId =" + param_subject.Value;
            }
            if (param_status.HasValue)//状态
            {
                where += " and Status =" + param_status.Value;
            }
            if (!string.IsNullOrEmpty(param_searchTxt))//查询条件
            {
                where += " and( Title like '%" + param_searchTxt + "%' or exists( SELECT 1 FROM dbo.Traning_Detail WHERE Title LIKE '%"
                    + param_searchTxt + "%' AND id=Class_Detail.TraningId))";
            }
            var list = DataTableToListHelper<Dianda.AP.Model.Class_Detail>.ConvertToModel(PagingQueryBll.GetPagingDataTable("Class_Detail", where, "id", pageIndex, out totalPage));

            ViewBag.pageIndex = pageIndex;
            ViewBag.totalPage = totalPage;
            ViewBag.basecount = 10;
            return View(list);
        }
    }
}
