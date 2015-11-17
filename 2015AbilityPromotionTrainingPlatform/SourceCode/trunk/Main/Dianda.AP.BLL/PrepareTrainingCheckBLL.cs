using Dianda.AP.DAL;
using Dianda.AP.Model;
using Dianda.AP.Model.Prepare.TrainingCheck;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.BLL
{
    public class PrepareTrainingCheckBLL
    {
        /// <summary>
        /// 判断课程是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool CheckExists(string title, int organId, int id)
        {
            StringBuilder where = new StringBuilder();
            where.Append("Delflag=0 and OrganId=" + organId + " and Title='" + title + "'");
            if (id > 0)
                where.Append(" and Id<>" + id);
            return new PrepareTrainingCheckDAL().CheckExists(where.ToString());
        }

        /// <summary>
        /// 取得课程总数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetTrainingInfoCount(string where)
        {
            return new PrepareTrainingCheckDAL().GetTrainingInfoCount(where);
        }

        /// <summary>
        /// 取得课程列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<TrainingInfo> GetTrainingInfoList(int pageSize, int pageIndex, string where, string orderBy)
        {
            return new PrepareTrainingCheckDAL().GetTrainingInfoList(pageSize, pageIndex, where, orderBy);
        }

        /// <summary>
        /// 取得学科类型
        /// </summary>
        /// <returns></returns>
        public List<Traning_InfoFk> GetSubject()
        {
            return new Traning_InfoFkBLL().GetList("Delflag=0 and CategoryType=3", "");
        }

        /// <summary>
        /// 取得区县和培训机构
        /// </summary>
        /// <returns></returns>
        public List<Organ_Detail> GetArea()
        {
            return new Organ_DetailBLL().GetListModel("Delflag=0 and OType in (1,5)");
        }

        /// <summary>
        /// 取得学区
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public List<Organ_StudyRegion> GetRegion(int areaId)
        {
            return new Organ_StudyRegionBLL().GetList("Delflag=0 and ParentId=" + areaId, "");
        }

        /// <summary>
        /// 取得学校
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public List<Organ_Detail> GetSchool(int regionId)
        {
            return new Organ_DetailBLL().GetListModel("Delflag=0 and OType=2 and StudyRegionId=" + regionId);
        }

        /// <summary>
        /// 取得培训对象类型
        /// </summary>
        /// <returns></returns>
        public List<Traning_InfoFk> GetTraningObject()
        {
            return new Traning_InfoFkBLL().GetList("Delflag=0 and CategoryType=2", "");
        }

        /// <summary>
        /// 取得学段类型
        /// </summary>
        /// <returns></returns>
        public List<Traning_InfoFk> GetStudyLevel()
        {
            return new Traning_InfoFkBLL().GetList("Delflag=0 and CategoryType=4", "");
        }

        /// <summary>
        /// 取得讲师称谓类型
        /// </summary>
        /// <returns></returns>
        public List<Traning_InfoFk> GetTeacherTitle()
        {
            return new Traning_InfoFkBLL().GetList("Delflag=0 and CategoryType=6", "");
        }

        /// <summary>
        /// 取得培训形式类型
        /// </summary>
        /// <returns></returns>
        public List<Traning_InfoFk> GetTrainingForm()
        {
            return new Traning_InfoFkBLL().GetList("Delflag=0 and CategoryType=5", "");
        }

        /// <summary>
        /// 取得课程大类
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Traning_Field> GetTrainingField(string where, string orderBy)
        {
            return new Traning_FieldBLL().GetList(where, orderBy);
        }

        /// <summary>
        /// 取得课程小类
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Traning_Category> GetTrainingCategory(string where, string orderBy)
        {
            return new Traning_CategoryBLL().GetList(where, orderBy);
        }

        /// <summary>
        /// 取得课程主题
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Traning_Topic> GetTrainingTopic(string where, string orderBy)
        {
            return new Traning_TopicBLL().GetList(where, orderBy);
        }

        /// <summary>
        /// 取得附件DataTable
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public DataTable GetAttachTable(string where, string orderBy)
        {
            return new Traning_AttachmentBLL().GetAttachTable(where, orderBy);
        }

        /// <summary>
        /// 取得一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Traning_Detail GetTrainingDetail(int id, string where)
        {
            return new Traning_DetailDAL().GetModel(id, where);
        }

        /// <summary>
        /// 更新一条课程记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateTrainingDetail(Traning_Detail model)
        {
            return new Traning_DetailDAL().Update(model) > 0;
        }

        /// <summary>
        /// 批量更新附件
        /// </summary>
        /// <param name="dt"></param>
        public void BatchAttach(DataTable dt)
        {
            new PrepareTrainingCheckDAL().BatchAttach(dt);
        }

        /// <summary>
        /// 新增一条审核记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddTrainingApply(Traning_ApplyApplication model)
        {
            return new Traning_ApplyApplicationDAL().Add(model) > 0;
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="where">更新课程表的条件</param>
        /// <param name="status"></param>
        /// <param name="remark"></param>
        /// <param name="applies"></param>
        public void BatchTrainingApply(string where, int status, string remark, List<Traning_ApplyApplication> applies)
        {
            new PrepareTrainingCheckDAL().BatchTrainingApply(where, status, remark, applies);
        }

        /// <summary>
        /// 取得上级机构Id
        /// </summary>
        /// <param name="organId"></param>
        /// <returns></returns>
        public int GetParentOrganId(int organId)
        {
            Organ_Detail model = new Organ_DetailDAL().GetModel(organId);
            if (model != null && model.ParentId != null)
            {
                return Convert.ToInt32(model.ParentId);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 取得外部课程类型
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Traning_OutCourseType> GetOutCourseType(string where, string orderBy)
        {
            return new Traning_OutCourseTypeBLL().GetList(where, orderBy);
        }
    }
}
