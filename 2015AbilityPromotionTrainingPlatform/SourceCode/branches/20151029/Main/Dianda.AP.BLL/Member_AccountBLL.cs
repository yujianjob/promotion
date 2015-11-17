using Dianda.AP.DAL;
using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dianda.AP.BLL
{
	public partial class Member_AccountBLL
	{
		Member_AccountDAL dal = new Member_AccountDAL();

		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Add(Member_Account model)
		{
			return dal.Add(model) > 0;
		}

		/// <summary>
		/// 更新一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Update(Member_Account model)
		{
			return dal.Update(model) > 0;
		}

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
        public Member_Account GetModel(int id, string where)
        {
            return dal.GetModel(id, where);
        }

        /// <summary>
        /// 取得一条记录（视图）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Member_Account V_GetModel(int id, string where)
        {
            return dal.V_GetModel(id, where);
        }

		/// <summary>
		/// 获取数据集
		/// </summary>
		/// <param name="where"></param>
		/// <param name="orderBy"></param>
		/// <returns></returns>
		public List<Member_Account> GetList(string where, string orderBy)
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
		public List<Member_Account> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			return dal.GetList(pageSize, pageIndex, where, orderBy, out recordCount);
		}

        public List<Member_AccountBaseInfo> GetMABListPractice(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            return dal.GetMABListPractice(pageSize, pageIndex, where, orderBy, out recordCount);
        }

        //public List<Member_AccountBaseInfo> GetMABList(int pageSize, int pageIndex, string where,string where2,int ClassId,int TrainingId, string orderBy, out int recordCount)
        //{
        //    return dal.GetMABList(pageSize, pageIndex, where,where2, ClassId, TrainingId,orderBy, out recordCount);
        //}

        public List<Member_AccountBaseInfo> GetMarketMemberList(int pageSize, int pageIndex, int Type, Class_Detail ClassModel, int TrainingId, int OrganId, int Groupid, string orderBy, out int recordCount)
        {
            return dal.GetMarketMemberList(pageSize, pageIndex, Type, ClassModel, TrainingId, OrganId, Groupid, orderBy, out recordCount);
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