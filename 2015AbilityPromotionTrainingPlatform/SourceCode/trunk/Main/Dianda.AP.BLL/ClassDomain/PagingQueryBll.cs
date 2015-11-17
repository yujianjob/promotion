using Dianda.AP.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.BLL
{
    public class PagingQueryBll
    {
        public static DataTable GetPagingDataTable(string tableName, string where, string orderField, int? PageIndex, out int TotalPage, string field = "*", int baseCount=10)
        {
            return PagingQuery.GetPagingDataTable(tableName, where, orderField, PageIndex, out TotalPage, field, baseCount);
        }
    }
}
