using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dianda.AP.Model;
using Dianda.AP.DAL;

namespace Dianda.AP.BLL
{
    public class Class_GroupBLL
    {
        Class_GroupDAL dal = new Class_GroupDAL();

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Class_Group model)
        {
            return dal.Add(model) > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Class_Group model)
        {
            return dal.Update(model) > 0;
        }

        /// <summary>
        /// 取得一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Class_Group GetModel(int id, string where)
        {
            return dal.GetModel(id, where);
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Class_Group> GetList(string where, string orderBy)
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
        public List<Class_Group> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, where, orderBy, out recordCount);
        }

        /// <summary>
        /// 根据用户获取其所在的班级和组
        /// </summary>
        /// <param name="iAccountId"></param>
        /// <returns></returns>
        public Class_Group GetClassAndGroup(int iAccountId)
        {
            return dal.GetClassAndGroup(iAccountId);
        }

        /// <summary>
        /// 获取 [小组成员-讲师,辅导员]
        /// </summary>
        /// <param name="iAccountId"></param>
        /// <param name="iClassId"></param>
        /// <returns></returns>
        public List<Class_GroupAll> GetGroupList(int iAccountId, int iClassId)
        {
            return dal.GetGroupList(iAccountId, iClassId);
        }

        /// <summary>
        /// 获取[小组成员-组员]总数
        /// </summary>
        /// <param name="iClassId"></param>
        /// <returns></returns>
        public int GetGroupList_MemberCount(int iClassId, int iAccountId)
        {
            return dal.GetGroupList_MemberCount(iClassId, iAccountId);
        }

        /// <summary>
        /// 获取分页数据集[小组成员-组员]
        /// [获取与当前登录用户同小组的成员信息]
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="iClassId"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<Class_GroupAll> GetGroupList_Member(int pageSize, int pageIndex, int iClassId, int iAccountId, out int recordCount)
        {
            return dal.GetGroupList_Member(pageSize, pageIndex, iClassId, iAccountId, out recordCount);
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
