﻿using Dianda.AP.BLL;
using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Web.Attributes;

namespace Web.Areas.Prepare.Controllers
{
    public class TrainingCheckController : Controller
    {
        //课程审核
        // GET: /Prepare/TrainingCheck/

        //课程审核列表
        [ValidateInput(false)]
        public ActionResult List()
        {
            PrepareTrainingCheckBLL bll = new PrepareTrainingCheckBLL();

            int subjectId = Code.ExtendHelper.ToInt(Request["SubjectId"]);
            ViewBag.Subject = bll.GetSubject();
            ViewBag.SubjectId = subjectId;

            int statusId = Code.ExtendHelper.ToInt(Request["StatusId"]);
            Dictionary<int, string> dict = new Dictionary<int, string>();
            int[] dictKey = { 2, 3, 4, 5, 6 };
            string[] dictValue = { "等待审核", "通过", "不通过", "已上架", "已下架" };
            for (int i = 0; i < dictKey.Length; i++)
            {
                dict.Add(dictKey[i], dictValue[i]);
            }
            ViewBag.Status = dict;
            ViewBag.StatusId = statusId;

            string keyWords = "";
            if (!string.IsNullOrEmpty(Request["KeyWords"]))
                keyWords = Request["KeyWords"].Trim();
            ViewBag.KeyWords = keyWords;

            Code.SiteCache cache = Code.SiteCache.Instance;
            StringBuilder where = new StringBuilder();
            where.Append("Delflag=0 and Status<>1 and PartitionId=" + cache.LoginInfo.PartitionId);
            if (keyWords != "")
                where.Append(" and Title like '%" + keyWords.Replace("'", "''") + "%'");
            if (subjectId != 0)
                where.Append(" and " + subjectId + " in (select value from [dbo].[split](Subject,','))");
            if (statusId != 0)
                where.Append(" and Status=" + statusId);
            int recordCount = bll.GetTrainingInfoCount(where.ToString());

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
            ViewBag.SubjectData = bll.GetSubject();

            return View(bll.GetTrainingInfoList(pageSize, pageIndex, where.ToString(), "CreateDate desc"));
        }

        //课程详细
        [UrlDecrypt]
        public ActionResult Details(int id)
        {
            PrepareTrainingCheckBLL bll = new PrepareTrainingCheckBLL();
            Traning_Detail model = bll.GetTrainingDetail(id, "Delflag=0");
            if (model == null)
            {
                return Content("<script>setTimeout(function () { window.location.href = '/Prepare/TrainingCheck/List' }, 3000);</script>操作失败,3秒后自动返回到列表！");
            }

            ViewBag.TrainingObjectData = bll.GetTraningObject();
            ViewBag.SubjectData = bll.GetSubject();
            ViewBag.StudyLevelData = bll.GetStudyLevel();
            ViewBag.TrainingFormData = bll.GetTrainingForm();
            ViewBag.TeacherTitleData = bll.GetTeacherTitle();
            ViewBag.TrainingFeildData = bll.GetTrainingField("Delflag=0 and IsSpec=0", "");
            ViewBag.TrainingCategoryData = bll.GetTrainingCategory("Delflag=0 and Field=" + model.TraingField, "");
            ViewBag.TrainingTopicData = bll.GetTrainingTopic("Delflag=0 and CategoryId=" + model.TraingCategory, "");
            ViewBag.AttachData = bll.GetAttachTable("Delflag=0 and TraningId=" + id, "");
            ViewBag.OutCourseTypeData = bll.GetOutCourseType("Delflag=0", "");
            ViewBag.NationalCoursData = new NationalAbility_CourseBLL().GetList2("Delflag=0 and TCategoryId=" + model.TraingCategory, "");
            return View(model);
        }

