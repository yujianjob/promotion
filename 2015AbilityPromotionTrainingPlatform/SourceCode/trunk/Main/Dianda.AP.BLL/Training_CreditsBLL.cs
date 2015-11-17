using Dianda.AP.Model;
using Dianda.AP.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dianda.AP.BLL
{
	public class Training_CreditsBLL
	{
		Training_CreditsDAL dal = new Training_CreditsDAL();

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public Training_Credits GetModel(int id, string where)
		{
			return dal.GetModel(id, where);
		}

		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Add(Training_Credits model)
		{
			return dal.Add(model) > 0;
		}

		/// <summary>
		/// 更新一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Update(Training_Credits model)
		{
			return dal.Update(model) > 0;
		}

		/// <summary>
		/// 获取数据集
		/// </summary>
		/// <param name="where"></param>
		/// <param name="orderBy"></param>
		/// <returns></returns>
		public DataTable GetList(string where, string orderBy)
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
		public List<Training_Credits> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			return dal.GetList(pageSize, pageIndex, where, orderBy, out recordCount);
		}

        public int GetAvgCredits(int Id,int PlanId)
        {
            DataTable dt = dal.GetAvgCredits(Id,PlanId);

            return Convert.ToInt32(dt.Rows[0]["MinValue"].ToString());
        }

        public int GetAvgId(int id,int PlanId)
        { 
            DataTable dt = dal.GetAvgCredits(id,PlanId);
            return Convert.ToInt32(dt.Rows[0]["Id"].ToString());
        }

        public int UpdCredits(string Value)
        {
            return dal.UpdCredits(Value);
        }

        public DataTable GetFieCount()
        {
            return dal.GetFieCount();
        }

        public int AddCredits(string Value, int OrganId, int PlanId)
        {
            return dal.AddCredits(Value, OrganId, PlanId);
        }

        public DataTable GetOrgan(int id,int PlanId)
        {
            return dal.GetOrgan(id,PlanId);
        }

        public bool YzAddCredits(int OrganId, int PlanId)
        {
            return dal.YzAddCredits(OrganId, PlanId);
        }
	}
}
