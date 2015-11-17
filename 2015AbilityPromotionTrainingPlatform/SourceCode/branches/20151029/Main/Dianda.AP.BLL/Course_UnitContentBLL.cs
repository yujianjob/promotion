using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dianda.AP.Model;
using Dianda.AP.DAL;
using Dianda.AP.Model.Course.CourseCreate;

namespace Dianda.AP.BLL
{
	public partial class Course_UnitContentBLL
	{
		Course_UnitContentDAL dal = new Course_UnitContentDAL();

		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(Course_UnitContent model)
		{
            //新增默认信息
            model.Display = true;
            model.CreateDate = DateTime.Now;
            model.Delflag = false;
            if (model.UnitType == 5 || model.UnitType == 6)
            {
                model.TimeLimit = model.TimeLength;
            }
            else
            {
                model.TimeLimit = 0;
            }
            model.PassLine = 0;

            return dal.Add(model);
		}

		/// <summary>
		/// 更新一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Update(Course_UnitContent model)
		{
            //新增默认信息
            if (model.UnitType == 5 || model.UnitType == 6)
            {
                model.TimeLimit = model.TimeLength;
            }
            else
            {
                model.TimeLimit = 0;
            }
            model.CreateDate = DateTime.Now;
            model.Delflag = false;

			return dal.Update(model) > 0;
		}

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return dal.Delete(id) > 0;
        }

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public Course_UnitContent GetModel(int id, string where)
		{
			return dal.GetModel(id, where);
		}

		/// <summary>
		/// 获取数据集
		/// </summary>
		/// <param name="where"></param>
		/// <param name="orderBy"></param>
		/// <returns></returns>
		public List<Course_UnitContent> GetList(string where, string orderBy)
		{
			return dal.GetList(where, orderBy);
		}

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Course_UnitContent> GetListOther(string where, string orderBy)
        {
            return dal.GetListOther(where, orderBy);
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
		public List<Course_UnitContent> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			return dal.GetList(pageSize, pageIndex, where, orderBy, out recordCount);
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
        public List<Course_UnitContent> GetListOther(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            return dal.GetListOther(pageSize, pageIndex, where, orderBy, out recordCount);
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

        /// <summary>
        /// 获取指定章节指定内容类型学习活动最大顺序号
        /// </summary>
        /// <param name="UnitId"></param>
        /// <param name="UnitType"></param>
        /// <returns></returns>
        public int GetUnitContentMaxOrder(int UnitId, int UnitType)
        {
            return dal.GetUnitContentMaxOrder(UnitId, UnitType);
        }

        /// <summary>
        /// 获取指定章节的活动信息(课程详细)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Course_ActivityInfo> GetActivityInfo(int id)
        {
            return dal.GetActivityInfo(id);
        }

        /// <summary>
        /// 获取指定章节的活动信息(课程预览和学习)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Course_ActivityInfo> GetLearnActivityInfo(int id)
        {
            return dal.GetLearnActivityInfo(id);
        }

        /// <summary>
        /// 获取指定章节的学习活动信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ClassId"></param>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public List<Course_ActivityLearn> GetActivityLearn(int id,int ClassId,int AccountId)
        {
            return dal.GetActivityLearn(id, ClassId,AccountId);
        }

        /// <summary>
        /// 获取指定课程ID的结业考试(课程详细)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Course_ActivityInfo> GetExamInfo(int id)
        {
            return dal.GetExamInfo(id);
        }

        /// <summary>
        /// 获取指定课程ID的结业考试(课程预览和学习)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Course_ActivityInfo> GetExamLearnInfo(int id)
        {
            return dal.GetExamLearnInfo(id);
        }

        /// <summary>
        /// 获取指定课程ID的学习结业考试
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ClassId"></param>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public List<Course_ActivityLearn> GetLearnExamInfo(int id, int ClassId, int AccountId)
        {
            return dal.GetLearnExamInfo(id, ClassId, AccountId);
        }

        /// <summary>
        /// 更新结业考试可测试次数(-1)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateTestCnt(int id)
        {
            return dal.UpdateTestCnt(id);
        }

        /// <summary>
        /// 是否学完所有课程
        /// </summary>
        /// <param name="TrainingId"></param>
        /// <param name="ClassId"></param>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public bool IsAllLearn(int TrainingId, int ClassId, int AccountId)
        {
            return dal.IsAllLearn(TrainingId, ClassId, AccountId);
        }

        /// <summary>
        /// 判断同一课程下新增章名称是否重复
        /// </summary>
        /// <param name="TrainingId"></param>
        /// <param name="tilte"></param>
        /// <returns></returns>
        public bool IsChapterAddRename(int TrainingId, string tilte)
        {
            return dal.IsChapterAddRename(TrainingId, tilte);
        }

        /// <summary>
        /// 判断同一课程下编辑章名称是否重复
        /// </summary>
        /// <param name="TrainingId"></param>
        /// <param name="tilte"></param>
        /// <returns></returns>
        public bool IsChapterEditRename(int TrainingId, string tilte, int CourseId)
        {
            return dal.IsChapterEditRename(TrainingId, tilte, CourseId);
        }

        /// <summary>
        /// 判断同一章下新增节名称是否重复
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="tilte"></param>
        /// <returns></returns>
        public bool IsSectionAddRename(int ParentId, string tilte)
        {
            return dal.IsSectionAddRename(ParentId, tilte);
        }

        /// <summary>
        /// 判断同一章下编辑节名称是否重复
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="tilte"></param>
        /// <returns></returns>
        public bool IsSectionEditRename(int ParentId, string tilte, int CourseId)
        {
            return dal.IsSectionEditRename(ParentId, tilte, CourseId);
        }

        /// <summary>
        /// 判断同一章节下同一类型活动名称是否重复
        /// </summary>
        /// <param name="UnitId"></param>
        /// <param name="UnitType"></param>
        /// <param name="tilte"></param>
        /// <returns></returns>
        public bool IsActivityAddRename(int UnitId, int UnitType, string tilte)
        {
            return dal.IsActivityAddRename(UnitId, UnitType, tilte);
        }

        /// <summary>
        /// 判断同一章节下同一类型编辑活动名称是否重复
        /// </summary>
        /// <param name="UnitId"></param>
        /// <param name="UnitType"></param>
        /// <param name="tilte"></param>
        /// <returns></returns>
        public bool IsActivityEditRename(int UnitId, int UnitType, string tilte, int UnitContent)
        {
            return dal.IsActivityEditRename(UnitId, UnitType,tilte,UnitContent);
        }
	}
}
