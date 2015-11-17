using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace XXW.DAL
{
    public class SysParametersDAL
    {
        /// <summary>
        /// 获取平台运行的最基本配置信息,缓存默认存放时间
        /// </summary>
        public int GetCacheExpiredTime()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ParameterValue  ");
            strSql.Append(" FROM Sys_Parameters where Range='Cache' and ParameterKey='ExpiredTime'  and delflag=0 ");


            object obj = Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteScalarBySql(strSql.ToString());
            int result = -1;
            if (obj != null && int.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            else
                return 600;
        }

        /// <summary>
        /// 获取平台运行的最基本配置信息,Session默认有效时间
        /// </summary>
        public int GetSessionExpiredTime()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ParameterValue  ");
            strSql.Append(" FROM Sys_Parameters where Range='Session' and ParameterKey='ExpiredTime'  and delflag=0 ");

            object obj = Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteScalarBySql(strSql.ToString());
            int result = -1;
            if (obj != null && int.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            else
                return 30;
        }

        /// <summary>
        /// Sys_Parameters获取值
        /// </summary>
        /// <param name="range"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetValue(string range, string key)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ParameterValue  ");
            strSql.Append(" FROM Sys_Parameters where Range='" + range + "' and ParameterKey='" + key + "'  and delflag=0 ");

            return Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteScalarBySql(strSql.ToString());

        }

        /// <summary>
        /// Sys_Parameters获取值
        /// </summary>
        /// <param name="range"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetValues(string range)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ParameterKey,ParameterValue  ");
            strSql.Append(" FROM Sys_Parameters where Range='" + range + "'   and delflag=0 ");
            Dictionary<string, string> result = new Dictionary<string, string>();
            System.Data.IDataReader dr = Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteDataReaderBySql(strSql.ToString());
            while (dr.Read())
            {
                result.Add(dr.GetString(0), dr.GetString(1));
            }
            dr.Close();
            return result;


        }

        ///// <summary>
        ///// 获取
        ///// </summary>
        ///// <returns></returns>
        //public Dictionary<string,string> GetSiteValue()
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select ParameterKey,ParameterValue  ");
        //    strSql.Append(" FROM Sys_Parameters where Range='Site' and delflag=0 ");
        //    Dictionary<string, string> result = new Dictionary<string, string>();
        //    System.Data .IDataReader dr= Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteDataReaderBySql(strSql.ToString());
        //    while (dr.Read())
        //    {
        //        result.Add(dr.GetString (0),dr.GetString (1));
        //    }
        //    dr.Close();
        //    return result;

        //}
    }

   

}
