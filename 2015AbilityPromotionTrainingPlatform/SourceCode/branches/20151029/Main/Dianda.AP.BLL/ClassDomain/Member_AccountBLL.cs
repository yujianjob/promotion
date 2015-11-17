using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.BLL
{
    public partial class Member_AccountBLL
    {
        /// <summary>
        /// 获取角色人员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetAccountByMannagerGroup(int PartitionId, int OrganId, int GroupId)
        {
            return this.dal.GetAccountByMannagerGroup(PartitionId,OrganId,GroupId);
        }
    }
}
