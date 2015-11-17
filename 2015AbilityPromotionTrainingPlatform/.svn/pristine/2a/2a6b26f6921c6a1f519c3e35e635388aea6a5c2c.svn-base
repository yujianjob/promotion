using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Dianda.AP.BLL;
using Dianda.AP.Model;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Text;
using Dianda.Common;
using Web.Attributes;
using NPOI.SS.UserModel;

namespace Web.Areas.Prepare.Controllers
{
    public class CertificateController : Controller
    {
        //
        // GET: /Prepare/Certificate/

        CertificateBLL bll = new CertificateBLL();
        PlanExemptionBLL planbll = new PlanExemptionBLL();

        [UrlDecrypt]
        public ActionResult Index()
        {
            
            int pageIndex = 1, pageSize = 10, pageCount, recordCount;
            if (!String.IsNullOrEmpty(Request["PageIndex"]))
                int.TryParse(QueryString.Decrypt(Request["PageIndex"]), out pageIndex);

            int GroupId = Code.SiteCache.Instance.GroupId;    //权限组ID
            int PlanId = Code.SiteCache.Instance.PlanId;    //本学期ID
            int PartitionId = Code.SiteCache.Instance.LoginInfo.PartitionId;     //本区ID
            int OrganId = Code.SiteCache.Instance.ManageOrganId;    //机构ID
            string where = " and a.PlanId = " + PlanId + " ";

            if (GroupId == 1)
            { 
                
            }
            else if (GroupId == 2)
            {
                where += " and (c.OrganId = " + OrganId + " or c.OrganId in (select Id from Organ_Detail where ParentId = " + OrganId + " )) ";
            }
            else
            {
                where += " and c.OrganId = " + OrganId + " ";
            }


            if (!String.IsNullOrEmpty(Request["whereOrganTo"]))
            {
                if (Request["whereOrganTo"].ToString() != "请选择")
                {
                    where += "and c.OrganId = '" + Request["whereOrganTo"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "' ";
                }
                else
                {
                    if (!String.IsNullOrEmpty(Request["whereOrgan"]))
                    {
                        if (Request["whereOrgan"] != "请选择")
                        {
                            where += " and (c.OrganId = " + Request["whereOrgan"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + " or c.OrganId in (select Id from Organ_Detail where ParentId = " + Request["whereOrgan"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + ")) ";
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
                        where += " and (c.OrganId = " + Request["whereOrgan"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + " or c.OrganId in (select Id from Organ_Detail where ParentId = " + Request["whereOrgan"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + ")) ";
                    }
                }
            }

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
                bll.ResultAdd(Convert.ToInt32(table.Rows[i]["AccountId"].ToString()), PlanId, flag);
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


        public ActionResult Export()
        {
            
            int GroupId = Code.SiteCache.Instance.GroupId;    //权限组ID
            int PlanId = Code.SiteCache.Instance.PlanId;    //本学期ID
            int PartitionId = Code.SiteCache.Instance.LoginInfo.PartitionId;     //本区ID
            int OrganId = Code.SiteCache.Instance.ManageOrganId;    //机构ID
            string where = " and a.PlanId = " + PlanId + "";
            if (GroupId == 1)
            {

            }
            else if (GroupId == 2)
            {
                where += " and (c.OrganId = " + OrganId + " or c.OrganId in (select Id from Organ_Detail where ParentId = " + OrganId + ") ) ";
            }
            else
            {
                where += " and c.OrganId = " + OrganId + " ";
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

            if (!string.IsNullOrEmpty(Request["whereName"]))
            {
                where += " and (c.NickName like '%" + Request["whereName"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "%' or d.TeacherNo like '%" + Request["whereName"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "%' or d.CredentialsNumber like '%" + Request["whereName"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "%') ";
            }

            DataTable ComDt = bll.GetTrainingFie();
            DataTable dt = bll.GetExport(where, "AccountId desc", PlanId);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool flag = true;
                for (int k = 0; k < ComDt.Rows.Count; k++)
                {

                    string[] item = dt.Rows[i][ComDt.Rows[k][1].ToString()].ToString().Split('/');
                    if (item.Length == 2)
                    {
                        if (double.Parse(item[0].ToString()) <= double.Parse(item[1].ToString()))
                        {
                            flag = false;
                        }
                    }
                    bll.ResultAdd(Convert.ToInt32(dt.Rows[i]["AccountId"].ToString()), PlanId, flag);
                }
            }
            dt = bll.GetExport(where, "AccountId desc", PlanId);

            if (dt.Rows.Count == 0)
            {
                return Content("<script>alert('无导出数据！！！');location.href = '/Prepare/Certificate/Index?whereName=" + Request["EwhereName"] + "&whereOrganTo=" + Request["EwhereOrganTo"] + "&whereOrgan=" + Request["EwhereOrgan"] + "';</script>");
            }
            else
            {
                DataTable table = new DataTable();
                string path = Server.MapPath("/Upload/Execl/Certificate/");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileName = "证书记录" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                string filePath = path + fileName;

                MemoryStream ms = new MemoryStream();
                IWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("sheet1");
                IRow headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("姓名");
                headerRow.CreateCell(1).SetCellValue("师训编号");

                for (int i = 0; i < ComDt.Rows.Count; i++)
                {
                    headerRow.CreateCell(i + 2).SetCellValue(ComDt.Rows[i][1].ToString());
                }
                headerRow.CreateCell(ComDt.Rows.Count + 3).SetCellValue("结业状态");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(dt.Rows[i]["RealName"].ToString());
                    rowtemp.CreateCell(1).SetCellValue(dt.Rows[i]["TeacherNo"].ToString());
                    for (int j = 0; j < ComDt.Rows.Count; j++)
                    {
                        if (dt.Rows[i][ComDt.Rows[j][1].ToString()].ToString() != "" && dt.Rows[i][ComDt.Rows[j][1].ToString()].ToString() != null)
                        {
                            string[] src = dt.Rows[i][ComDt.Rows[j][1].ToString()].ToString().Split('/');
                            rowtemp.CreateCell(j + 2).SetCellValue(src[0] + "\\" + src[1]);
                        }
                        else
                        {
                            rowtemp.CreateCell(j + 2).SetCellValue("0" + "\\" + "0");
                        }
                    }

                    if (dt.Rows[i]["Result"].ToString() == "2")
                    {
                        rowtemp.CreateCell(ComDt.Rows.Count + 3).SetCellValue("已完成");
                    }
                    else
                    {
                        rowtemp.CreateCell(ComDt.Rows.Count + 3).SetCellValue("未完成");
                    }
                }
                workbook.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms, "application/vnd.ms-excel", fileName);
            }
        }


        public ActionResult GetSearch(int AccountId)
        {
            int PlanId = Code.SiteCache.Instance.PlanId;
            List<SearchList> list = new List<SearchList>();
            DataTable SearchDt = bll.GetSearch(AccountId, PlanId);
            for (int i = 0; i < SearchDt.Rows.Count; i++)
            {
                SearchList model = new SearchList();
                model.Title = SearchDt.Rows[i]["FidTitle"].ToString();
                if (SearchDt.Rows[i]["TotalTime"] != DBNull.Value)
                {
                    model.Credits = Convert.ToInt32(SearchDt.Rows[i]["TotalTime"]);
                }
                else
                {
                    model.Credits = 0;
                }
                list.Add(model);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchExport(int AccountId, string RealNameTo, string CertificateCodeTo, string TeacherNoTo)
        {
            DataTable dt = bll.GetSearch(AccountId, Code.SiteCache.Instance.PlanId);

            if (dt.Rows.Count == 0)
            {
                return Content("<script>alert('无导出数据！！！');location.href = '/Prepare/Certificate/Index?whereName=" + Request["EwhereName"] + "&whereOrganTo=" + Request["EwhereOrganTo"] + "&whereOrgan=" + Request["EwhereOrgan"] + "';</script>");
            }
            else
            {
                string path = Server.MapPath("/Upload/Execl/Certificate/");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileName = RealNameTo + "用户结业证书" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
                string filePath = path + fileName;


                MemoryStream ms = new MemoryStream();
                IWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("sheet1");
                for (int i = 0; i < 4; i++)
                {
                    IRow headerRow = sheet.CreateRow(i);
                    if (i == 0)
                    {
                        headerRow.CreateCell(0).SetCellValue("姓名");
                        headerRow.CreateCell(1).SetCellValue(RealNameTo);
                    }
                    else if (i == 1)
                    {
                        headerRow.CreateCell(0).SetCellValue("师训编号");
                        headerRow.CreateCell(1).SetCellValue(TeacherNoTo);
                    }
                    else if (i == 2)
                    {
                        headerRow.CreateCell(0).SetCellValue("毕业证号");
                        headerRow.CreateCell(1).SetCellValue(CertificateCodeTo);
                    }
                    else
                    {
                        headerRow.CreateCell(0).SetCellValue("类型");
                        headerRow.CreateCell(1).SetCellValue("学时");
                    }
                }
                

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow rowtemp = sheet.CreateRow(4 + i);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        rowtemp.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString().Replace("\n", "").Replace("\r", ""));
                    }
                }
                workbook.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms, "application/vnd.ms-excel", fileName);
            }
        }


        public ActionResult CertificateCode(int AccountId)
        {
            if (bll.CertificateCode(AccountId, Code.SiteCache.Instance.PlanId))
            {
                return Content("yes:生成成功！！！");
            }
            else
            {
                return Content("no:生成失败！！！");
            }
        }
    }
}