        //编辑课程
        [HttpGet]
        [UrlDecrypt]
        public ActionResult Edit(int id)
        {
            PrepareTrainingCheckBLL bll = new PrepareTrainingCheckBLL();
            NationalAbility_CourseBLL nationalAbility_CourseBLL = new NationalAbility_CourseBLL();
            Traning_Detail model = bll.GetTrainingDetail(id, "Delflag=0 and Status<>1");
            if (model == null)
            {
                return Content("<script>setTimeout(function () { window.location.href = '/Prepare/TrainingCheck/List' }, 3000);</script>操作失败,3秒后自动返回到列表！");
            }

            ViewBag.TrainingObjectData = bll.GetTraningObject();
            ViewBag.SubjectData = bll.GetSubject();
            ViewBag.StudyLevelData = bll.GetStudyLevel();
            ViewBag.TrainingFormData = bll.GetTrainingForm();
            ViewBag.TeacherTitleData = bll.GetTeacherTitle();
            ViewBag.TrainingFeildData = bll.GetTrainingField("Delflag=0 and IsSpec=0", "");
            ViewBag.TrainingCategoryData = bll.GetTrainingCategory("Delflag=0 and Field=" + model.TraingField, "");
            ViewBag.TrainingTopicData = bll.GetTrainingTopic("Delflag=0 and CategoryId=" + model.TraingCategory, "");
            ViewBag.NationalCoursData = nationalAbility_CourseBLL.GetList2("Delflag=0 and TCategoryId=" + model.TraingCategory, "");
            ViewBag.AttachData = bll.GetAttachTable("Delflag=0 and TraningId=" + id, "");
            ViewBag.OutCourseTypeData = bll.GetOutCourseType("Delflag=0", "");
            Organ_Detail otypeModel = new Organ_DetailBLL().GetModel(Convert.ToInt32(model.OrganId));
            ViewBag.OTypeId = otypeModel == null ? 0 : Convert.ToInt32(otypeModel.OType);
            return View(model);
        }

