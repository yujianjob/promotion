using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.DAL
{
    public class UtilsDAL
    {
        /// <summary>
        /// 获取递归Id值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="idName"></param>
        /// <param name="parentIdName"></param>
        /// <param name="currentId"></param>
        /// <returns></returns>
        public DataTable GetRecursion(string tableName, string idName, string parentIdName, int currentId)
        {
            SqlParameter[] cmdParams = new SqlParameter[]
            {
                new SqlParameter("@TableName", SqlDbType.VarChar, 500) { Value = tableName },
                new SqlParameter("@IdName", SqlDbType.VarChar, 500) { Value = idName },
                new SqlParameter("@ParentIdName", SqlDbType.VarChar, 500) { Value = parentIdName },
                new SqlParameter("@KeyId", SqlDbType.Int, 4) { Value = currentId }
            };
            return MSEntLibSqlHelper.ExecuteDataSetByStoreProcecdure("[dbo].[sp_recursion]", cmdParams).Tables[0];
        }
    }
}
