using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.DAL
{
    public class PagingQuery
    {
        public static DataTable GetPagingDataTable(string tableName, string where, string orderField,  int? PageIndex, out int TotalPage,string field="*",int baseCount=10)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT  * FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY " + orderField + @" DESC ) AS num , " + field + " ");
            sbSql.Append(" FROM     " + tableName + " where " + where );
            sbSql.Append(" ) AS d  WHERE   num BETWEEN " + ((PageIndex - 1) * baseCount + 1) + " and " + PageIndex * baseCount);
            sbSql.Append(" SELECT COUNT(1) FROM " + tableName + " where " + where );

            DataSet ds= MSEntLibSqlHelper.ExecuteDataSetBySql(sbSql.ToString());

            TotalPage =ds.Tables[1].Rows==null?0:Convert.ToInt32( ds.Tables[1].Rows[0][0]);
            return ds.Tables[0];
        }
    }
}
