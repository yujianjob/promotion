using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXW;
using XXW.BLL;

namespace Dianda.CacheFactory
{
    public static class CacheData
    {


        /////// <summary>
        /////// 获取用户昵称，没有则返回用户名
        /////// </summary>
        /////// <param name="accountId"></param>
        /////// <returns></returns>
        ////public static string GetNickName(int accountId)
        ////{

        ////    if (CacheHelper.Instance.Get(CacheCatalog.GetMemberNickName(accountId.ToString())) == null)
        ////    {
        ////        return  CacheHelper.Instance.Get(CacheCatalog.GetMemberNickName(accountId.ToString())).ToString ();

        ////    }
        ////    else
        ////    {
        ////        string names = (new Manager_DetailBLL()).GetNickName(accountId);
        ////        CacheHelper.Instance.Add(CacheCatalog.GetMemberNickName(accountId.ToString()), names);
        ////        return names;
        ////    }
        ////}
        ///// <summary>
        ///// 删除用户缓存信息
        ///// </summary>
        ///// <param name="accountId"></param>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //public static void RemoveMemberInfo(int accountId)
        //{
        //    CacheHelper.Instance.Remove(CacheCatalog.GetTable("MemberAccount", accountId.ToString()));

        //}

        //public static XXW.Models.MemberAccount GetMemberInfo(int accountId)
        //{
        //    object obj = CacheHelper.Instance.Get(CacheCatalog.GetTable("MemberAccount", accountId.ToString()));
        //    if (obj != null)
        //    {
        //        return obj as XXW.Models.MemberAccount;

        //    }
        //    else
        //    {
        //        XXW.Models.MemberAccount model = (new MemberAccountBLL()).GetMemberInfo(accountId);
        //        CacheHelper.Instance.Add(CacheCatalog.GetTable("MemberAccount", accountId.ToString()), model);
        //        return model;
        //    }
        //}
        //const int CONST_PORTAL_EXPERT_TIME = 30;//首页排行和推荐数据列表的缓存有效期。

        ///// <summary>
        ///// 获取首页推荐数据的列表。不是在前台显示数据的时候用的！！！
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public static List<int> GetPortalRecommendList(XXW.Enum.ObjectTypeEnum type)
        //{
            
        //    List<int> list=new List<int> ();
        //    object obj = Dianda.CacheFactory.CacheHelper.Instance.Get(Dianda.CacheFactory.CacheCatalog.GetOther(XXW. Enum.SiteTypeEnum.Portal.ToString() + "_RecList_"+((int)type).ToString () ));
        //    if (obj == null)
        //    {
        //       list=(new XXW.CommonBLL.Portal()).GetRecommendList(type);
        //       Dianda.CacheFactory.CacheHelper.Instance.Add(Dianda.CacheFactory.CacheCatalog.GetOther(XXW.Enum.SiteTypeEnum.Portal.ToString() + "_RecList_" + ((int)type).ToString()), list, new TimeSpan(0, 30, 0));

        //    }
        //    else
        //        list = obj as List<int>;
        //    return list;
    
        //}

        //public static void ClearPortalRecommendList(int objectType)
        //{
        //    Dianda.CacheFactory.CacheHelper.Instance.Remove(Dianda.CacheFactory.CacheCatalog.GetOther(XXW.Enum.SiteTypeEnum.Portal.ToString() + "_RecList_" + objectType.ToString()));
        //}

        //public static List<int> GetPortalRankList(XXW.Enum.ObjectTypeEnum type)
        //{

        //    List<int> list = new List<int>();
        //    object obj = Dianda.CacheFactory.CacheHelper.Instance.Get(Dianda.CacheFactory.CacheCatalog.GetOther(XXW.Enum.SiteTypeEnum.Portal.ToString() + "_RankList_" + ((int)type).ToString()));
        //    if (obj == null)
        //    {
        //        list = (new XXW.CommonBLL.Portal()).GetRankList(type);
        //        Dianda.CacheFactory.CacheHelper.Instance.Add(Dianda.CacheFactory.CacheCatalog.GetOther(XXW.Enum.SiteTypeEnum.Portal.ToString() + "_RankList_" + ((int)type).ToString()), list, new TimeSpan(0, 30, 0));

        //    }
        //    else
        //        list = obj as List<int>;
        //    return list;

        //}

        //public static void ClearPortalRankList(int objectType)
        //{
        //    Dianda.CacheFactory.CacheHelper.Instance.Remove(Dianda.CacheFactory.CacheCatalog.GetOther(XXW.Enum.SiteTypeEnum.Portal.ToString() + "_RankList_" + objectType.ToString()));
        //}
    }
}
