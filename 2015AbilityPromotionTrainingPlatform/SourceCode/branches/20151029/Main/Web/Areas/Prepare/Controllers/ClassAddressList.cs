﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dianda.AP.Model;
using Dianda.AP.BLL;
using System.Data;
using XXW.Enum;
using Web.Attributes;

namespace Web.Areas.Prepare.Controllers
{
    public class ClassAddressListController : Controller
    {
        //
        // GET: /Prepare/Standard_Setting/
        [UrlDecrypt]
        public ActionResult Index(int TrainingId, int ClassId)
        {
            Course_DetailBLL bll_detail = new Course_DetailBLL();
            Member_BaseInfoBLL bll = new Member_BaseInfoBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //指定班级的ID
            ViewBag.ClassId = ClassId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll_detail.GetTrainingInfoById(Convert.ToInt32(TrainingId));
            //获取班级通讯录信息
            ViewBag.MemberInfo = bll.GetMemberInfo(TrainingId,ClassId);
            
            return View();
        }
    }
}
