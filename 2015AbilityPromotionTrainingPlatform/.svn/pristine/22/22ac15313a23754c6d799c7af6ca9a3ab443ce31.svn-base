using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dianda.AP.BLL
{
    //Class_GroupMember
    public partial class Class_GroupMemberBLL
    {

        private DAL.Class_GroupMemberDAL dal = new DAL.Class_GroupMemberDAL();
        public Class_GroupMemberBLL()
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
        public bool Exists(int GroupId, int AccountId)
        {
            return dal.Exists(GroupId, AccountId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Class_GroupMember model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Class_GroupMember model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int GroupId, int AccountId)
        {
            return dal.Delete(GroupId, AccountId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Class_GroupMember GetModel(int GroupId, int AccountId)
        {
            return dal.GetModel(GroupId, AccountId);
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
        public List<Model.Class_GroupMember> GetListModel(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<Model.Class_GroupMember> GetListModel(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(Top, strWhere, filedOrder);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Class_GroupMember> DataTableToList(DataTable dt)
        {
            List<Model.Class_GroupMember> modelList = new List<Model.Class_GroupMember>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Class_GroupMember model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.Class_GroupMember();
                    if (dt.Rows[n]["GroupId"].ToString() != "")
                    {
                        model.GroupId = int.Parse(dt.Rows[n]["GroupId"].ToString());
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


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        #endregion

    }
}