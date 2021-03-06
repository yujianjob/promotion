using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dianda.AP.Model;
using Dianda.AP.DAL;
using Dianda.AP.Model.Course.CourseCreate;

namespace Dianda.AP.BLL
{
	public class Course_UnitDetailBLL
	{
		Course_UnitDetailDAL dal = new Course_UnitDetailDAL();

		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Add(Course_UnitDetail model)
        {
            //新增默认信息
            model.CreateDate = DateTime.Now;
            model.Delflag = false;
            
            return dal.Add(model) > 0;
		}

		/// <summary>
		/// 更新一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Update(Course_UnitDetail model)
		{
            //更改默认信息
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
		public Course_UnitDetail GetModel(int id, string where)
		{
			return dal.GetModel(id, where);
		}

		/// <summary>
		/// 获取数据集
		/// </summary>
		/// <param name="where"></param>
		/// <param name="orderBy"></param>
		/// <returns></returns>
		public List<Course_UnitDetail> GetList(string where, string orderBy)
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
		public List<Course_UnitDetail> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			return dal.GetList(pageSize, pageIndex, where, orderBy, out recordCount);
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
        /// 获取指定ID的章节信息(课程详细)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Course_ChapterSectionInfo> GetChapterSectionInfo(int TrainingId, int id)
        {
            return dal.GetChapterSectionInfo(TrainingId,id);
        }

        /// <summary>
        /// 获取指定ID的章节信息(课程预览和学习)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Course_ChapterSectionInfo> GetLearnChapterSectionInfo(int TrainingId, int id)
        {
            return dal.GetLearnChapterSectionInfo(TrainingId, id);
        }

        /// <summary>
        /// 获取指定ID的的最大顺序号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetChapterSectionMaxOrder(int id,int TrainingId)
        {
            return dal.GetChapterSectionMaxOrder(id, TrainingId);
        }

        /// <summary>
        /// 获取指定章节的总时长(课程详细)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetTotalTimeLength(int id)
        {
            return dal.GetTotalTimeLength(id);
        }

        /// <summary>
        /// 获取指定章节的总时长(课程预览和学习)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetLearnTotalTimeLength(int id)
        {
            return dal.GetLearnTotalTimeLength(id);
        }
	}
}
