using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dianda.AP.Model;
using Dianda.AP.DAL;
using Dianda.AP.Model.Course.CourseCreate;

namespace Dianda.AP.BLL
{
	public class Course_UnitQuestionBLL
	{
		Course_UnitQuestionDAL dal = new Course_UnitQuestionDAL();

		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Add(Course_UnitQuestion model)
		{
            model.Verson = "-1";

            model.Display = true;
            model.Delflag = false;
            model.CreateDate = DateTime.Now;

            return dal.Add(model) > 0;
		}

		/// <summary>
		/// 更新一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Update(Course_UnitQuestion model)
		{
			return dal.Update(model) > 0;
		}

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public Course_UnitQuestion GetModel(int id, string where)
		{
			return dal.GetModel(id, where);
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
		/// 获取数据集
		/// </summary>
		/// <param name="where"></param>
		/// <param name="orderBy"></param>
		/// <returns></returns>
		public List<Course_UnitQuestion> GetList(string where, string orderBy)
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
		public List<Course_UnitQuestion> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
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
        /// 判断指定单元活动是否已经添加试题
        /// </summary>
        /// <param name="Id"></param>
        public bool IsExistsQuizQues(int Id)
        {
            return new Course_UnitQuestionDAL().IsExistsQuizQues(Id);
        }

        /// <summary>
        /// 获取指定活动的单元活动考试试题信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Course_UnitQuestion> GetQuizQuesInfo(int id)
        {
            return dal.GetQuizQuesInfo(id);
        }

        /// <summary>
        /// 更新一条单元活动考试试题记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCourseQuestion(Course_Question model)
        {
            return dal.UpdateCourseQuestion(model) > 0;
        }

        /// <summary>
        /// 更新指定活动考试试题版本号
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public bool SetVerson(int UnitContent)
        {
            string Verson = DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2, '0')
                           + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Hour.ToString().PadLeft(2, '0')
                           + DateTime.Now.Minute.ToString().PadLeft(2, '0');

            return dal.SetVerson(UnitContent, Verson) > 0;
        }
	}
}
