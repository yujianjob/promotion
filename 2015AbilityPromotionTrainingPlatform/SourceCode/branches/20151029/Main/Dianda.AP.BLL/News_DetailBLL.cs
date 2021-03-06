using System;
using System.Collections.Generic;
using Dianda.AP.Model;
using Dianda.AP.DAL;
using System.Web.Mvc;

namespace Dianda.AP.BLL
{
	public class News_DetailBLL
	{
		News_DetailDAL dal = new News_DetailDAL();

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public News_Detail GetModel(int id, string where)
		{
			return dal.GetModel(id, where);
		}

		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Add(News_Detail model)
		{
			return dal.Add(model) > 0;
		}

		/// <summary>
		/// 更新一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Update(News_Detail model)
		{
			return dal.Update(model) > 0;
		}

		/// <summary>
		/// 获取数据集
		/// </summary>
		/// <param name="where"></param>
		/// <param name="orderBy"></param>
		/// <returns></returns>
		public List<News_Detail> GetList(string where, string orderBy)
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
		public List<News_Detail> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			return dal.GetList(pageSize, pageIndex, where, orderBy, out recordCount);
		}
        public List<SelectListItem> GetPlan()
        {
            return dal.GetPlan();
        }

        public bool NewsDel(int id)
        {
            return dal.NewsDel(id);
        }

        public bool YzTitle(string Title,int id)
        {
            return dal.YzTitle(Title,id);
        }
	}
}
