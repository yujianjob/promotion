using Dianda.AP.DAL;
using Dianda.AP.Model.Learn.MyLearn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.BLL
{
    public class LearnMyPracticeBLL
    {
        LearnMyPracticeDAL dal = new LearnMyPracticeDAL();

        /// <summary>
        /// 获取我的实践总记录数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetMyPracticeCount(int accountId, int planId)
        {
            StringBuilder where = new StringBuilder();
            where.Append("A.Delflag=0 and A.PlanId=" + planId);
            where.Append(" and B.Delflag=0 and B.AccountId=" + accountId);
            where.Append(" and C.Delflag=0 and C.Status=2");
            where.Append(" and D.Delflag=0");
            return dal.GetMyPracticeCount(where.ToString());
        }

        /// <summary>
        /// 获取我的实践分页数据集
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<MyPracticeInfo> GetMyPracticeList(int pageSize, int pageIndex, string orderBy, int accountId, int planId)
        {
            StringBuilder where = new StringBuilder();
            where.Append("A.Delflag=0 and A.PlanId=" + planId);
            where.Append(" and B.Delflag=0 and B.AccountId=" + accountId);
            where.Append(" and C.Delflag=0 and C.Status=2");
            where.Append(" and D.Delflag=0");
            where.Append(" and E.Delflag=0");
            where.Append(" and F.Delflag=0");
            where.Append(" and G.Delflag=0");
            return dal.GetMyPracticeList(pageSize, pageIndex, where.ToString(), orderBy);
        }
    }
}
