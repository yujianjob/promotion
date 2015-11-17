using Dianda.AP.DAL;
using Dianda.AP.Model;
using Dianda.AP.Model.Learn.MyLearn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.BLL
{
    public class LearnMyLearnBLL
    {
        LearnMyLearnDAL dal = new LearnMyLearnDAL();

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Class_Detail model)
        {
            return dal.Add(model) > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Class_Detail model)
        {
            return dal.Update(model) > 0;
        }

        /// <summary>
        /// 取得一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Class_Detail GetModel(int id, string where)
        {
            return dal.GetModel(id, where);
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Class_Detail> GetList(string where, string orderBy)
        {
            return dal.GetList(where, orderBy);
        }

        /// <summary>
        /// 获取分页数据集
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<Class_Detail> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, where, orderBy, out recordCount);
        }

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public DataTable GetTable(string where, string orderBy)
        {
            return dal.GetTable(where, orderBy);
        }

        /// <summary>
        /// 获取分页数据集
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataTable GetTable(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            return dal.GetTable(pageSize, pageIndex, where, orderBy, out recordCount);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="partitionId"></param>
        /// <param name="status">5：学习中；3：未开始；6：已结束</param>
        /// <returns></returns>
        //public int GetMyLearnCount(int accountId, int partitionId, string status)
        //{
        //    StringBuilder where = new StringBuilder();
        //    where.Append("A.Delflag=0 and A.Status=4 and A.AccountId=" + accountId);
        //    if (status == "6")//已结束的课程需要判断时间
        //        where.Append(" and B.Delflag=0 and (B.Status in (" + status + ") or B.OpenClassTo<" + DateTime.Now.ToShortDateString() + ")");
        //    else
        //        where.Append(" and B.Delflag=0 and B.Status in (" + status + ")");
        //    where.Append(" and C.Delflag=0 and C.PartitionId=" + partitionId);
        //    where.Append(" and D.Delflag=0");
        //    where.Append(" and E.Delflag=0 and E.Delflag=0");
        //    return dal.GetMyLearnCount(where.ToString());
        //}

        /// <summary>
        /// 取得我的课程分页数据集
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderBy"></param>
        /// <param name="accountId"></param>
        /// <param name="partitionId"></param>
        /// <param name="status">5：学习中；3：未开始；6：已结束</param>
        /// <returns></returns>
        //public List<MyLearnInfo> GetMyLearnList(int pageSize, int pageIndex, string orderBy, int accountId, int partitionId, string status)
        //{
        //    StringBuilder where = new StringBuilder();
        //    where.Append("A.Delflag=0 and A.Status=4 and A.AccountId=" + accountId);
        //    if (status == "6")//已结束的课程需要判断时间
        //        where.Append(" and B.Delflag=0 and (B.Status in (" + status + ") or B.OpenClassTo<" + DateTime.Now.ToShortDateString() + ")");
        //    else
        //        where.Append(" and B.Delflag=0 and B.Status in (" + status + ")");
        //    where.Append(" and C.Delflag=0 and C.PartitionId=" + partitionId);
        //    where.Append(" and D.Delflag=0");
        //    where.Append(" and E.Delflag=0 and E.Delflag=0");
        //    return dal.GetMyLearnList(pageSize, pageIndex, where.ToString(), orderBy);
        //}

        #region 学习中的课程
        public int GetInDateCount(int accountId, int partitionId)
        {
            StringBuilder where = new StringBuilder();
            where.Append("A.Delflag=0 and A.Status=4 and (A.Result<>1 or Result is null) and A.AccountId=" + accountId);
            where.Append(" and B.Delflag=0 and B.Status=5");
            where.Append(" and C.Delflag=0 and C.PartitionId=" + partitionId);
            where.Append(" and D.Delflag=0");
            where.Append(" and E.Delflag=0 and E.Delflag=0");
            return dal.GetMyLearnCount(where.ToString());
        }

        public List<MyLearnInfo> GetInDateList(int pageSize, int pageIndex, string orderBy, int accountId, int partitionId)
        {
            StringBuilder where = new StringBuilder();
            where.Append("A.Delflag=0 and A.Status=4 and (A.Result<>1 or Result is null) and A.AccountId=" + accountId);
            where.Append(" and B.Delflag=0 and B.Status=5");
            where.Append(" and C.Delflag=0 and C.PartitionId=" + partitionId);
            where.Append(" and D.Delflag=0");
            where.Append(" and E.Delflag=0 and E.Delflag=0");
            return dal.GetMyLearnList(pageSize, pageIndex, where.ToString(), orderBy);
        }
        #endregion

        #region 未开始的课程
        public int GetBeforeDateCount(int accountId, int partitionId)
        {
            StringBuilder where = new StringBuilder();
            where.Append("A.Delflag=0 and A.Status=4 and (A.Result<>1 or Result is null) and A.AccountId=" + accountId);
            where.Append(" and B.Delflag=0 and B.Status=3");
            where.Append(" and C.Delflag=0 and C.PartitionId=" + partitionId);
            where.Append(" and D.Delflag=0");
            where.Append(" and E.Delflag=0 and E.Delflag=0");
            return dal.GetMyLearnCount(where.ToString());
        }

        public List<MyLearnInfo> GetBeforeDateList(int pageSize, int pageIndex, string orderBy, int accountId, int partitionId)
        {
            StringBuilder where = new StringBuilder();
            where.Append("A.Delflag=0 and A.Status=4 and (A.Result<>1 or Result is null) and A.AccountId=" + accountId);
            where.Append(" and B.Delflag=0 and B.Status=3");
            where.Append(" and C.Delflag=0 and C.PartitionId=" + partitionId);
            where.Append(" and D.Delflag=0");
            where.Append(" and E.Delflag=0 and E.Delflag=0");
            return dal.GetMyLearnList(pageSize, pageIndex, where.ToString(), orderBy);
        }
        #endregion

        #region 已结束的课程
        public int GetOutDateCount(int accountId, int partitionId)
        {
            StringBuilder where = new StringBuilder();
            where.Append("A.Delflag=0 and A.Status=4 and A.AccountId=" + accountId);
            where.Append(" and B.Delflag=0");
            where.Append(" and C.Delflag=0 and C.PartitionId=" + partitionId);
            where.Append(" and D.Delflag=0");
            where.Append(" and E.Delflag=0");
            where.Append(" and (A.Result=1 or B.Status=6)");
            return dal.GetMyLearnCount(where.ToString());
        }

        public List<MyLearnInfo> GetOutDateList(int pageSize, int pageIndex, string orderBy, int accountId, int partitionId)
        {
            StringBuilder where = new StringBuilder();
            where.Append("A.Delflag=0 and A.Status=4 and A.AccountId=" + accountId);
            where.Append(" and B.Delflag=0");
            where.Append(" and C.Delflag=0 and C.PartitionId=" + partitionId);
            where.Append(" and D.Delflag=0");
            where.Append(" and E.Delflag=0");
            where.Append(" and (A.Result=1 or B.Status=6)");
            return dal.GetMyLearnList(pageSize, pageIndex, where.ToString(), orderBy);
        }
        #endregion

        /// <summary>
        /// 取得外部课程信息
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public OutCourseInfo GetOutCourse(int trainingId)
        {
            StringBuilder where = new StringBuilder();
            where.Append("A.Id=" + trainingId);
            return dal.GetOutCourse(where.ToString());
        }
    }
}
