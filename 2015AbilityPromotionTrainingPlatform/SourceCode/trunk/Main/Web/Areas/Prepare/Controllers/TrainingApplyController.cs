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

    public class TrainingApplyController : Controller
    {
        //课程申报
        // GET: /Prepare/CourseApply/

        //课程申报列表
        [ValidateInput(false)]
        public ActionResult List()
        {
            PrepareTrainingApplyBLL bll = new PrepareTrainingApplyBLL();

            int subjectId = Code.ExtendHelper.ToInt(Request["SubjectId"]);
            ViewBag.Subject = bll.GetSubject();
            ViewBag.SubjectId = subjectId;
            //ViewBag.OutCourseType = bll.GetOutCourseType("Delflag=0", "");

            int statusId = Code.ExtendHelper.ToInt(Request["StatusId"]);
            Dictionary<int, string> dict = new Dictionary<int, string>();
            int[] dictKey = { 1, 2, 3, 4, 5, 6 };
            string[] dictValue = { "草稿", "等待审核", "通过", "不通过", "已上架", "已下架" };
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
            where.Append("Delflag=0 and PartitionId=" + cache.LoginInfo.PartitionId + " and OrganId=" + cache.ManageOrganId);
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
            PrepareTrainingApplyBLL bll = new PrepareTrainingApplyBLL();
            Traning_Detail model = bll.GetTrainingDetail(id, "Delflag=0");
            if (model == null)
            {
                return Content("<script>setTimeout(function () { window.location.href = '/Prepare/TrainingApply/List' }, 3000);</script>操作失败,3秒后自动返回到列表！");
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

        //下载文件
        public ActionResult DownloadFile(string filePath, string fileName)
        {
            return File(filePath, "file", fileName);
        }

        //新增课程申报
        [HttpGet]
        public ActionResult Add()
        {
            PrepareTrainingApplyBLL bll = new PrepareTrainingApplyBLL();

            ViewBag.TrainingObjectData = bll.GetTraningObject();
            ViewBag.SubjectData = bll.GetSubject();
            ViewBag.StudyLevelData = bll.GetStudyLevel();
            ViewBag.TrainingFormData = bll.GetTrainingForm();
            ViewBag.TeacherTitleData = bll.GetTeacherTitle();
            ViewBag.TrainingFeildData = bll.GetTrainingField("Delflag=0 and IsSpec=0", "");
            ViewBag.OutCourseTypeData = bll.GetOutCourseType("Delflag=0", "");
            Organ_Detail otypeModel = new Organ_DetailBLL().GetModel(Code.SiteCache.Instance.ManageOrganId);
            ViewBag.OTypeId = otypeModel == null ? 0 : Convert.ToInt32(otypeModel.OType);
            return View();
        }

        //新增课程申报
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Traning_Detail model)
        {
            PrepareTrainingApplyBLL bll = new PrepareTrainingApplyBLL();
            ViewBag.TrainingObjectData = bll.GetTraningObject();
            ViewBag.SubjectData = bll.GetSubject();
            ViewBag.StudyLevelData = bll.GetStudyLevel();
            ViewBag.TrainingFormData = bll.GetTrainingForm();
            ViewBag.TeacherTitleData = bll.GetTeacherTitle();
            ViewBag.OutCourseTypeData = bll.GetOutCourseType("Delflag=0", "");
            Organ_Detail otypeModel = new Organ_DetailBLL().GetModel(Code.SiteCache.Instance.ManageOrganId);
            ViewBag.OTypeId = otypeModel == null ? 0 : Convert.ToInt32(otypeModel.OType);

            if (model.TraingCategory == 0)
            {
                TempData["Msg"] = "课程小类不能为空！";
                return View(model);
            }

            if (model.TraingTopic == null || model.TraingTopic == 0)
            {
                TempData["Msg"] = "课程主题不能为空！";
                return View(model);
            }

            if (string.IsNullOrEmpty(model.TeacherName))
            {
                TempData["Msg"] = "请输入讲师名称！";
                return View(model);
            }

            if (string.IsNullOrEmpty(model.TeacherFrom))
            {
                TempData["Msg"] = "请输入讲师来自什么地方！";
                return View(model);
            }

            string msg = "";
            if (model.Title == null)
                model.Title = "";
            else
                model.Title = model.Title.Trim();
            model.OrganId = Code.SiteCache.Instance.ManageOrganId;
            //if (bll.CheckExists(model.Title.Replace("'", "''"), Convert.ToInt32(model.OrganId), model.Id))
            //{
            //    TempData["Msg"] = "课程名称已存在";
            //    return View(model);
            //}

            int picType = Code.ExtendHelper.ToInt(Request["PicType"]);
            if (picType == 0)
            {
                TempData["Msg"] = "请选择图片！";
                return View(model);
            }
            else if (picType == 1)//选择提供的图片
            {
                model.Pic = Request["PicValue"];
            }
            else if (picType == 2)//选择上传的图片
            {
                HttpPostedFileBase pic = Request.Files["Pic"];
                string picPath = Code.UploadCore.UploadImage(pic, ref msg);
                if (picPath == "")
                {
                    TempData["Msg"] = msg;
                    return View(model);
                }
                model.Pic = picPath;
            }

            HttpPostedFileBase teacherPic = Request.Files["TeacherPic"];
            string teacherPicPath = Code.UploadCore.UploadImage(teacherPic, ref msg);
            if (teacherPicPath == "")
            {
                TempData["Msg"] = msg;
                return View(model);
            }
            model.TeacherPic = teacherPicPath;
            model.CreateDate = DateTime.Now;
            model.Creater = Code.SiteCache.Instance.ManagerId;
            model.PartitionId = Code.SiteCache.Instance.LoginInfo.PartitionId;
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

            if (Request["submit"] == "保存")
                model.Status = 1;
            if (Request["submit"] == "保存并提交")
                model.Status = 2;
            model.Display = true;

            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    //新增培训信息
                    bll.AddTrainingDetail(model);

                    //更新附件 
                    if (!string.IsNullOrEmpty(Request["AttachPathList"]))
                    {
                        DataTable dt = bll.GetAttachTable("1=0", "");
                        foreach (string s in Request["AttachPathList"].Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            DataRow row = dt.NewRow();
                            string[] file = s.Split('|');
                            row["Id"] = file[0];
                            row["TraningId"] = model.Id;
                            row["Title"] = file[1].Replace("'", "''");
                            row["Link"] = file[2];
                            row["Sort"] = 50;
                            row["Display"] = 1;
                            row["Delflag"] = 0;
                            row["CreateDate"] = DateTime.Now;
                            dt.Rows.Add(row);
                        }
                        bll.BatchAttach(dt);
                    }

                    if (model.Status == 2)//保存并提交时，新增一条申请流程记录
                    {
                        Traning_ApplyApplication apply = new Traning_ApplyApplication();
                        apply.TraningId = model.Id;
                        apply.Status = 1;
                        apply.Remark = "提交审核";
                        apply.Creater = model.Creater;
                        apply.OrganId = model.OrganId;
                        apply.Delflag = false;
                        apply.CreateDate = DateTime.Now;
                        bll.AddTrainingApply(apply);
                    }

                    trans.Complete();
                    return RedirectToAction("List");
                }
                catch (Exception)
                {
                    TempData["Msg"] = "保存失败！";
                    return View(model);
                }
            }
            
        }

        //编辑课程申报
        [HttpGet]
        [UrlDecrypt]
        public ActionResult Edit(int id)
        {
            PrepareTrainingApplyBLL bll = new PrepareTrainingApplyBLL();
            NationalAbility_CourseBLL nationalAbility_CourseBLL = new NationalAbility_CourseBLL();
            Traning_Detail model = bll.GetTrainingDetail(Convert.ToInt32(id), "Delflag=0 and (Status=1 or Status=4 or CanEdit=1)");
            if (model == null)
            {
                return Content("<script>setTimeout(function () { window.location.href = '/Prepare/TrainingApply/List' }, 3000);</script>操作失败,3秒后自动返回到列表！");
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
            Organ_Detail otypeModel = new Organ_DetailBLL().GetModel(Code.SiteCache.Instance.ManageOrganId);
            ViewBag.OTypeId = otypeModel == null ? 0 : Convert.ToInt32(otypeModel.OType);
            return View(model);
        }

        //编辑课程申报
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Traning_Detail model)
        {
            PrepareTrainingApplyBLL bll = new PrepareTrainingApplyBLL();

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
            if (picType == 0)
            {
                TempData["Msg"] = "请选择图片！";
                return Redirect("Edit?Id=" + Dianda.Common.QueryString.UrlEncrypt(model.Id));
            }
            else if (picType == 1)//选择提供的图片
            {
                model.Pic = Request["PicValue"];
            }
            else if (picType == 2)//选择上传的图片
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

            if (!model.CanEdit)
            {
                if (Request["submit"] == "保存")
                    model.Status = 1;
                if (Request["submit"] == "保存并提交")
                    model.Status = 2;
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
                    //更新培训信息
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

                    if (model.Status == 2)//保存并提交时，新增一条申请流程记录
                    {
                        Traning_ApplyApplication apply = new Traning_ApplyApplication();
                        apply.TraningId = model.Id;
                        apply.Status = 1;
                        apply.Remark = "提交审核";
                        apply.Creater = model.Creater;
                        apply.OrganId = model.OrganId;
                        apply.Delflag = false;
                        apply.CreateDate = DateTime.Now;
                        bll.AddTrainingApply(apply);
                    }

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

        //安排教师列表
        public ActionResult TeacherList(int trainingId)
        {
            PrepareTrainingApplyBLL bll = new PrepareTrainingApplyBLL();
            Code.SiteCache cache = Code.SiteCache.Instance;
            int partitionId = cache.LoginInfo.PartitionId;
            int organId = cache.ManageOrganId;

            StringBuilder where = new StringBuilder();
            string keyWords = "";
            if (!string.IsNullOrEmpty(Request["KeyWords"]))
                keyWords = Request["KeyWords"].Trim();
            ViewBag.KeyWords = keyWords;
            if (keyWords != "")
                where.Append("A.NickName like '%" + keyWords + "'");

            DataTable dt = new UtilsBLL().GetRecursion("Organ_Detail", "Id", "ParentId", organId);
            StringBuilder organ = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                organ.Append(row["Id"] + ",");
            }
            organ.Append("0");

            int recordCount = bll.GetTeacherCount(partitionId, organ.ToString(), where.ToString());
            int pageSize = 15, pageIndex;
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
            ViewBag.TrainingId = trainingId;

            List<Traning_Teacher> list = new Traning_TeacherBLL().GetList("Delflag=0 and TraningId=" + trainingId, "");
            StringBuilder ids = new StringBuilder();
            foreach (var item in list)
            {
                ids.Append(item.PlatformManagerId + ",");
            }
            if (ids.Length > 0)
                ids.Remove(ids.Length - 1, 1);
            ViewBag.SelectedTeacher = list;
            ViewData["SelectedIds"] = ids.ToString();

            return View(bll.GetTeacherList(pageSize, pageIndex, "B.Id", partitionId, organ.ToString(), where.ToString()).Rows);
        }

        #region Ajax
        //课程大类
        public ActionResult AjaxTrainingCategory(int fieldId)
        {
            List<Traning_Category> list = new PrepareTrainingApplyBLL().GetTrainingCategory("Delflag=0 and Field=" + fieldId, "Sort desc");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public class TopicAndNation
        {
            public List<Traning_Topic> topic{get;set;}
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

        //批量安排教师
        public ActionResult AjaxBatchSelectTeacher(int trainingId, string ids)
        {
            PrepareTrainingApplyBLL bll = new PrepareTrainingApplyBLL();
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(ids))
            {
                list.AddRange(ids.Split(','));
            }
            DataTable dt = new Traning_TeacherBLL().GetTable("Delflag=0 and TraningId=" + trainingId, "");

            int count = 0;
            foreach (string s in list)
            {
                if (dt.Select("PlatformManagerId=" + s).Count() == 0)
                {
                    DataRow row = dt.NewRow();
                    row["Id"] = 0;
                    row["TraningId"] = trainingId;
                    row["PlatformManagerId"] = Convert.ToInt32(s);
                    row["Status"] = 1;
                    row["Delflag"] = 0;
                    row["CreateDate"] = DateTime.Now;
                    dt.Rows.Add(row);
                    count++;
                }
            }
            foreach (DataRow row in dt.Rows)
            {
                if (!list.Exists(t => t == row["PlatformManagerId"].ToString()))
                {
                    row.Delete();
                    count++;
                }
            }

            if (count == 0)
            {
                return Json(new { Result = false, Msg = "请勾选或移除教师！" }, JsonRequestBehavior.AllowGet);
            }

            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    bll.BatchSelectTeacher(dt);
                    trans.Complete();

                    return Json(new { Result = true, Msg = "提交成功！" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { Result = false, Msg = "提交失败！" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        //上架、下架
        public ActionResult AjaxSetShelf(int trainingId, int status)
        {
            PrepareTrainingApplyBLL bll = new PrepareTrainingApplyBLL();
            Traning_Detail model = bll.GetTrainingDetail(trainingId, "Delflag=0");
            bool result;
            string msg;

            if (model == null)
            {
                result = false;
                msg = "记录不存在！";
            }
            else
            {
                if (status == 5)//上架
                {
                    if (model.Status == 5)
                    {
                        result = false;
                        msg = "课程已上架！";
                    }
                    else
                    {
                        model.Status = 5;
                        bll.UpdateTrainingDetail(model);
                        result = true;
                        msg = "上架成功！";
                    }
                }
                else if (status == 6)//下架
                {
                    if (model.Status == 6)
                    {
                        result = false;
                        msg = "课程已下架！";
                    }
                    else
                    {
                        model.Status = 6;
                        bll.UpdateTrainingDetail(model);
                        result = true;
                        msg = "下架成功！";
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

        //撤销提交审核
        public ActionResult AjaxRevoke(int trainingId)
        {
            PrepareTrainingApplyBLL bll = new PrepareTrainingApplyBLL();
            Traning_Detail model = bll.GetTrainingDetail(trainingId, "Delflag=0 and Status=2");
            bool result;
            string msg;

            if (model == null)
            {
                result = false;
                msg = "课程不在待审核状态中，请尝试刷新页面！";
            }
            else
            {
                if (model.Status == 1)
                {
                    result = false;
                    msg = "课程还未提交审核！";
                }
                else
                {
                    model.Status = 1;
                    bll.UpdateTrainingDetail(model);
                    result = true;
                    msg = "撤销成功！";
                }
            }

            return Json(new { Result = result, Msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxDelete(int trainingId)
        {
            PrepareTrainingApplyBLL bll = new PrepareTrainingApplyBLL();
            Traning_Detail model = bll.GetTrainingDetail(trainingId, "Delflag=0 and Status=1");
            bool result;
            string msg;

            if (model == null)
            {
                result = false;
                msg = "课程不在待审核状态中，请尝试刷新页面！";
            }
            else
            {
                model.Delflag = true;
                bll.UpdateTrainingDetail(model);
                result = true;
                msg = "删除成功！";
            }

            return Json(new { Result = result, Msg = msg }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
