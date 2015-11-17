using Dianda.AP.DAL;
using Dianda.AP.Model;
using Dianda.AP.Model.Entrance.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.BLL
{
    public class EntranceLoginBLL
    {
        /// <summary>
        /// 取得分区数据集
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Partition_Detail> GetPartition_DetailList(string where, string orderBy)
        {
            return new Partition_DetailDAL().GetList(where, orderBy);
        }

        /// <summary>
        /// 判断用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Login(string userName, string password, out int userId)
        {
            return new EntranceLoginDAL().Login(userName, password, out userId);
        }
    }
}
