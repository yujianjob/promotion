using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using Dianda.AP.Model;
using Dianda.AP.BLL;
using System.Reflection;
using System.IO;
using Dianda.Common;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using System.Web;

namespace Web.Areas.Prepare.Controllers
{
    public class PeriodController : Controller
    {
        //
        // GET: /Prepare/Period/

        PlanExemptionBLL planbll = new PlanExemptionBLL();
        PeriodBLL bll = new PeriodBLL();
        CertificateBLL cbll = new CertificateBLL();
        public ActionResult Index()
        {
            int pageIndex = 1, pageSize = 10, pageCount, recordCount;
            if (!String.IsNullOrEmpty(Request["PageIndex"]))
                int.TryParse(QueryString.Decrypt(Request["PageIndex"]), out pageIndex);

            int GroupId = Code.SiteCache.Instance.GroupId;    //权限组ID
            int PlanId = Code.SiteCache.Instance.PlanId;    //本学期ID
            int PartitionId = Code.SiteCache.Instance.LoginInfo.PartitionId;     //本区ID
            int OrganId = Code.SiteCache.Instance.ManageOrganId;    //机构ID
            string where = " and a.PlanId = '" + PlanId + "'";
            if (GroupId == 1)
            {
                
            }
            else if (GroupId == 2)
            {
                where += " and (c.OrganId = '" + OrganId + "' or c.OrganId in (select Id from Organ_Detail where ParentId = '" + OrganId + "' )) ";
            }
            else
            {
                where += " and c.OrganId = '" + OrganId + "' ";
            }

            if (!String.IsNullOrEmpty(Request["whereOrganTo"]))
            {
                if (Request["whereOrganTo"].ToString() != "请选择")
                {
                    where += "and CONVERT(varchar,c.OrganId) = '" + Request["whereOrganTo"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "' ";
                }
                else
                {
                    if (!String.IsNullOrEmpty(Request["whereOrgan"]))
                    {
                        if (Request["whereOrgan"] != "请选择")
                        {
                            where += " and (CONVERT(varchar,c.OrganId) = '" + Request["whereOrgan"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "' or CONVERT(varchar,c.OrganId) in (select Id from Organ_Detail where CONVERT(varchar,ParentId) = '" + Request["whereOrgan"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "')) ";
                        }
                    }
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(Request["whereOrgan"]))
                {
                    if (Request["whereOrgan"] != "请选择")
                    {
                        where += " and (CONVERT(varchar,c.OrganId) = '" + Request["whereOrgan"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "' or CONVERT(varchar,c.OrganId) in (select Id from Organ_Detail where CONVERT(varchar,ParentId) = '" + Request["whereOrgan"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "')) ";
                    }
                }
            }

            //if (!String.IsNullOrEmpty(Request["whereOrganTo"]))
            //{
            //    if (QueryString.Decrypt(Request["whereOrganTo"]) != "" && QueryString.Decrypt(Request["whereOrganTo"]).ToString() != "请选择")
            //    {
            //        where += "and c.OrganId = '" + QueryString.Decrypt(Request["whereOrganTo"]) + "' ";
            //    }
            //    else
            //    {
            //        if (!String.IsNullOrEmpty(Request["whereOrgan"]))
            //        {
            //            if (QueryString.Decrypt(Request["whereOrgan"]) != "" && QueryString.Decrypt(Request["whereOrgan"]) != "请选择")
            //            {
            //                where += " and (c.OrganId = " + QueryString.Decrypt(Request["whereOrgan"]) + " or c.OrganId in (select Id from Organ_Detail where ParentId = " + QueryString.Decrypt(Request["whereOrgan"]) + ")) ";
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    if (!String.IsNullOrEmpty(Request["whereOrgan"]))
            //    {
            //        if (QueryString.Decrypt(Request["whereOrgan"]) != "" && QueryString.Decrypt(Request["whereOrgan"]) != "请选择")
            //        {
            //            where += " and (c.OrganId = " + QueryString.Decrypt(Request["whereOrgan"]) + " or c.OrganId in (select Id from Organ_Detail where ParentId = " + QueryString.Decrypt(Request["whereOrgan"]) + ")) ";
            //        }
            //    }
            //}



            if (!string.IsNullOrEmpty(Request["whereName"]))
            {
                where += " and (c.NickName like '%" + Request["whereName"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "%' or d.TeacherNo like '%" + Request["whereName"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "%' or d.CredentialsNumber like '%" + Request["whereName"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "%') ";
            }


            DataTable ColumnDt = bll.GetTrainingFie();
            ViewBag.ColumnDt = ColumnDt;
            DataTable table = bll.List(pageSize, pageIndex, where, "AccountId desc", out recordCount, PlanId);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                bool flag = true;
                for (int k = 0; k < ColumnDt.Rows.Count; k++)
                {

                    string[] item = table.Rows[i][ColumnDt.Rows[k][1].ToString()].ToString().Split('/');
                    if (item.Length == 2)
                    {
                        if (double.Parse(item[0].ToString()) < double.Parse(item[1].ToString()))
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                cbll.ResultAdd(Convert.ToInt32(table.Rows[i]["AccountId"].ToString()), PlanId, flag);
            }
            table = bll.GetListTo(pageSize, pageIndex, where, "AccountId desc", out recordCount, PlanId);
            ViewBag.GetList = table;
            pageCount = (int)Math.Ceiling((double)recordCount / pageSize);
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            ViewData["recordCount"] = recordCount;

            ViewData["whereOrgan"] = Request["whereOrgan"];
            ViewData["whereOrganTo"] = Request["whereOrganTo"];
            ViewData["whereName"] = Request["whereName"] == null ? "" : Request["whereName"];
            ViewData["GroupId"] = GroupId;
            ViewData["OrganId"] = OrganId;
            DataTable dt = new DataTable();
            if (GroupId == 1)
            {
                dt = planbll.GetOrgan(1);
            }
            else
            {
                dt = planbll.GetOrganqu(OrganId);
            }
            List<SelectListItem> selectlist = new List<SelectListItem>();
            List<SelectListItem> tolist = new List<SelectListItem>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = dt.Rows[i][1].ToString(),
                    Value = dt.Rows[i][0].ToString()
                };
                selectlist.Add(item);
            }
            ViewData["Organ"] = selectlist;
            ViewData["OrganTo"] = tolist;
            return View();
        }


        public ActionResult GetOrganTo(int id)
        {
            DataTable dt = planbll.GetOrgan(id);
            List<SelectListItem> list = new List<SelectListItem>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = dt.Rows[i][1].ToString(),
                    Value = dt.Rows[i][0].ToString()
                };
                list.Add(item);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSearch(int AccountId)
        {
            try
            {
                int PlanId = Code.SiteCache.Instance.PlanId;
                List<SearchModel> list = new List<SearchModel>();
                DataTable SearchDt = bll.GetSearch(AccountId, PlanId);
                for (int i = 0; i < SearchDt.Rows.Count; i++)
                {
                    SearchModel model = new SearchModel();
                    model.FidTitle = SearchDt.Rows[i]["FidTitle"].ToString();
                    model.DetailTitle = SearchDt.Rows[i]["DetailTitle"].ToString();
                    if (SearchDt.Rows[i]["Credits"] != DBNull.Value)
                    {
                        model.Credits = Convert.ToDouble(SearchDt.Rows[i]["Credits"]);
                    }
                    else
                    {
                        model.Credits = 0;
                    }
                    if (SearchDt.Rows[i]["CreateDate"] != DBNull.Value)
                    {
                        model.CreateDate = Convert.ToDateTime(SearchDt.Rows[i]["CreateDate"]);
                    }
                    else
                    {
                        model.CreateDate = DateTime.Now;
                    }
                    list.Add(model);
                }

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content("no:超时");
            }
        }


        public ActionResult Export(int AccountId, string RealNameTo)
        {
            DataTable dt = bll.GetSearch(AccountId, Code.SiteCache.Instance.PlanId);
            DataTable table = new DataTable();
            if (dt.Rows.Count == 0)
            {
                return Content("<script>alert('无导出数据！！！');location.href = '/Prepare/Period/Index';</script>");
            }
            else
            {
                string path = Server.MapPath("/Upload/Execl/Period/");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileName = RealNameTo + "用户学分记录" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
                string filePath = path + fileName;
                table.Columns.Add("类型");
                table.Columns.Add("课程名称");
                table.Columns.Add("学时");
                table.Columns.Add("获得日期");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dr[j] = dt.Rows[i][j].ToString().Replace("\n", "").Replace("\r", "");
                    }
                    table.Rows.Add(dr);
                }



                MemoryStream ms = new MemoryStream();
                IWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("sheet1");
                IRow headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("类型");
                headerRow.CreateCell(1).SetCellValue("课程名称");
                headerRow.CreateCell(2).SetCellValue("学时");
                headerRow.CreateCell(3).SetCellValue("获得日期");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(table.Rows[i][0].ToString());
                    rowtemp.CreateCell(1).SetCellValue(table.Rows[i][1].ToString());
                    rowtemp.CreateCell(2).SetCellValue(table.Rows[i][2].ToString());
                    rowtemp.CreateCell(3).SetCellValue(table.Rows[i][3].ToString());
                }
                workbook.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms, "application/vnd.ms-excel", fileName);
            }
        }
    }
}
