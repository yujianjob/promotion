using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dianda.AP.DAL;
using Dianda.AP.Model.API;

namespace Dianda.AP.BLL
{
	public class Course_OutCourseRecordBLL
	{
		Course_OutCourseRecordDAL dal = new Course_OutCourseRecordDAL();

		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Add(Course_OutCourseRecord model)
		{
			return dal.Add(model) > 0;
		}

		/// <summary>
		/// 更新一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Update(Course_OutCourseRecord model)
		{
			return dal.Update(model) > 0;
		}

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public Course_OutCourseRecord GetModel(int id, string where)
		{
			return dal.GetModel(id, where);
		}

		/// <summary>
		/// 取得记录数
		/// </summary>
		/// <param name="where"></param>
		/// <returns></returns>
		public int GetCount(string where)
		{
			return dal.GetCount(where);
		}

		/// <summary>
		/// 获取数据集
		/// </summary>
		/// <param name="where"></param>
		/// <param name="orderBy"></param>
		/// <returns></returns>
		public List<Course_OutCourseRecord> GetList(string where, string orderBy)
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
		/// <returns></returns>
		public List<Course_OutCourseRecord> GetList(int pageSize, int pageIndex, string where, string orderBy)
		{
			return dal.GetList(pageSize, pageIndex, where, orderBy);
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
		/// <returns></returns>
		public DataTable GetTable(int pageSize, int pageIndex, string where, string orderBy)
		{
			return dal.GetTable(pageSize, pageIndex, where, orderBy);
		}

	}
}
