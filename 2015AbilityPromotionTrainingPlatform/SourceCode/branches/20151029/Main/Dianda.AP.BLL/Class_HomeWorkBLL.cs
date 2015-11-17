using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dianda.AP.BLL
{
    //Class_HomeWork
    public partial class Class_HomeWorkBLL
    {

        private DAL.Class_HomeWorkDAL dal = new DAL.Class_HomeWorkDAL();
        public Class_HomeWorkBLL()
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
        public int Add(Model.Class_HomeWork model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Class_HomeWork model)
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
        public Model.Class_HomeWork GetModel(int Id)
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
        public List<Model.Class_HomeWork> GetListModel(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<Model.Class_HomeWork> GetListModel(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(Top, strWhere, filedOrder);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Class_HomeWork> DataTableToList(DataTable dt)
        {
            List<Model.Class_HomeWork> modelList = new List<Model.Class_HomeWork>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Class_HomeWork model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.Class_HomeWork();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["CreateDate"].ToString() != "")
                    {
                        model.CreateDate = DateTime.Parse(dt.Rows[n]["CreateDate"].ToString());
                    }
                    if (dt.Rows[n]["HomeWorkId"].ToString() != "")
                    {
                        model.HomeWorkId = int.Parse(dt.Rows[n]["HomeWorkId"].ToString());
                    }
                    if (dt.Rows[n]["ClassId"].ToString() != "")
                    {
                        model.ClassId = int.Parse(dt.Rows[n]["ClassId"].ToString());
                    }
                    model.Content = dt.Rows[n]["Content"].ToString();
                    if (dt.Rows[n]["AccountId"].ToString() != "")
                    {
                        model.AccountId = int.Parse(dt.Rows[n]["AccountId"].ToString());
                    }
                    if (dt.Rows[n]["ParentReplyId"].ToString() != "")
                    {
                        model.ParentReplyId = int.Parse(dt.Rows[n]["ParentReplyId"].ToString());
                    }
                    model.AttList = dt.Rows[n]["AttList"].ToString();
                    if (dt.Rows[n]["Display"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Display"].ToString() == "1") || (dt.Rows[n]["Display"].ToString().ToLower() == "true"))
                        {
                            model.Display = true;
                        }
                        else
                        {
                            model.Display = false;
                        }
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


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        #endregion

    }
}