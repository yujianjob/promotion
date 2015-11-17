using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XXW.DAL;

namespace XXW.BLL
{
    public class GeneralBLL
    {
    }

    public class SysParametersBLL
    {
        public SysParametersDAL sysParamDal;
        public SysParametersBLL()
        {
            sysParamDal = new SysParametersDAL();
        }
        /// <summary>
        /// 获取平台运行的最基本配置信息,缓存默认存放时间
        /// </summary>
        public int GetCacheExpiredTime()
        {
            return sysParamDal.GetCacheExpiredTime();
        }

        /// <summary>
        /// 获取平台运行的最基本配置信息,Session默认有效时间
        /// </summary>
        public int GetSessionExpiredTime()
        {
            return sysParamDal.GetSessionExpiredTime();
        }

        /// <summary>
        /// Sys_Parameters获取值
        /// </summary>
        /// <param name="range"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetValue(string range, string key)
        {
            return sysParamDal.GetValue(range, key);
        }
        /// <summary>
        /// Sys_Parameters获取值
        /// </summary>
        /// <param name="range"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetValues(string range)
        {
            return sysParamDal.GetValues(range);
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
