using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dianda.AP.BLL
{
    //Member_PlanOverall
    public partial class Member_PlanOverallBLL
    {

        private DAL.Member_PlanOverallDAL dal = new DAL.Member_PlanOverallDAL();
        public Member_PlanOverallBLL()
        { }
        #region  Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PlanId, int AccountId)
        {
            return dal.Exists(PlanId, AccountId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Member_PlanOverall model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Member_PlanOverall model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int PlanId, int AccountId)
        {
            return dal.Delete(PlanId, AccountId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Member_PlanOverall GetModel(int PlanId, int AccountId)
        {
            return dal.GetModel(PlanId, AccountId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Member_PlanOverall> GetListModel(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<Model.Member_PlanOverall> GetListModel(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(Top, strWhere, filedOrder);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Member_PlanOverall> DataTableToList(DataTable dt)
        {
            List<Model.Member_PlanOverall> modelList = new List<Model.Member_PlanOverall>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Member_PlanOverall model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.Member_PlanOverall();
                    if (dt.Rows[n]["PlanId"].ToString() != "")
                    {
                        model.PlanId = int.Parse(dt.Rows[n]["PlanId"].ToString());
                    }
                    if (dt.Rows[n]["AccountId"].ToString() != "")
                    {
                        model.AccountId = int.Parse(dt.Rows[n]["AccountId"].ToString());
                    }
                    if (dt.Rows[n]["Result"].ToString() != "")
                    {
                        model.Result = int.Parse(dt.Rows[n]["Result"].ToString());
                    }
                    if (dt.Rows[n]["Delflag"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Delflag"].ToString() == "1") || (dt.Rows[n]["Delflag"].ToString().ToLower() == "true"))
                        {
                            model.Delflag = true;
                        }
                        else
                        {
                            model.Delflag = false;
                        }
                    }
                    if (dt.Rows[n]["CreateDate"].ToString() != "")
                    {
                        model.CreateDate = DateTime.Parse(dt.Rows[n]["CreateDate"].ToString());
                    }


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        #endregion

    }
}