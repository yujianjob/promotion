using Dianda.CacheFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XXW.BLL;
using XXW.Enum;

namespace XXW.SiteUtils
{
    public class SiteAddressHelper
    {

        static SiteAddressHelper me = new SiteAddressHelper();
        public static SiteAddressHelper Instance
        {
            get { return me; }
            private set { me = value; }
        }

        string SITE_DOMAIN_CACHE_KEY = "XXW_SiteDomain";

        public string SiteDomain
        {
            get
            {
                object obj = CacheHelper.Instance.Get(SITE_DOMAIN_CACHE_KEY);
                if (obj == null)
                {
                    obj = (new SysParametersBLL()).GetValue("Cache", "Domain");
                    string v = obj == null ? "" : obj.ToString();
                    CacheHelper.Instance.Set(SITE_DOMAIN_CACHE_KEY, v);
                    return v;
                }
                return obj.ToString();
            }
        }
      
        public SiteAddressHelper()
        {


        }

        private void initData()
        {


            Dictionary<string, string> dictSite = (new SysParametersBLL()).GetValues("Site");

            foreach (string key in dictSite.Keys)
            {
                CacheHelper.Instance.Add(CacheCatalog.GetWebSiteDomain(key), dictSite[key]);
            }




        }

        /// <summary>
        /// 重新加载所有的缓存数据
        /// </summary>
        public void ReLoad()
        {
            foreach (string item in System.Enum.GetNames(typeof(SiteTypeEnum)))
            {
                CacheHelper.Instance.Remove(CacheCatalog.GetWebSiteDomain(item));
            }
            //CacheHelper.Instance.Remove(COURSE);
            //CacheHelper.Instance.Remove(ACTIVITIES);
            //CacheHelper.Instance.Remove(READING);
            initData();
        }

        public static string ResourceSiteUrl
        {
            get
            {
                return SiteAddressHelper.Instance.GetUrl(SiteTypeEnum.ResourceUrl);
            }
        }

        /// <summary>
        /// 获取站点地址
        /// </summary>
        /// <param name="siteType"></param>
        /// <returns></returns>
        public string GetUrl(SiteTypeEnum type)
        {
            return getUrl(type.ToString());
        }

        string getUrl(string key)
        {
            object obj;
           string  cKey = CacheCatalog.GetWebSiteDomain(key);
           if (!CacheHelper.Instance.TryGet(cKey, out obj))
            {
                obj = (new SysParametersBLL()).GetValue("Site", key);
                if (obj != null)
                {
                    CacheHelper.Instance.Add(cKey, obj.ToString());
                    return obj.ToString();
                }
                else

                    return string.Empty;
            }
            else
                return obj.ToString();
        }

        public string GetUrlWithObjectType(ObjectTypeEnum type)
        {
            switch (type)
            {
                case  ObjectTypeEnum.ActivitiesApplyResult:
                    return getUrl(SiteTypeEnum.Activities.ToString ());
                case ObjectTypeEnum.ActivitiesInfo:
                    return getUrl(SiteTypeEnum.Activities.ToString());
                case ObjectTypeEnum.CircleInfo:
                    return getUrl(SiteTypeEnum.Circle.ToString());
                case ObjectTypeEnum.CircleInfoResult:
                    return getUrl(SiteTypeEnum.Circle.ToString());
                case ObjectTypeEnum.CourseInfo:
                    return getUrl(SiteTypeEnum.Course.ToString());
                case ObjectTypeEnum.CourseWareInfo:
                    return getUrl(SiteTypeEnum.Course.ToString());
                case ObjectTypeEnum.HotMember:        
                    return getUrl(SiteTypeEnum.Member.ToString());
                case ObjectTypeEnum.MemberFriendApply:
                    return getUrl(SiteTypeEnum.Member.ToString());
                case ObjectTypeEnum.MemberFriendResult:
                    return getUrl(SiteTypeEnum.Member.ToString());
                case ObjectTypeEnum.MsgRequest:
                    return getUrl(SiteTypeEnum.Member.ToString());
                case ObjectTypeEnum.MsgResponse:
                    return getUrl(SiteTypeEnum.Member.ToString());
                case ObjectTypeEnum.ReadingBook:
                    return getUrl(SiteTypeEnum.Reading.ToString());
                case ObjectTypeEnum.Share:
                    return getUrl(SiteTypeEnum.Share.ToString());
                case ObjectTypeEnum.TopicInfo:
                    return getUrl(SiteTypeEnum.Circle.ToString());
                case ObjectTypeEnum.InformationNews:
                    return getUrl(SiteTypeEnum.Information.ToString());
                default:
                    return getUrl(SiteTypeEnum.Portal.ToString ());
            }
        }

        public string GetRediectUrl(ObjectTypeEnum type, int? objectId)
        {
            return GetUrl(SiteTypeEnum.ResourceSite) + "Go/" + Dianda.Common.StringSecurity.DES.Encrypt(((int)type).ToString()+"," + (objectId.HasValue ?  objectId.Value.ToString() : SiteTypeEnum .Portal.ToString ()));
        }

        public string GetErrorPage(string errorMsg, int errorCode)
        {
            return getUrl(SiteTypeEnum.Portal.ToString()) + ErrorPage;
        }

        public string ErrorPage
        {
            get 
            {
                object obj = CacheHelper.Instance.Get(CacheCatalog.GetOther("Potral_ErrorPage"));
                if (obj == null)
                {
                    obj = (new SysParametersBLL()).GetValue("Sys", "ErrorPage");
                    CacheHelper.Instance.Set(CacheCatalog.GetOther("Potral_ErrorPage"), obj);
                    return obj.ToString();
                    //XXW.SiteUtils.SiteAddressHelper.Instance.GetUrl(SiteTypeEnum.ResourceUrl);
                }
                else
                    return obj.ToString();
            }
        }
    }
}
