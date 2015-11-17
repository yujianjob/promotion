using Dianda.AP.DAL;
using Dianda.AP.Model;
using Dianda.AP.Model.Entrance.Home;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.BLL
{
    public class EntranceHomeBLL
    {
        /// <summary>
        /// 取得一条用户账号信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Member_Account GetMember_AccountModel(int id, string where)
        {
            return new Member_AccountDAL().GetModel(id, where);
        }

        public Member_Account GetMember_AccountModel(string where)
        {
            return new Member_AccountDAL().GetModel(where);
        }

        public Member_BaseInfo GetModelByAccountId(int accountId)
        {
            return new Member_BaseInfoDAL().GetModelByAccountId(accountId);
        }


        /// <summary>
        /// 取得平台管理员权限组
        /// </summary>
        /// <returns></returns>
        public List<PlatformGroups> GetPlatformGroupsList(int partitionId, int userId)
        {
            List<PlatformManager_Detail> managers = new PlatformManager_DetailDAL().GetList("Delflag=0 and PartitionId=" + partitionId + " and AccountId=" + userId, "");
            List<PlatformManager_Groups> roles = new PlatformManager_GroupsDAL().GetList("Delflag=0", "");
            List<PlatformGroups> list = new List<PlatformGroups>();
            var result = from m in managers
                         join r in roles on m.GroupId equals r.Id
                         //into rr
                         //from r in rr.DefaultIfEmpty
                         //(
                         //     new PlatformManager_Groups { Id = 0, Title = "", Role = "" }
                         //)
                         orderby r.Id
                         select new PlatformGroups { Id = m.GroupId, Title = r.Title, ManageOrganId = m.OrganId, ManagerId = m.Id };
            return result.ToList();
        }

        /// <summary>
        /// 取得通知提醒数量
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="partitionId"></param>
        /// <returns></returns>
        public int GetNotifyCount(int accountId, int partitionId)
        {
            return new EntranceHomeDAL().GetNotifyCount(accountId, partitionId);
        }

        /// <summary>
        /// 取得培训计划
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public DataSet GetTrainingPlanList(string where, string orderBy)
        {
            return new Training_PlanDAL().GetList(1, where, orderBy);
        }

        /// <summary>
        /// 取得平台管理员权限数据集
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<PlatformRoles> GetPlatformRolesList(string where, string orderBy)
        {
            return new EntranceHomeDAL().GetPlatformRolesList(where, orderBy);
        }

        /// <summary>
        /// 取得权限组信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public PlatformManager_Groups GetPlatformGroup(int id, string where)
        {
            return new PlatformManager_GroupsDAL().GetModel(id, where);
        }

        /// <summary>
        /// 取得菜单数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<PlatformMenu> GetPlatformMenusList(string where, string orderBy)
        {
            return new EntranceHomeDAL().GetPlatformMenusList(where, orderBy);
        }
    }
}