        //编辑课程
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Traning_Detail model)
        {
            PrepareTrainingCheckBLL bll = new PrepareTrainingCheckBLL();

            //ViewBag.TrainingObjectData = bll.GetTraningObject();
            //ViewBag.SubjectData = bll.GetSubject();
            //ViewBag.StudyLevelData = bll.GetStudyLevel();
            //ViewBag.TrainingFormData = bll.GetTrainingForm();
            //ViewBag.TeacherTitleData = bll.GetTeacherTitle();
            //ViewBag.TrainingFeildData = bll.GetTrainingField("Delflag=0 and IsSpec=0", "");
            //ViewBag.TrainingCategoryData = bll.GetTrainingCategory("Delflag=0 and Field=" + model.TraingField, "");
            //ViewBag.TrainingTopicData = bll.GetTrainingTopic("Delflag=0 and CategoryId=" + model.TraingCategory, "");
            //DataTable attachData = bll.GetAttachTable("Delflag=0 and TraningId=" + model.Id, "");
            //ViewBag.AttachData = attachData;
            //ViewBag.OutCourseTypeData = bll.GetOutCourseType("Delflag=0", "");

            if (model.TraingCategory == 0)
            {
                TempData["Msg"] = "课程小类不能为空！";
                return Redirect("Edit?Id=" + Dianda.Common.QueryString.UrlEncrypt(model.Id));
            }

            if (model.TraingTopic == null || model.TraingTopic == 0)
            {
                TempData["Msg"] = "课程主题不能为空！";
                return Redirect("Edit?Id=" + Dianda.Common.QueryString.UrlEncrypt(model.Id));
            }

            if (string.IsNullOrEmpty(model.TeacherName))
            {
                TempData["Msg"] = "请输入讲师名称！";
                return Redirect("Edit?Id=" + Dianda.Common.QueryString.UrlEncrypt(model.Id));
            }

            if (string.IsNullOrEmpty(model.TeacherFrom))
            {
                TempData["Msg"] = "请输入讲师来自什么地方！";
                return Redirect("Edit?Id=" + Dianda.Common.QueryString.UrlEncrypt(model.Id));
            }

            string msg = "";

            if (model.Title == null)
                model.Title = "";
            else
                model.Title = model.Title.Trim();
            //if (bll.CheckExists(model.Title.Replace("'", "''"), Convert.ToInt32(model.OrganId), model.Id))
            //{
            //    TempData["Msg"] = "课程名称已存在";
            //    return Redirect("Edit?Id=" + Dianda.Common.QueryString.UrlEncrypt(model.Id));
            //}

            int picType = Code.ExtendHelper.ToInt(Request["PicType"]);
            if (picType == 1)//选择提供的图片
            {
                model.Pic = Request["PicValue"];
            }
            if (picType == 2)//选择上传的图片
            {
                HttpPostedFileBase pic = Request.Files["Pic"];
                string picPath = Code.UploadCore.UploadImage(pic, ref msg);
                if (picPath == "")
                {
                    TempData["Msg"] = msg;
                    return Redirect("Edit?Id=" + Dianda.Common.QueryString.UrlEncrypt(model.Id));
                }
                model.Pic = picPath;
            }

            if (Request.Files["TeacherPic"] != null)
            {
                if (Request.Files["TeacherPic"].ContentLength > 0)
                {
                    string teacherPicPath = Code.UploadCore.UploadImage(Request.Files["TeacherPic"], ref msg);
                    if (teacherPicPath == "")
                    {
                        TempData["Msg"] = msg;
                        return Redirect("Edit?Id=" + Dianda.Common.QueryString.UrlEncrypt(model.Id));
                    }
                    model.TeacherPic = teacherPicPath;
                }
                else
                {
                    model.TeacherPic = Request["TeacherPicValue"];
                }
            }
            else
            {
                model.TeacherPic = Request["TeacherPicValue"];
            }

            if (model.Range == 1)//区级时更新上级机构Id
                model.ParentOrganId = bll.GetParentOrganId(Convert.ToInt32(model.OrganId));
            if (model.Range == 2)//市级
                model.ParentOrganId = null;
            int outCourseType = Code.ExtendHelper.ToInt(Request["OutCourseType"]);
            if (model.OutSideType == -1)//内部平台
            {
                model.OutSideLink = null;
                model.OutSideContent = null;
            }
            else//外部平台
            {
                model.OutSideType = outCourseType;
            }

            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    //更新培训
                    bll.UpdateTrainingDetail(model);

                    //更新附件
                    DataTable attach = bll.GetAttachTable("Delflag=0 and TraningId=" + model.Id, "");
                    List<string> attachIdList = new List<string>();
                    if (!string.IsNullOrEmpty(Request["AttachPathList"]))
                    {
                        foreach (string s in Request["AttachPathList"].Split(':'))
                        {
                            string[] file = s.Split('|');
                            if (file[0] == "0")
                            {
                                DataRow row = attach.NewRow();
                                row["Id"] = file[0];
                                row["TraningId"] = model.Id;
                                row["Title"] = file[1].Replace("'", "''");
                                row["Link"] = file[2];
                                row["Sort"] = 50;
                                row["Display"] = 1;
                                row["Delflag"] = 0;
                                row["CreateDate"] = DateTime.Now;
                                attach.Rows.Add(row);
                            }
                            else
                            {
                                attachIdList.Add(file[0]);
                            }
                        }
                    }
                    foreach (DataRow row in attach.Rows)
                    {
                        if (row.RowState == DataRowState.Unchanged && !attachIdList.Contains(row["Id"].ToString()))
                            row.Delete();
                    }
                    bll.BatchAttach(attach);

                    trans.Complete();
                    return RedirectToAction("List");
                }
                catch (Exception)
                {
                    TempData["Msg"] = "保存失败！";
                    return Redirect("Edit?Id=" + Dianda.Common.QueryString.UrlEncrypt(model.Id));
                }
            }
        }

        //审核课程
        [HttpGet]
        [UrlDecrypt]
        public ActionResult Check(int id)
        {
            PrepareTrainingCheckBLL bll = new PrepareTrainingCheckBLL();
            Traning_Detail model = bll.GetTrainingDetail(id, "Delflag=0 and Status in (2,4)");
            if (model == null)
            {
                return Content("<script>setTimeout(function () { window.location.href = '/Prepare/TrainingCheck/List' }, 3000);</script>操作失败,3秒后自动返回到列表！");
            }

            ViewBag.TrainingObjectData = bll.GetTraningObject();
            ViewBag.SubjectData = bll.GetSubject();
            ViewBag.StudyLevelData = bll.GetStudyLevel();
            ViewBag.TrainingFormData = bll.GetTrainingForm();
            ViewBag.TeacherTitleData = bll.GetTeacherTitle();
            ViewBag.TrainingFeildData = bll.GetTrainingField("Delflag=0 and IsSpec=0", "");
            ViewBag.TrainingCategoryData = bll.GetTrainingCategory("Delflag=0 and Field=" + model.TraingField, "");
            ViewBag.TrainingTopicData = bll.GetTrainingTopic("Delflag=0 and CategoryId=" + model.TraingCategory, "");
            ViewBag.AttachData = bll.GetAttachTable("Delflag=0 and TraningId=" + id, "");
            ViewBag.OutCourseTypeData = bll.GetOutCourseType("Delflag=0", "");
            ViewBag.NationalCoursData = new NationalAbility_CourseBLL().GetList2("Delflag=0 and TCategoryId=" + model.TraingCategory, "");
            return View(model);
        }

        //审核课程
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Check(int id, string checkResult, string unpassRemark)
        {
            PrepareTrainingCheckBLL bll = new PrepareTrainingCheckBLL();
            Traning_Detail model = bll.GetTrainingDetail(id, "Delflag=0 and Status in (2,4)");
            if (model == null)
            {
                return Content("<script>setTimeout(function () { window.location.href = '/Prepare/TrainingCheck/List' }, 3000);</script>操作失败,3秒后自动返回到列表！");
            }

            if (checkResult == "1")//审核通过
            {
                model.Status = 3;
                model.ApplyRemark = null;
            }
            else//审核不通过
            {
                if (unpassRemark.Trim() == "")
                {
                    TempData["Msg"] = "不通过原因不能为空白！";
                    return Redirect("Check?Id=" + Dianda.Common.QueryString.UrlEncrypt(id));
                }
                model.Status = 4;
                model.ApplyRemark = unpassRemark.Trim();
            }

            int managerId = Code.SiteCache.Instance.ManagerId;
            int partitionId = Code.SiteCache.Instance.LoginInfo.PartitionId;

            Traning_ApplyApplication apply = new Traning_ApplyApplication();
            apply.TraningId = model.Id;
            apply.Status = Convert.ToInt32(checkResult);
            apply.Remark = model.ApplyRemark;
            apply.Creater = managerId;
            apply.OrganId = Code.SiteCache.Instance.ManageOrganId;
            apply.Delflag = false;
            apply.CreateDate = DateTime.Now;

            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    bll.UpdateTrainingDetail(model);//更新课程
                    bll.AddTrainingApply(apply);//新增一条课程审核记录
                    string msg = "";
                    if (model.Status == 3)
                    {
                        msg = string.Format("课程：{0} 审核通过！", model.Title);
                    }
                    else
                    {
                        msg = string.Format("课程：{0} 审核不通过，原因：{1}", model.Title, model.ApplyRemark);
                    }
                    Code.MsgHelper.sendMsg(model.Creater, managerId, partitionId, "审核信息", msg);//新增消息通知
                    trans.Complete();
                    return RedirectToAction("List");
                }
                catch
                {
                    TempData["Msg"] = "操作失败！";
                    return Redirect("Check?Id=" + Dianda.Common.QueryString.UrlEncrypt(id));
                }
            }
        }

        //下载文件
        public ActionResult DownloadFile(string filePath,string fileName)
        {
            return File(filePath, "file", fileName);
        }

        #region Ajax
        //课程小类
        public ActionResult AjaxTrainingCategory(int fieldId)
        {
            List<Traning_Category> list = new PrepareTrainingCheckBLL().GetTrainingCategory("Delflag=0 and Field=" + fieldId, "Sort desc");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public class TopicAndNation
        {
            public List<Traning_Topic> topic { get; set; }
            public List<NationalAbility_Course> nation { get; set; }
        }

        //课程主题
        public ActionResult AjaxTrainingTopic(int categoryId)
        {
            List<Traning_Topic> listT = new PrepareTrainingApplyBLL().GetTrainingTopic("Delflag=0 and CategoryId=" + categoryId, "Sort desc");

            List<NationalAbility_Course> listN = new NationalAbility_CourseBLL().GetList2("Delflag=0 and Display=1 and TCategoryId=" + categoryId, "Sort");
            TopicAndNation topicAndNation = new TopicAndNation();
            topicAndNation.topic = listT;
            topicAndNation.nation = listN;
            return Json(topicAndNation, JsonRequestBehavior.AllowGet); 
        }

        //课程审核
        public ActionResult AjaxTrainingCheck(int id, string value, string remark)
        {
            PrepareTrainingCheckBLL bll = new PrepareTrainingCheckBLL();
            Traning_Detail model = bll.GetTrainingDetail(id, "Delflag=0 and Status in (2,4) and PartitionId=" + Code.SiteCache.Instance.LoginInfo.PartitionId);
            if (model == null)
            {
                return Json(new { Result = true, Msg = "记录不存在！" }, JsonRequestBehavior.AllowGet);
            }

            if (value == "1")//审核通过
            {
                model.Status = 3;
                model.ApplyRemark = "";
            }
            else//审核不通过
            {
                model.Status = 4;
                model.ApplyRemark = remark;
            }

            Traning_ApplyApplication apply = new Traning_ApplyApplication();
            apply.TraningId = model.Id;
            apply.Status = Convert.ToInt32(value);
            apply.Remark = model.ApplyRemark;
            apply.Creater = Code.SiteCache.Instance.LoginInfo.UserId;
            apply.OrganId = Code.SiteCache.Instance.ManageOrganId;
            apply.Delflag = false;
            apply.CreateDate = DateTime.Now;

            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    bll.UpdateTrainingDetail(model);//更新课程
                    bll.AddTrainingApply(apply);//新增一条课程审核记录
                    trans.Complete();
                    return Json(new { Result = true, Msg ="操作成功！" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { Result = false, Msg = "操作失败！" }, JsonRequestBehavior.AllowGet);
                }
            }
            
        }

        //批量审核
        public ActionResult AjaxTrainingCheckBatch(string ids, string value, string remark)
        {
            StringBuilder where = new StringBuilder();
            where.Append("Delflag=0 and Status in (2,4) and PartitionId=" + Code.SiteCache.Instance.LoginInfo.PartitionId
                + " and Id in (" + ids + ")");
            int status = value == "1" ? 3 : 4;
            remark = value == "1" ? "" : remark;
            int managerId = Code.SiteCache.Instance.ManagerId;
            List<Traning_ApplyApplication> applies = new List<Traning_ApplyApplication>();
            foreach (string id in ids.Split(','))
            {
                Traning_ApplyApplication apply = new Traning_ApplyApplication();
                apply.TraningId = Convert.ToInt32(id);
                apply.Status = Convert.ToInt32(value);
                apply.Remark = remark;
                apply.Creater = managerId;
                apply.OrganId = Code.SiteCache.Instance.ManageOrganId;
                apply.Delflag = false;
                apply.CreateDate = DateTime.Now;
                applies.Add(apply);
            }

            List<Traning_Detail> trainings = new Traning_DetailBLL().GetList("Id in (" + ids + ")", "");

            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    int partitionId = Code.SiteCache.Instance.LoginInfo.PartitionId;
                    string msg;
                    foreach (Traning_Detail training in trainings)
                    {
                        if (status == 3)
                        {
                            msg = string.Format("课程：{0} 审核通过！", training.Title);
                        }
                        else
                        {
                            msg = string.Format("课程：{0} 审核不通过，原因：{1}", training.Title, remark);
                        }
                        Code.MsgHelper.sendMsg(training.Creater, managerId, partitionId, "审核信息", msg);
                    }
                    
                    new PrepareTrainingCheckBLL().BatchTrainingApply(where.ToString(), status, remark, applies);
                    trans.Complete();
                    return Json(new { Result = true, Msg = "操作成功！" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { Result = false, Msg = "操作失败！" }, JsonRequestBehavior.AllowGet);
                }
            }
            
        }

        //开放关闭编辑
        public ActionResult AjaxEditStatus(int id, int value)
        {
            PrepareTrainingCheckBLL bll = new PrepareTrainingCheckBLL();
            Traning_Detail model = bll.GetTrainingDetail(id, "Delflag=0");
            bool result;
            string msg;

            if (model == null)
            {
                result = false;
                msg = "记录不存在！";
            }
            else
            {
                if (value == 1)//开放编辑
                {
                    if (model.CanEdit)
                    {
                        result = false;
                        msg = "课程已开放编辑！";
                    }
                    else
                    {
                        model.CanEdit = true;
                        bll.UpdateTrainingDetail(model);
                        result = true;
                        msg = "开放编辑成功！";
                    }
                }
                else if (value == 0)//关闭编辑
                {
                    if (!model.CanEdit)
                    {
                        result = false;
                        msg = "课程已关闭编辑！";
                    }
                    else
                    {
                        model.CanEdit = false;
                        bll.UpdateTrainingDetail(model);
                        result = true;
                        msg = "关闭编辑成功！";
                    }
                }
                else
                {
                    result = false;
                    msg = "操作失败！";
                }
            }

            return Json(new { Result = result, Msg = msg }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
