using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using XXW.Models;
using Dianda.CacheFactory;

namespace XXW.SiteUtils
{
    public class SessionHelper
    {
        const string CONST_LOGIN_PAGE = "~/default.aspx";
        static SessionHelper me = new SessionHelper();
        public static SessionHelper Instance
        {
            get { return me; }
            set { me = value; }
        }

        protected internal ISession ish = new MemCacheDSession();

        #region 通用方法，获取session中的一些值

        /// <summary>
        /// 平台管理员Id
        /// </summary>
        public int AccountId
        {
            get { return getObject<int>("AccountId", false); }
            set { setIntMark("AccountId", value); }
        }

        /// <summary>
        /// 分区Id
        /// </summary>
        public int PartitionId
        {
            get { return getObject<int>("PartitionId", false); }
            set { setIntMark("PartitionId", value); }
        }

        /// <summary>
        /// 组织Id
        /// </summary>
        public int OrganId
        {
            get { return getObject<int>("OrganId", false); }
            set { setIntMark("OrganId", value); }
        }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level
        {
            get { return getObject<int>("Level", false); }
            set { setIntMark("Level", value); }
        }

        /// <summary>
        /// 权限组Id
        /// </summary>
        public int GroupId
        {
            get { return getObject<int>("GroupId", false); }
            set { setIntMark("GroupId", value); }
        }

        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId
        {
            get { return getObject<int>("RoleId", false); }
            set { setIntMark("RoleId", value); }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return getObject<string>("UserName", false); }
            set { setObject<string>("UserName", value); }
        }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            get { return getObject<string>("NickName", false); }
            set { setObject<string>("NickName", value); }
        }

        /// <summary>
        /// 权限组名称
        /// </summary>
        public string GroupName
        {
            get { return getObject<string>("GroupName", false); }
            set { setObject<string>("GroupName", value); }
        }

        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidateCode
        {
            get { return getObject<string>("ValidateCode", false); }
            set { setObject<string>("ValidateCode", value); }
        }
        #endregion

        #region 基础方法
        public void SetSession(string key, object obj)
        {
            setObject(key, obj);
        }

        public object GetSession(string key)
        {

            return getObject(key, false);

        }

        public string GetSessionKey()
        {
            return ish.GetSessionKey();
        }
        /// <summary>
        /// 获取任意类型的对象，如果没有则执行跳转代码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="sessionName"></param>
        /// <returns></returns>
        protected T getObject<T>(string sessionName, bool gotoLoginPage)
        {

           
            object obj = ish.GetObject(sessionName);
            if (obj==null||!(obj is T))
            {
                if (gotoLoginPage)
                {
                    System.Web.UI.Page page = HttpContext.Current.Handler as System.Web.UI.Page;
                    if (page != null)
                    {
                        try
                        {

                            page.Response.Redirect(CONST_LOGIN_PAGE);

                        }
                        catch (System.Threading.ThreadAbortException e)
                        {

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                return default(T);
            }

            return (T)obj ;
        }

        protected object getObject(string sessionName, bool gotoLoginPage)
        {
            return ish.GetObject(sessionName);
        }

       

        protected  void setIntMark(string sessionName, int value)
        {

            if (HttpContext.Current.Session != null)
            {
                if (value > 0)
                {
                    ish.SetObject(sessionName, value);
                }
                else
                {
                    Remove(sessionName);
                }
            }

        }

        /// <summary>
        /// 获取任意类型的对象，如果没有则执行跳转代码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="sessionName"></param>
        /// <returns></returns>
        protected  void setObject<T>(string key, T value)
        {
            ish.SetObject(key, value);
        }

        protected void setObject(string key, object value)
        {
            ish.SetObject(key, value);
        }
        public void Clear()
        {

            ish.Clear();


        }
        public void Remove(string key)
        {
            ish.Remove(key);
        }
        /// <summary>
        /// 保持session的过期时间为最长
        /// </summary>
        public void Refresh()
        {
           
            ish.SetObject("sessionFlag", "1");
        }
        #endregion
    }
}
