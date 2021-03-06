﻿using Dianda.AP.BLL;
using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Api.Controllers
{
    public class MemberController : Controller
    {
        //
        // GET: /Api/Member/

        //username:用户名
        //pwd:密码,utf-8的aes加密，然后urlencode编码。密钥：nltswx05
	    //code：校验码,md5加密 用户名+”shlllnet”+当前时间(2015082714，精确到小时)
        //Result:数字，1为成功，2为密码错误，3为该用户没有有效师训信息，4密码加密格式错误，5验证码错误，6某个参数为空。
        //Json格式:(Name:用户真实姓名,TeacherNumber:师训编号)
        [HttpPost]
        public ActionResult Login1()
        {
            //return Content(this.Encrypt("123456", "nltswx05nltswx05", "nltswx05nltswx05"));
            if (Request["userName"] == null || Request["pwd"] == null || Request["code"] == null)
            {
                return Json(new { Result = 6 });
            }

            string userName = HttpUtility.UrlDecode(Request["userName"]);
            string pwd = HttpUtility.UrlDecode(Request["pwd"]);
            string code = Request["code"];

            string checkCode = Dianda.Common.StringSecurity.MD5Lib.Encrypt(userName + "shlllnet" + DateTime.Now.ToString("yyyyMMddHH"));
            if (!code.Equals(checkCode, StringComparison.CurrentCultureIgnoreCase))
            {
                return Json(new { Result = 5 });
            }

            string aesKey = "nltswx05nltswx05";
            string aesIV = "nltswx05nltswx05";
            string decPwd = this.Decrypt(pwd, aesKey, aesIV).Replace("\0", "");
            
            if (string.IsNullOrEmpty(decPwd))
            {
                return Json(new { Result = 4 });
            }

            if (decPwd.Length > 11)
                decPwd = decPwd.Substring(0, 11);

            EntranceLoginBLL bll = new EntranceLoginBLL();

            int userId;
            if (bll.Login(userName, decPwd + "%", out userId))
            {
                Member_BaseInfo member = new Member_BaseInfoBLL().GetListModel("AccountId=" + userId).FirstOrDefault();
                if (member == null)
                {
                    return Json(new { Result = 3 });
                }
                else
                {
                    if (string.IsNullOrEmpty(member.RealName) || string.IsNullOrEmpty(member.TeacherNo))
                    {
                        return Json(new { Result = 3 });
                    }
                    else
                    {
                        var DataModel = new { Name = member.RealName, TeacherNumber = member.TeacherNo };
                        return Json(new { Result = 1, DataModel });
                    }
                }
            }
            else
            {
                return Json(new { Result = 2 });
            }
        }

        private string Encrypt(string toEncrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        private string Decrypt(string toDecrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
