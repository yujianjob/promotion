using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dianda.AP.Model;
using Dianda.AP.DAL;
using Dianda.AP.Model.Course.CourseCreate;

namespace Dianda.AP.BLL
{
	public class Course_DetailBLL
	{
		Course_DetailDAL dal = new Course_DetailDAL();

		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Add(Course_Detail model)
		{
			return dal.Add(model) > 0;
		}

		/// <summary>
		/// 更新一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Update(Course_Detail model)
		{
			return dal.Update(model) > 0;
		}

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public Course_Detail GetModel(int id, string where)
		{
			return dal.GetModel(id, where);
		}

		/// <summary>
		/// 获取数据集
		/// </summary>
		/// <param name="where"></param>
		/// <param name="orderBy"></param>
		/// <returns></returns>
		public List<Course_Detail> GetList(string where, string orderBy)
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
		public List<Course_Detail> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
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
        /// 取得学科分类信息
        /// </summary>
        /// <returns></returns>
        public List<Course_SubjectInfo> GetSubjectInfoList()
        {
            return new Course_DetailDAL().GetSubjectInfoList();
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
        /// 取得课程总数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetTrainingInfoCount(string where)
        {
            return new Course_DetailDAL().GetTrainingInfoCount(where);
        }

        /// <summary>
        /// 取得课程列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Course_CourseInfo> GetTrainingInfoList(int pageSize, int pageIndex, string where, string orderBy)
        {
            return new Course_DetailDAL().GetTrainingInfoList(pageSize, pageIndex, where, orderBy);
        }

        /// <summary>
        /// 判断指定课程的是否存在
        /// </summary>
        /// <param name="Id"></param>
        public bool IsExistsCourseInfo(int Id)
        {
            return new Course_DetailDAL().IsExistsCourseInfo(Id);
        }

        /// <summary>
        /// 取得指定课程的课程信息
        /// </summary>
        /// <param name="Id"></param>
        public Course_CourseInfo GetTrainingInfoById(int Id)
        {
            return new Course_DetailDAL().GetTrainingInfoById(Id);
        }

        /// <summary>
        /// 取得一条考核比例记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Course_TestRate GetTestRateModel(int id, string where)
        {
            return dal.GetTestRateModel(id, where);
        }

        /// <summary>
        /// 更新一条考核比例记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateTestRate(Course_TestRate model)
        {
            return dal.UpdateTestRate(model) > 0;
        }
	}
}
