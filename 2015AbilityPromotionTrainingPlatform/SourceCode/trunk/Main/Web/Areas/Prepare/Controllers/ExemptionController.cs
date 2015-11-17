﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Dianda.AP.Model;
using Dianda.AP.BLL;
using System.Data;
using System.Data.OleDb;
using System.IO;
using HttpMultipartParser;
using NPOI;
using System.Web.Hosting;
using NPOI.HSSF.UserModel;
using System.Text.RegularExpressions;
using Dianda.Common;
using System.Web;

namespace Web.Areas.Prepare.Controllers
{
    public class ExemptionController : Controller
    {
        //
        // GET: /Prepare/Exemption/
        PlanExemptionBLL bll = new PlanExemptionBLL();

        public ActionResult Index()
        {
            int GroupId = Code.SiteCache.Instance.GroupId;
            int OrganId = Code.SiteCache.Instance.ManageOrganId;
            int PlanId = Code.SiteCache.Instance.PlanId;
            string where = " and PlanId = '" + PlanId + "' ";
            if (GroupId == 1)
            {

            }
            else if (GroupId == 2)
            {
                where += " and (d.OrganId = " + OrganId + " or d.OrganId in (select Id from Organ_Detail where ParentId = " + OrganId + " )) ";
            }
            else
            {
                where += " and d.OrganId = " + OrganId + " ";
            }

            int pageIndex = 1, pageSize = 10, pageCount, recordCount;
            if (!String.IsNullOrEmpty(Request["PageIndex"]))
                int.TryParse(QueryString.Decrypt(Request["PageIndex"]), out pageIndex);

            if (!String.IsNullOrEmpty(Request["whereOrganTo"]))
            {
                if (Request["whereOrganTo"].ToString() != "请选择")
                {
                    where += "and CONVERT(varchar,d.OrganId) = '" + Request["whereOrganTo"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "' ";
                }
                else
                {
                    if (!String.IsNullOrEmpty(Request["whereOrgan"]))
                    {
                        if (Request["whereOrgan"] != "请选择")
                        {
                            where += " and (CONVERT(varchar,d.OrganId) = '" + Request["whereOrgan"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "' or CONVERT(varchar,d.OrganId) in (select Id from Organ_Detail where CONVERT(varchar,ParentId) = '" + Request["whereOrgan"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "')) ";
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
                        where += " and (CONVERT(varchar,d.OrganId) = '" + Request["whereOrgan"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "' or CONVERT(varchar,d.OrganId) in (select Id from Organ_Detail where CONVERT(varchar,ParentId) = '" + Request["whereOrgan"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "')) ";
                    }
                }
            }

            if (!string.IsNullOrEmpty(Request["whereName"]))
            {
                where += " and (b.RealName like '%" + Request["whereName"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "%' or b.TeacherNo like '%" + Request["whereName"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "%' or b.CredentialsNumber like '%" + Request["whereName"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "%') ";
            }

            List<PlanExemption> list = new List<PlanExemption>();
            list = bll.GetList(pageSize, pageIndex, where, "a.Id desc", out recordCount);
            ViewBag.GetList = list;
            pageCount = (int)Math.Ceiling((double)recordCount / pageSize);
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            ViewData["recordCount"] = recordCount;
            ViewData["whereOrgan"] = Request["whereOrgan"] == null ? "" : Request["whereOrgan"];
            ViewData["whereOrganTo"] = Request["whereOrganTo"] == null ? "" : Request["whereOrganTo"];
            ViewData["whereName"] = Request["whereName"] == null ? "" : Request["whereName"];
            DataTable dt = new DataTable();
            if (GroupId == 1)
            {
                dt = bll.GetOrgan(1);
            }
            else
            {
                dt = bll.GetOrganqu(OrganId);
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
            DataTable dt = bll.GetOrgan(id);
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


        public ActionResult ExemExeclTemp()
        {
            string msg = "";
            HttpPostedFileBase execl = Request.Files[0];
            string fileName = Server.MapPath(Code.UploadCore.UploadAttach(execl, ref msg));
            int GroupId = Code.SiteCache.Instance.GroupId;
            int OrganId = Code.SiteCache.Instance.ManageOrganId;

            //DataTable dt = ExcelToDataTable(fileName, "sheet1");
            DataTable dt = ReadExcel(fileName);
            if (dt.Columns[0].ColumnName.ToString() != "姓名" || dt.Columns[1].ColumnName.ToString() != "师训编号" || dt.Columns[2].ColumnName.ToString() != "所属学校" || dt.Columns[3].ColumnName.ToString() != "免修依据" || dt.Columns[4].ColumnName.ToString() != "抵扣学分" || dt.Columns[5].ColumnName != "类型")
            {
                return Content("no:导入的格式不正确！！！");
            }
            else
            {
                List<PlanExemption> list = new List<PlanExemption>();
                if (dt.Rows.Count == 0)
                {
                    return Content("no:无导入数据！！！");
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                if (string.IsNullOrEmpty(dt.Rows[i][k].ToString()))
                                {
                                    return Content("no:导入的数据有空值！！！");
                                }
                            }

                            PlanExemption model = new PlanExemption();
                            model.PlanId = Code.SiteCache.Instance.PlanId;
                            model.Status = 0;
                            model.Remark = dt.Rows[i][3].ToString();
                            if (Regex.IsMatch(dt.Rows[i][4].ToString(), @"^[+-]?\d*[.]?\d*$"))
                            {
                                model.Credits = Convert.ToDouble(dt.Rows[i][4]);
                            }
                            else
                            {
                                return Content("no:抵扣学分必须是数字类型！！！");
                            }
                            if (dt.Rows[i][4].ToString().Length > 5)
                            {
                                return Content("no:抵扣学分数字长度不能超过五位数！！！");
                            }
                            if (dt.Rows[i][4].ToString() == "0")
                            {
                                return Content("no:第" + (i + 1) + "行的抵扣学分不能为0");
                            }
                            if (GroupId == 2)
                            {
                                if (!bll.GetSchool(dt.Rows[i][2].ToString(), OrganId))
                                {
                                    return Content("no:请导入本区内的学校学员数据！！！");
                                }
                            }
                            else if (GroupId == 3 || GroupId == 4)
                            {
                                if (!bll.GetSchoolTo(dt.Rows[i][2].ToString(), OrganId))
                                {
                                    return Content("no:请导入本学校或本机构内的学员数据！！！");
                                }
                            }

                            model.Creater = Code.SiteCache.Instance.LoginInfo.UserId;
                            model.AccountId = bll.GetAccount(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
                            if (model.AccountId == 0 || model.AccountId == null)
                            {
                                return Content("no:导入数据的第" + (i + 1) + "行用户数据有误,请检查用户姓名、师训编号与用户所属学校是否正确！！！");
                            }
                            if (dt.Rows[i][5].ToString() != "通识课程" && dt.Rows[i][5].ToString() != "专业课程" && dt.Rows[i][5].ToString() != "实践应用课程")
                            {
                                return Content("no:请选择正确类型！！！");
                            }
                            else
                            {
                                model.PEType = dt.Rows[i][5].ToString() == "通识课程" ? 1 : dt.Rows[i][5].ToString() == "专业课程" ? 2 : dt.Rows[i][5].ToString() == "实践应用课程" ? 3 : 0;
                            }
                            model.Delflag = false;
                            model.CreateDate = DateTime.Now;
                            list.Add(model);
                        }
                        catch (Exception ex)
                        {
                            return Content("no:" + ex);
                        }
                    }

                    int count = bll.AddExem(list);
                    if (count > 0)
                    {
                        return Content("yes:成功导入" + count + "行数据");
                    }
                    else
                    {
                        return Content("no:导入失败");
                    }

                }
            }
        }

        public bool bolNum(string Credits)
        {
            for (int i = 0; i < Credits.Length; i++)
            {
                byte tempByte = Convert.ToByte(Credits[i]);
                if ((tempByte < 48) || (tempByte > 57))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 导出模板
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportExecl()
        {
            return File(new FileStream(Server.MapPath("/Upload/Execl/Demo/CreditExemption.xls"), FileMode.Open), "application/octet-stream", "CreditExemption.xls");
        }


        public ActionResult DelExem(int id)
        {
            if (bll.DelExem(id))
            {
                return Content("yes:删除成功！！！");
            }
            else
            {
                return Content("no:删除失败！！！");
            }
        }


        public ActionResult GetModel(int id)
        {
            PlanExemption model = bll.GetModel(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ExemEdit(int id, string TeacherNo, string Remark, double Credits, int PEType)
        {
            PlanExemption model = new PlanExemption();
            model.Id = id;
            model.AccountId = bll.GetTeacherNo(TeacherNo);
            if (model.AccountId == 0 || model.AccountId == null)
            {
                return Content("no:师训编号不存在！！！");
            }
            model.Remark = Remark;
            model.Credits = Credits;
            model.PEType = PEType;
            if (bll.ExemEdit(model))
            {
                return Content("yes:修改成功！！！");
            }
            else
            {
                return Content("no:修改失败！！！");
            }
        }


        /// <summary>
        /// 读取Execl
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataTable ReadExcel(string fileName)
        {
            DataTable dt = new DataTable();
            if (fileName != null)
            {
                dt = ImportExcelFile(fileName);
            }
            //文件是否存在  
            if (System.IO.File.Exists(fileName))
            {

            }
            return dt;
        }

        /// <summary>
        /// 将导入数据转换成DataTable
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataTable ImportExcelFile(string fileName)
        {
            HSSFWorkbook hssfworkbook;
            #region//初始化信息
            try
            {
                using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion

            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            DataTable dt = new DataTable();
            rows.MoveNext();
            HSSFRow row = (HSSFRow)rows.Current;
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                //dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());  
                //将第一列作为列表头  
                dt.Columns.Add(row.GetCell(j).ToString());
            }
            while (rows.MoveNext())
            {
                row = (HSSFRow)rows.Current;
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

    }
}
