using Dianda.CacheFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace XXW.SiteUtils
{
    public class AvatarHelper
    {
        private static string AVATAR_CACHE_KEY = "XXW_DefaultAvatarList";
        private static string _defaultAvatarFilePath;
        public static string DEFAULT_AVATAR_FILE_PATH
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultAvatarFilePath))
                    _defaultAvatarFilePath = "/File/Images/Predefine/";
                return _defaultAvatarFilePath;
            }
        }
        private static string _defaultAvatarFileFullPath;
        public static string DEFAULT_AVATAR_FILE_FULL_PATH {
            get {
                if (string.IsNullOrEmpty(_defaultAvatarFileFullPath))
                    _defaultAvatarFileFullPath = System.Web.HttpContext.Current.Server.MapPath(DEFAULT_AVATAR_FILE_PATH);
                return _defaultAvatarFileFullPath;
            }
        }

        /// <summary>
        /// 所有默认头像信息
        /// </summary>
        public static IList<string> AvatarList
        {
            get
            {
                object obj = CacheHelper.Instance.Get(AVATAR_CACHE_KEY);
                if (obj == null)
                {
                    IList<string> list = new List<string>();

                    DirectoryInfo dir = new DirectoryInfo(DEFAULT_AVATAR_FILE_FULL_PATH);
                    foreach (FileInfo file in dir.GetFiles()) {
                        string fileName = file.Name;
                        list.Add(fileName);
                    }

                    CacheHelper.Instance.Set(AVATAR_CACHE_KEY, list);
                    return list;
                }
                else
                    return (IList<string>)obj;
            }
        }



        private static string AVATAR_ICON_CACHE_KEY = "XXW_DefaultAvatarIconList";
        private static string _defaultAvatarIconFilePath;
        public static string DEFAULT_AVATAR_ICON_FILE_PATH
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultAvatarIconFilePath))
                    _defaultAvatarIconFilePath = "/File/Images/Predefine/icon/";
                return _defaultAvatarIconFilePath;
            }
        }
        private static string _defaultAvatarIconFileFullPath;
        public static string DEFAULT_AVATAR_ICON_FILE_FULL_PATH
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultAvatarIconFileFullPath))
                    _defaultAvatarIconFileFullPath = System.Web.HttpContext.Current.Server.MapPath(DEFAULT_AVATAR_ICON_FILE_PATH);
                return _defaultAvatarIconFileFullPath;
            }
        }

        /// <summary>
        /// 所有默认头像图标信息
        /// </summary>
        public static IList<string> AvatarIconList
        {
            get
            {
                object obj = CacheHelper.Instance.Get(AVATAR_ICON_CACHE_KEY);
                if (obj == null)
                {
                    IList<string> list = new List<string>();

                    DirectoryInfo dir = new DirectoryInfo(DEFAULT_AVATAR_ICON_FILE_FULL_PATH);
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        string fileName = file.Name;
                        list.Add(DEFAULT_AVATAR_ICON_FILE_PATH + fileName);
                    }

                    CacheHelper.Instance.Set(AVATAR_ICON_CACHE_KEY, list);
                    return list;
                }
                else
                    return (IList<string>)obj;
            }
        }

        public static string GetRandomAvatarIcon()
        {
            DateTime now = DateTime.Now;
            IList<string> list = AvatarHelper.AvatarIconList;
            Random ran = new Random();
            int i = ran.Next(0, list.Count - 1);
            return list[i];
        }
    }
}
