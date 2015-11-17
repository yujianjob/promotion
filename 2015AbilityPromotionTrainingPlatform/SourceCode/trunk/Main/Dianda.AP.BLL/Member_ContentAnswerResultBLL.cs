using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dianda.AP.BLL
{
    //Member_ContentAnswerResult
    public partial class Member_ContentAnswerResultBLL
    {

        private DAL.Member_ContentAnswerResultDAL dal = new DAL.Member_ContentAnswerResultDAL();
        public Member_ContentAnswerResultBLL()
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
        public int Add(Model.Member_ContentAnswerResult model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Member_ContentAnswerResult model)
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
        public Model.Member_ContentAnswerResult GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

        /// <summary>
        /// 取得一条记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public Model.Member_ContentAnswerResult GetModel(string where)
        {
            return dal.GetModel(where);
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
        public List<Model.Member_ContentAnswerResult> GetListModel(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<Model.Member_ContentAnswerResult> GetListModel(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(Top, strWhere, filedOrder);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Member_ContentAnswerResult> DataTableToList(DataTable dt)
        {
            List<Model.Member_ContentAnswerResult> modelList = new List<Model.Member_ContentAnswerResult>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Member_ContentAnswerResult model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.Member_ContentAnswerResult();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["AccountId"].ToString() != "")
                    {
                        model.AccountId = int.Parse(dt.Rows[n]["AccountId"].ToString());
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
                    model.Verson = dt.Rows[n]["Verson"].ToString();
                    if (dt.Rows[n]["UnitContent"].ToString() != "")
                    {
                        model.UnitContent = int.Parse(dt.Rows[n]["UnitContent"].ToString());
                    }
                    if (dt.Rows[n]["ClassId"].ToString() != "")
                    {
                        model.ClassId = int.Parse(dt.Rows[n]["ClassId"].ToString());
                    }
                    if (dt.Rows[n]["Score"].ToString() != "")
                    {
                        model.Score = decimal.Parse(dt.Rows[n]["Score"].ToString());
                    }
                    if (dt.Rows[n]["QuestionCnt"].ToString() != "")
                    {
                        model.QuestionCnt = int.Parse(dt.Rows[n]["QuestionCnt"].ToString());
                    }
                    if (dt.Rows[n]["RightAnswer"].ToString() != "")
                    {
                        model.RightAnswer = int.Parse(dt.Rows[n]["RightAnswer"].ToString());
                    }
                    if (dt.Rows[n]["WrongAnswer"].ToString() != "")
                    {
                        model.WrongAnswer = int.Parse(dt.Rows[n]["WrongAnswer"].ToString());
                    }
                    if (dt.Rows[n]["Result"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Result"].ToString() == "1") || (dt.Rows[n]["Result"].ToString().ToLower() == "true"))
                        {
                            model.Result = true;
                        }
                        else
                        {
                            model.Result = false;
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