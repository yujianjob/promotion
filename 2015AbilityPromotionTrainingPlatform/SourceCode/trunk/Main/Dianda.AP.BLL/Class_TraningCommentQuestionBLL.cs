using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dianda.AP.BLL
{
    //Class_TraningCommentQuestion
    public partial class Class_TraningCommentQuestionBLL
    {

        private DAL.Class_TraningCommentQuestionDAL dal = new DAL.Class_TraningCommentQuestionDAL();
        public Class_TraningCommentQuestionBLL()
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
        public int Add(Model.Class_TraningCommentQuestion model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Class_TraningCommentQuestion model)
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
        public Model.Class_TraningCommentQuestion GetModel(int Id)
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
        public List<Model.Class_TraningCommentQuestion> GetListModel(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<Model.Class_TraningCommentQuestion> GetListModel(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(Top, strWhere, filedOrder);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Class_TraningCommentQuestion> DataTableToList(DataTable dt)
        {
            List<Model.Class_TraningCommentQuestion> modelList = new List<Model.Class_TraningCommentQuestion>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Class_TraningCommentQuestion model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.Class_TraningCommentQuestion();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.Verson = dt.Rows[n]["Verson"].ToString();
                    model.Content = dt.Rows[n]["Content"].ToString();
                    if (dt.Rows[n]["QTtype"].ToString() != "")
                    {
                        model.QTtype = int.Parse(dt.Rows[n]["QTtype"].ToString());
                    }
                    model.Question = dt.Rows[n]["Question"].ToString();
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