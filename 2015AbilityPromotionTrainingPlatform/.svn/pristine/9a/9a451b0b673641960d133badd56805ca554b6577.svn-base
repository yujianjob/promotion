using Dianda.AP.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.BLL
{
    public class UtilsBLL
    {
        UtilsDAL dal = new UtilsDAL();

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
            return dal.GetRecursion(tableName, idName, parentIdName, currentId);
        } 
    }
}
