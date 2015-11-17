using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dianda.AP.Model;
using Dianda.AP.DAL;

namespace Dianda.AP.BLL
{
	public class accountBLL
	{
		accountDAL dal = new accountDAL();

		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Add(account model)
		{
			return dal.Add(model) > 0;
		}

		/// <summary>
		/// 更新一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Update(account model)
		{
			return dal.Update(model) > 0;
		}

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public account GetModel(int id, string where)
		{
			return dal.GetModel(id, where);
		}

		/// <summary>
		/// 获取数据集
		/// </summary>
		/// <param name="where"></param>
		/// <param name="orderBy"></param>
		/// <returns></returns>
		public List<account> GetList(string where, string orderBy)
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
		public List<account> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			return dal.GetList(pageSize, pageIndex, where, orderBy, out recordCount);
		}

        /// <summary>
        /// 获取数据集总记录数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetListOtherCount(string strWhere)
        {
            return dal.GetListOtherCount(strWhere);
        }

        /// <summary>
        /// 获取分页数据集
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<accountOther> GetListOther(int pageSize, int pageIndex, string where, out int recordCount)
        {
            return dal.GetListOther(pageSize, pageIndex, where, out recordCount);
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

        #region 前测统计
        /// <summary>
        /// 取得统计数据
        /// </summary>
        /// <param name="organId">0：市级；其它：区级和培训机构</param>
        /// <returns></returns>
        public DataSet Statistics(int organId = 0)
        {
            if (organId == 0)
            {
                return dal.Statistics();
            }
            else
            {
                return dal.Statistics(organId);
            }
        }

        /// <summary>
        /// 校级统计数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet StatisticsSub(int id)
        {
            return dal.StatisticsSub(id);
        }

        /// <summary>
        /// 区县和培训机构
        /// </summary>
        /// <returns></returns>
        public List<Organ_Detail> GetArea()
        {
            return new Organ_DetailBLL().GetListModel("Delflag=0 and OType in (1,5)");
        }

        /// <summary>
        /// 学区
        /// </summary>
        /// <param name="areaId">区县、培训机构Id</param>
        /// <returns></returns>
        public List<Organ_StudyRegion> GetRegion(int areaId)
        {
            return new Organ_StudyRegionBLL().GetList("Delflag=0 and ParentId=" + areaId, "");
        }

        /// <summary>
        /// 学校
        /// </summary>
        /// <param name="regionId">学区Id</param>
        /// <returns></returns>
        public List<Organ_Detail> GetSchool(int regionId)
        {
            return new Organ_DetailBLL().GetListModel("Delflag=0 and OType=2 and StudyRegionId=" + regionId);
        }

        /// <summary>
        /// 图形统计数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataSet ChartData(string where)
        {
            return dal.ChartData(where);
        }
        #endregion
    }
}
