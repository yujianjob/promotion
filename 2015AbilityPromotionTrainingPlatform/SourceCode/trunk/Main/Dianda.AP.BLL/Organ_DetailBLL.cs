using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dianda.AP.BLL
{
    //Organ_Detail
    public partial class Organ_DetailBLL
    {

        private DAL.Organ_DetailDAL dal = new DAL.Organ_DetailDAL();
        public Organ_DetailBLL()
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
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Organ_Detail model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Organ_Detail model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {
            return dal.Delete(Id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Organ_Detail GetModel(int Id)
        {
            return dal.GetModel(Id);
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
        public List<Model.Organ_Detail> GetListModel(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<Model.Organ_Detail> GetListModel(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(Top, strWhere, filedOrder);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Organ_Detail> DataTableToList(DataTable dt)
        {
            List<Model.Organ_Detail> modelList = new List<Model.Organ_Detail>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Organ_Detail model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.Organ_Detail();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.Title = dt.Rows[n]["Title"].ToString();
                    if (dt.Rows[n]["ParentId"].ToString() != "")
                    {
                        model.ParentId = int.Parse(dt.Rows[n]["ParentId"].ToString());
                    }
                    if (dt.Rows[n]["OType"].ToString() != "")
                    {
                        model.OType = int.Parse(dt.Rows[n]["OType"].ToString());
                    }
                    model.Remark = dt.Rows[n]["Remark"].ToString();
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
                    if (dt.Rows[n]["PartitionId"].ToString() != "")
                    {
                        model.PartitionId = int.Parse(dt.Rows[n]["PartitionId"].ToString());
                    }


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 报名审核 开课机构的筛选
        /// </summary>
        public List<Model.Organ_Detail> GetOrganList(string strWhere)
        {
            DataSet ds = dal.GetOrganList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        #endregion

    }
}