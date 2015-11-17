using Dianda.AP.Model;
using Dianda.AP.Model.Entrance.Home;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.DAL
{
    public class EntranceHomeDAL
    {
        /// <summary>
        /// 取得通知提醒数量
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="partitionId"></param>
        /// <returns></returns>
        public int GetNotifyCount(int accountId, int partitionId)
        {
            string sql = "select count(1) from Sys_Message where Delflag=0 and Status=0 and AccountId=" + accountId + " and PartitionId=" + partitionId;
            return Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sql.ToString()));
        }

        /// <summary>
        /// 取得平台管理员权限数据集
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<PlatformRoles> GetPlatformRolesList(string where, string orderBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from [dbo].[PlatformManager_Role]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            if (!string.IsNullOrEmpty(orderBy))
                sql.Append(" order by " + orderBy);
            List<PlatformRoles> list = new List<PlatformRoles>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    PlatformRoles model = new PlatformRoles();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 取得菜单数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<PlatformMenu> GetPlatformMenusList(string where, string orderBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from [dbo].[PlatformManager_Role]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            if (!string.IsNullOrEmpty(orderBy))
                sql.Append(" order by " + orderBy);
            List<PlatformMenu> list = new List<PlatformMenu>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    PlatformMenu model = new PlatformMenu();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        private void ConvertToModel(IDataReader reader, PlatformRoles model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["PagePath"] != DBNull.Value)
                model.PagePath = reader["PagePath"].ToString();
        }

        private void ConvertToModel(IDataReader reader, PlatformMenu model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["ParentId"] != DBNull.Value)
                model.ParentId = Convert.ToInt32(reader["ParentId"]);
            if (reader["MenuText"] != DBNull.Value)
                model.MenuText = reader["MenuText"].ToString();
            if (reader["IsFolder"] != DBNull.Value)
                model.IsFolder = Convert.ToBoolean(reader["IsFolder"]);
            if (reader["PagePath"] != DBNull.Value)
                model.PagePath = reader["PagePath"].ToString();
            if (reader["MenuPath"] != DBNull.Value)
                model.MenuPath = reader["MenuPath"].ToString();
        }
    }
}
