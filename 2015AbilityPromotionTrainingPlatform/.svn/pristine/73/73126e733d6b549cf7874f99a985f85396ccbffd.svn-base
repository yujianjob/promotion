using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.CacheFactory
{
    public  class CacheCatalog
    {
        const string CONST_SEPARATOR = "_";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public  static string GetSession(string sessionId)
        {
            return "Session"+CONST_SEPARATOR+sessionId;
        }
        public static string GetTable(string tableName, string key)
        {
            return "Table" + CONST_SEPARATOR + tableName + CONST_SEPARATOR + key;
        }
        public static string GetTagsId(string name)
        {
            return "TagsId" + CONST_SEPARATOR + System.Web.HttpUtility .UrlEncode (name);
        }
        public static string GetTagsName(string id)
        {
            return "TagsName" + CONST_SEPARATOR + id;
        }
        /// <summary>
        /// 获取各个站点域名的key
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public  static string GetWebSiteDomain(string siteName)
        {
            return "SiteDomain" + CONST_SEPARATOR + siteName;
        }
        /// <summary>
        /// 获取生成静态文件的地址和名称规范
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static string GetStaticFilePath(string fileType)
        {
            return "StaticFilePath" + CONST_SEPARATOR + fileType;
        }
        /// <summary>
        /// 获取用户昵称的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetMemberNickName(string Id)
        {
            return "Member" + CONST_SEPARATOR +"NickName"+CONST_SEPARATOR+ Id;
        }

        public static string GetOther(string key)
        {
            return "Other" + CONST_SEPARATOR + key;
        }

    }
}
