using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XXW.Enum;
using Dianda.CacheFactory;
using XXW.BLL;

namespace XXW.SiteUtils
{

    public static class StaticFilesGen
    {


        static StaticFilesGen()
        {
            init();
        }



        //public static string GetFilePath(ObjectTypeEnum type, string Id, bool isEncode, DateTime now)
        //{
        //   return  GetFilePath(type, Id, isEncode, now.ToString("yyyyMMddHHmmss"));
        //}

        /// <summary>
        /// 只有CourseInfo,ActivitiesInfo,ReadingEBook,CourseWareInfo才有值。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetFilePath(ObjectTypeEnum type, string Id, bool isEncode)
        {
            return getFilePath(type, Id, string.Empty, isEncode);
        }


        public static string GetFileOutPath(ObjectTypeEnum type, string Id, bool isEncode)
        {
            return getFilePath(type, Id, "_o", isEncode);
        }
        /// <summary>
        /// 只有CourseInfo,ActivitiesInfo,ReadingEBook,CourseWareInfo才有值。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string getFilePath(ObjectTypeEnum type, string Id, string prefix, bool isEncode)
        {
            Id = Id + prefix;
            return string.Format(getFilePath(type.ToString()), isEncode ? encode(Id) : Id);
        }
        //public static string GetFilePath(ObjectTypeEnum type, string Id,bool isEncode,string code)
        //{
        //    if (string.IsNullOrEmpty(code))
        //        return string.Format(getFilePath(type.ToString()), isEncode ? encode(Id) : Id);
        //    else
        //        return string.Format(getFilePath(type.ToString()), isEncode ? encode(Id) : Id) + "?" + code;

        //}

        public static string GetFileNameTemplate(ObjectTypeEnum type)
        {
            return getFilePath(type.ToString());
        }
        /// <summary>
        /// 重新加载所有的缓存数据
        /// </summary>
        public static void ReLoad()
        {
            //CacheHelper.Instance.Remove(Dianda.CacheFactory .CacheCatalog.GetStaticFilePath(ObjectTypeEnum.CourseInfo.ToString()));
            //CacheHelper.Instance.Remove(Dianda.CacheFactory .CacheCatalog.GetStaticFilePath(ObjectTypeEnum.ActivitiesInfo.ToString()));
            //CacheHelper.Instance.Remove(Dianda.CacheFactory .CacheCatalog.GetStaticFilePath(ObjectTypeEnum.CourseWareInfo.ToString()));
            //CacheHelper.Instance.Remove(Dianda.CacheFactory .CacheCatalog.GetStaticFilePath(ObjectTypeEnum.ReadingBook.ToString()));
            init();
        }
        static void init()
        {
            Dictionary<string, string> DictFilePath = (new SysParametersBLL()).GetValues("Template");
            foreach (string key in DictFilePath.Keys)
            {
                CacheHelper.Instance.Remove(Dianda.CacheFactory.CacheCatalog.GetStaticFilePath(key));
                CacheHelper.Instance.Add(Dianda.CacheFactory.CacheCatalog.GetStaticFilePath(key), DictFilePath[key]);
            }
        }

        //文件名称编码类。
        static string encode(string name)
        {
            return name;
        }
        static string getFilePath(string key)
        {
            object obj;
            string cKey = Dianda.CacheFactory.CacheCatalog.GetStaticFilePath(key);
            if (!CacheHelper.Instance.TryGet(cKey, out obj))
            {
                obj = (new SysParametersBLL()).GetValue("Template", key);
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

        #region 区县文件地址
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Id"></param>
        /// <param name="RegionId"></param>
        /// <param name="isEncode"></param>
        /// <returns></returns>
        public static string GetFilePath(string Key, string Id, string RegionId, bool isEncode)
        {
            return getFilePath(Key, Id, RegionId, string.Empty, isEncode);
        }
        private static string getFilePath(string Key, string Id, string RegionId, string prefix, bool isEncode)
        {
            Id = Id + prefix;
            return string.Format(getFilePath(Key), isEncode ? encode(Id) : Id, isEncode ? encode(RegionId) : RegionId);
        }
        #endregion
    }
}
