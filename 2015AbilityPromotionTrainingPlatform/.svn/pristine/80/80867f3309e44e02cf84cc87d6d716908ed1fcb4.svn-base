using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianda.AP.Model.Prepare.ClassAddressList;

namespace Dianda.AP.BLL
{
    //Member_BaseInfo
    public partial class Member_BaseInfoBLL
    {

        private DAL.Member_BaseInfoDAL dal = new DAL.Member_BaseInfoDAL();
        public Member_BaseInfoBLL()
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
        public int Add(Model.Member_BaseInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Member_BaseInfo model)
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
        public Model.Member_BaseInfo GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

        public Model.Member_BaseInfo GetModelByAccountId(int AccountId)
        {
            return dal.GetModelByAccountId(AccountId);
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
        public List<Model.Member_BaseInfo> GetListModel(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<Model.Member_BaseInfo> GetListModel(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(Top, strWhere, filedOrder);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Member_BaseInfo> DataTableToList(DataTable dt)
        {
            List<Model.Member_BaseInfo> modelList = new List<Model.Member_BaseInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Member_BaseInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.Member_BaseInfo();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["CredentialsType"].ToString() != "")
                    {
                        model.CredentialsType = int.Parse(dt.Rows[n]["CredentialsType"].ToString());
                    }
                    model.CredentialsNumber = dt.Rows[n]["CredentialsNumber"].ToString();
                    if (dt.Rows[n]["Sex"].ToString() != "")
                    {
                        model.Sex = int.Parse(dt.Rows[n]["Sex"].ToString());
                    }
                    if (dt.Rows[n]["Birthday"].ToString() != "")
                    {
                        model.Birthday = DateTime.Parse(dt.Rows[n]["Birthday"].ToString());
                    }
                    if (dt.Rows[n]["Nation"].ToString() != "")
                    {
                        model.Nation = int.Parse(dt.Rows[n]["Nation"].ToString());
                    }
                    if (dt.Rows[n]["PoliticalStatus"].ToString() != "")
                    {
                        model.PoliticalStatus = int.Parse(dt.Rows[n]["PoliticalStatus"].ToString());
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
                    if (dt.Rows[n]["RegionId"].ToString() != "")
                    {
                        model.RegionId = int.Parse(dt.Rows[n]["RegionId"].ToString());
                    }
                    model.StudySection = dt.Rows[n]["StudySection"].ToString();
                    if (dt.Rows[n]["AccountId"].ToString() != "")
                    {
                        model.AccountId = int.Parse(dt.Rows[n]["AccountId"].ToString());
                    }
                    if (dt.Rows[n]["Organid"].ToString() != "")
                    {
                        model.Organid = int.Parse(dt.Rows[n]["Organid"].ToString());
                    }
                    if (dt.Rows[n]["Job"].ToString() != "")
                    {
                        model.Job = int.Parse(dt.Rows[n]["Job"].ToString());
                    }
                    if (dt.Rows[n]["WorkRank"].ToString() != "")
                    {
                        model.WorkRank = int.Parse(dt.Rows[n]["WorkRank"].ToString());
                    }
                    if (dt.Rows[n]["TeachDate"].ToString() != "")
                    {
                        model.TeachDate = DateTime.Parse(dt.Rows[n]["TeachDate"].ToString());
                    }
                    model.TeachStudySection = dt.Rows[n]["TeachStudySection"].ToString();
                    model.TeachSubject = dt.Rows[n]["TeachSubject"].ToString();
                    model.TeachGrade = dt.Rows[n]["TeachGrade"].ToString();
                    if (dt.Rows[n]["TraningType"].ToString() != "")
                    {
                        model.TraningType = int.Parse(dt.Rows[n]["TraningType"].ToString());
                    }
                    if (dt.Rows[n]["TraningObject"].ToString() != "")
                    {
                        model.TraningObject = int.Parse(dt.Rows[n]["TraningObject"].ToString());
                    }
                    if (dt.Rows[n]["EduLevel"].ToString() != "")
                    {
                        model.EduLevel = int.Parse(dt.Rows[n]["EduLevel"].ToString());
                    }
                    model.TeacherNo = dt.Rows[n]["TeacherNo"].ToString();
                    if (dt.Rows[n]["EduDegree"].ToString() != "")
                    {
                        model.EduDegree = int.Parse(dt.Rows[n]["EduDegree"].ToString());
                    }
                    if (dt.Rows[n]["EduMajor"].ToString() != "")
                    {
                        model.EduMajor = int.Parse(dt.Rows[n]["EduMajor"].ToString());
                    }
                    model.EduMajorOhter = dt.Rows[n]["EduMajorOhter"].ToString();
                    model.GraduateSchool = dt.Rows[n]["GraduateSchool"].ToString();
                    if (dt.Rows[n]["GraduateTime"].ToString() != "")
                    {
                        model.GraduateTime = DateTime.Parse(dt.Rows[n]["GraduateTime"].ToString());
                    }
                    if (dt.Rows[n]["GroupId"].ToString() != "")
                    {
                        model.GroupId = int.Parse(dt.Rows[n]["GroupId"].ToString());
                    }
                    model.RealName = dt.Rows[n]["RealName"].ToString();
                    model.Mobile = dt.Rows[n]["Mobile"].ToString();
                    model.Phone = dt.Rows[n]["Phone"].ToString();
                    model.Address = dt.Rows[n]["Address"].ToString();
                    model.PostCode = dt.Rows[n]["PostCode"].ToString();


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获取指定班级的会员信息
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public List<AddressList> GetMemberInfo(int TrainingId, int ClassId)
        {
            return dal.GetMemberInfo(TrainingId,ClassId);
        }
        #endregion

    }
}