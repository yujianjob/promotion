using Dianda.CacheFactory;
using Dianda.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXW.BLL;

namespace XXW.SiteUtils
{
    public class ParameterHelper
    {
       
        //public static string GetValue(string range,string key)
        //{
        //    string cacheKey = "SysParameter_" + range + "_" + key;
        //    object obj = Dianda.CacheFactory.CacheHelper.Instance.Get(cacheKey);
        //    if (obj == null)
        //    {
        //        BLL.SysParametersBLL g = new BLL.SysParametersBLL();
        //        obj= g.GetValue(range, key);
        //        if (obj != null)
        //        {
        //            Dianda.CacheFactory.CacheHelper.Instance.Add (cacheKey,obj);
        //            nameList.Add(cacheKey);
        //        }
        //    }
        //}

        static ParameterHelper me = new ParameterHelper();
        public static ParameterHelper Instance
        {
            get { return me; }
            set { me = value; }
        }

        #region circle

        private static string CIRCLE_COVER_LIMITED_SIZE = "XXW_CircleCoverLimitedSize";
        private static string CIRCLE_ATTACHMENT_PIC_LIMITED_SIZE = "XXW_CircleAttachmentPicLimitedSize";
        private static string CIRCLE_ATTACHMENT_FILE_LIMITED_SIZE = "XXW_CircleAttachmentFileLimitedSize";

        /// <summary>
        /// 学圈封面文件限制容量
        /// </summary>
        public int CircleCoverLimitedSize
        {
            get
            {
                return GetCacheValue(CIRCLE_COVER_LIMITED_SIZE, "UCircle", "PicSize");
            }
        }

        /// <summary>
        /// 学圈附件图片限制容量
        /// </summary>
        public int CircleAttachmentPicLimitedSize
        {
            get
            {
                return GetCacheValue(CIRCLE_ATTACHMENT_PIC_LIMITED_SIZE, "UCircle", "AttachPicSize");
            }
        }

        /// <summary>
        /// 学圈附件文件限制容量
        /// </summary>
        public int CircleAttachmentFileLimitedSize
        {
            get
            {
                return GetCacheValue(CIRCLE_ATTACHMENT_FILE_LIMITED_SIZE, "UCircle", "AttachFileSize");
            }
        }

        #endregion

        #region member

        private static string MEMBER_AVATAR_LIMITED_SIZE = "XXW_MemberAvatarLimitedSize";

        /// <summary>
        /// 会员头像文件限制容量
        /// </summary>
        public int MemberAvatarLimitedSize
        {
            get
            {
                return GetCacheValue(MEMBER_AVATAR_LIMITED_SIZE, "UMember", "UserPicSize");
            }
        }

        #endregion

        #region portal

        private static string ESPECIALLY_PLAN_LIMITED_SIZE = "XXW_EspeciallyPlanLimitedSize";

        /// <summary>
        /// 特别策划文件限制容量
        /// </summary>
        public int EspeciallyPlanLimitedSize
        {
            get
            {
                return GetCacheValue(ESPECIALLY_PLAN_LIMITED_SIZE, "UPortal", "SpPlan");
            }
        }

        private static string ESPECIALLY_PLAN_PREVIEW_LIMITED_SIZE = "XXW_EspeciallyPlanPreviewLimitedSize";

        /// <summary>
        /// 特别策划缩略图文件限制容量
        /// </summary>
        public int EspeciallyPlanPreviewLimitedSize
        {
            get
            {
                return GetCacheValue(ESPECIALLY_PLAN_PREVIEW_LIMITED_SIZE, "UPortal", "SpPlanPreview");
            }
        }

        private static string SPECIAL_RECOMMEND_COVER_LIMITED_SIZE = "XXW_SpecialRecommendCoverLimitedSize";

        /// <summary>
        /// 特别推荐封面文件限制容量
        /// </summary>
        public int SpecialRecommendCoverLimitedSize
        {
            get
            {
                return GetCacheValue(SPECIAL_RECOMMEND_COVER_LIMITED_SIZE, "UPortal", "SpRecommendCover");
            }
        }

        private static string LEVEL_ICO_LIMITED_SIZE = "XXW_LevelIcoLimitedSize";

        /// <summary>
        /// 等级图标文件限制容量
        /// </summary>
        public int LevelIcoLimitedSize
        {
            get
            {
                return GetCacheValue(LEVEL_ICO_LIMITED_SIZE, "ULevel", "LevelIcoSize");
            }
        }

        private static string _staticHomepagePath;
        public static string StaticHomepagePath {
            get { 
                if (string.IsNullOrEmpty(_staticHomepagePath))
                    _staticHomepagePath = (new SysParametersBLL()).GetValue("Sys", "PortalMainPage").ToString();
                return _staticHomepagePath;
            }
        }

        private static string HOMEPAGE_PATH_CACHE_KEY = "XXW_HomepagePath";

        /// <summary>
        /// 首页生成路径（包含路径和文件名）
        /// </summary>
        public string HomepagePath
        {
            get
            {
                object obj = CacheHelper.Instance.Get(HOMEPAGE_PATH_CACHE_KEY);
                if (obj == null)
                {
                    string path = (new SysParametersBLL()).GetValue("Sys", "PortalMainPage").ToString();
                    CacheHelper.Instance.Set(HOMEPAGE_PATH_CACHE_KEY, path);
                    return path;
                }
                else
                    return obj.ToString();
            }
        }



        #endregion

        #region Smart Study Encrypt Key

        private static string SMART_STUDY_ENCRYPT_KEY_CACHE_KEY = "XXW_SmartStudyEncryptKey";

        /// <summary>
        /// 特别策划文件限制容量
        /// </summary>
        public string SmartStudyEncryptKey
        {
            get
            {
                return GetCacheValue2(SMART_STUDY_ENCRYPT_KEY_CACHE_KEY, "Sys", "AcCode");
            }
        }
        #endregion

        private int GetCacheValue(string cacheKey, string dbRange, string dbKey)
        {
            object obj = CacheHelper.Instance.Get(cacheKey);
            if (obj == null)
            {
                int size = TypeConverter.ObjectToInt((new SysParametersBLL()).GetValue(dbRange, dbKey));
                CacheHelper.Instance.Set(cacheKey, size);
                return size;
            }
            else
                return (int)obj;
        }

        private string GetCacheValue2(string cacheKey, string dbRange, string dbKey)
        {
            object obj = CacheHelper.Instance.Get(cacheKey);
            if (obj == null)
            {
                obj = (new SysParametersBLL()).GetValue(dbRange, dbKey).ToString();
                string v = obj == null ? "" : obj.ToString();
                CacheHelper.Instance.Set(cacheKey, v);
                return v;
            }
            else
                return obj.ToString();
        }

    }
}
