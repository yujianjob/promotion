using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dianda.AP.Model;
using Dianda.AP.DAL;

namespace Dianda.AP.BLL
{
    public class Course_UnitReplyDetailBLL
    {
        Course_UnitReplyDetailDAL dal = new Course_UnitReplyDetailDAL();

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Course_UnitReplyDetail model)
        {
            return dal.Add(model) > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Course_UnitReplyDetail model)
        {
            return dal.Update(model) > 0;
        }

        /// <summary>
        /// 删除话题,下面的回复修改Delflag = 1
        /// </summary>
        /// <param name="iId"></param>
        /// <returns></returns>
        public bool Update(int iId)
        {
            return dal.Update(iId) > 0;
        }

        /// <summary>
        /// 取得一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Course_UnitReplyDetail GetModel(int id, string where)
        {
            return dal.GetModel(id, where);
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Course_UnitReplyDetail> GetList(string where, string orderBy)
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
        public List<Course_UnitReplyDetail> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, where, orderBy, out recordCount);
        }

        /// <summary>
        /// 获取[讨论主题]数据集总数
        /// </summary>
        /// <param name="iClassId"></param>
        /// <param name="iUnitContent"></param>
        /// <returns></returns>
        public int GetListTopicTotalCount(int iClassId, int iUnitContent)
        {
            return dal.GetListTopicTotalCount(iClassId, iUnitContent);
        }

        /// <summary>
        /// 获取[讨论主题]分页数据集
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="iClassId"></param>
        /// <param name="iUnitContent"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<Course_UnitReplyDetailOther> GetListTopicOther(int pageSize, int pageIndex, int iClassId, int iUnitContent, out int recordCount)
        {
            return dal.GetListTopicOther(pageSize, pageIndex, iClassId, iUnitContent, out  recordCount);
        }

        /// <summary>
        /// 获取[讨论主题]下所有回复
        /// </summary>
        /// <param name="iClassId"></param>
        /// <param name="iUnitContent"></param>
        /// <param name="iParentReplyId"></param>
        /// <returns></returns>
        public List<Course_UnitReplyDetailOther> GetListReplyOther(int iClassId, int iUnitContent, int iParentReplyId)
        {
            return dal.GetListReplyOther(iClassId, iUnitContent, iParentReplyId);
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

    }
}
