using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.DAL
{
    public class Sys_ParametersDAL
    {
        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select ParameterValue from Sys_Parameters where ParameterKey=@Key");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Key", SqlDbType.VarChar, 20) { Value = key }
			};
            object obj = MSEntLibSqlHelper.ExecuteScalarBySql(sql.ToString(), cmdParams);
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
