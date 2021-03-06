﻿using Dianda.AP.Model;
using Dianda.AP.Model.Learn.MyLearn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.DAL
{
    public class LearnMyLearnDAL
    {
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Class_Detail model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into [dbo].[Class_Detail] ([Title],[TraningId],[PlanId],[SignUpStartTime],[SignUpEndTime],[OpenClassFrom],[OpenClassTo],[ClassForm],[People],[LimitPeopleCnt],[Address],[Content],[ManagerId],[OrganId],[Subject],[StudyLevel],[TeachGrade],[TeachRank],[OrganRange],[Instructor],[Status],[ApplyRemark],[Display],[Delflag],[CreateDate],[PartitionId])");
            sql.Append(" values (@Title,@TraningId,@PlanId,@SignUpStartTime,@SignUpEndTime,@OpenClassFrom,@OpenClassTo,@ClassForm,@People,@LimitPeopleCnt,@Address,@Content,@ManagerId,@OrganId,@Subject,@StudyLevel,@TeachGrade,@TeachRank,@OrganRange,@Instructor,@Status,@ApplyRemark,@Display,@Delflag,@CreateDate,@PartitionId)");
            sql.Append(" set @Id=@@IDENTITY");
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
				new SqlParameter("@TraningId", SqlDbType.Int, 4) { Value = model.TraningId },
				new SqlParameter("@PlanId", SqlDbType.Int, 4) { Value = model.PlanId },
				new SqlParameter("@SignUpStartTime", SqlDbType.DateTime, 8) { Value = model.SignUpStartTime },
				new SqlParameter("@SignUpEndTime", SqlDbType.DateTime, 8) { Value = model.SignUpEndTime },
				new SqlParameter("@OpenClassFrom", SqlDbType.DateTime, 8) { Value = model.OpenClassFrom },
				new SqlParameter("@OpenClassTo", SqlDbType.DateTime, 8) { Value = model.OpenClassTo },
				new SqlParameter("@ClassForm", SqlDbType.Int, 4) { Value = model.ClassForm },
				new SqlParameter("@People", SqlDbType.Int, 4) { Value = model.People },
				new SqlParameter("@LimitPeopleCnt", SqlDbType.Int, 4) { Value = model.LimitPeopleCnt },
				new SqlParameter("@Address", SqlDbType.VarChar, 200) { Value = model.Address },
				new SqlParameter("@Content", SqlDbType.VarChar, 500) { Value = model.Content },
				new SqlParameter("@ManagerId", SqlDbType.Int, 4) { Value = model.ManagerId },
				new SqlParameter("@OrganId", SqlDbType.Int, 4) { Value = model.OrganId },
				new SqlParameter("@Subject", SqlDbType.VarChar, 200) { Value = model.Subject },
				new SqlParameter("@StudyLevel", SqlDbType.VarChar, 200) { Value = model.StudyLevel },
				new SqlParameter("@TeachGrade", SqlDbType.VarChar, 200) { Value = model.TeachGrade },
				new SqlParameter("@TeachRank", SqlDbType.VarChar, 200) { Value = model.TeachRank },
				new SqlParameter("@OrganRange", SqlDbType.VarChar, 500) { Value = model.OrganRange },
				new SqlParameter("@Instructor", SqlDbType.Int, 4) { Value = model.Instructor },
				new SqlParameter("@Status", SqlDbType.Int, 4) { Value = model.Status },
				new SqlParameter("@ApplyRemark", SqlDbType.VarChar, 500) { Value = model.ApplyRemark },
				new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = model.Display },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@PartitionId", SqlDbType.Int, 4) { Value = model.PartitionId }
			};
            int result = Convert.ToInt32(MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams));
            model.Id = Convert.ToInt32(cmdParams[0].Value);
            return result;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Class_Detail model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Class_Detail] set ");
            sql.Append("[Title]=@Title,[TraningId]=@TraningId,[PlanId]=@PlanId,[SignUpStartTime]=@SignUpStartTime,[SignUpEndTime]=@SignUpEndTime,[OpenClassFrom]=@OpenClassFrom,[OpenClassTo]=@OpenClassTo,[ClassForm]=@ClassForm,[People]=@People,[LimitPeopleCnt]=@LimitPeopleCnt,[Address]=@Address,[Content]=@Content,[ManagerId]=@ManagerId,[OrganId]=@OrganId,[Subject]=@Subject,[StudyLevel]=@StudyLevel,[TeachGrade]=@TeachGrade,[TeachRank]=@TeachRank,[OrganRange]=@OrganRange,[Instructor]=@Instructor,[Status]=@Status,[ApplyRemark]=@ApplyRemark,[Display]=@Display,[Delflag]=@Delflag,[CreateDate]=@CreateDate,[PartitionId]=@PartitionId");
            sql.Append(" where [Id]=@Id");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
				new SqlParameter("@TraningId", SqlDbType.Int, 4) { Value = model.TraningId },
				new SqlParameter("@PlanId", SqlDbType.Int, 4) { Value = model.PlanId },
				new SqlParameter("@SignUpStartTime", SqlDbType.DateTime, 8) { Value = model.SignUpStartTime },
				new SqlParameter("@SignUpEndTime", SqlDbType.DateTime, 8) { Value = model.SignUpEndTime },
				new SqlParameter("@OpenClassFrom", SqlDbType.DateTime, 8) { Value = model.OpenClassFrom },
				new SqlParameter("@OpenClassTo", SqlDbType.DateTime, 8) { Value = model.OpenClassTo },
				new SqlParameter("@ClassForm", SqlDbType.Int, 4) { Value = model.ClassForm },
				new SqlParameter("@People", SqlDbType.Int, 4) { Value = model.People },
				new SqlParameter("@LimitPeopleCnt", SqlDbType.Int, 4) { Value = model.LimitPeopleCnt },
				new SqlParameter("@Address", SqlDbType.VarChar, 200) { Value = model.Address },
				new SqlParameter("@Content", SqlDbType.VarChar, 500) { Value = model.Content },
				new SqlParameter("@ManagerId", SqlDbType.Int, 4) { Value = model.ManagerId },
				new SqlParameter("@OrganId", SqlDbType.Int, 4) { Value = model.OrganId },
				new SqlParameter("@Subject", SqlDbType.VarChar, 200) { Value = model.Subject },
				new SqlParameter("@StudyLevel", SqlDbType.VarChar, 200) { Value = model.StudyLevel },
				new SqlParameter("@TeachGrade", SqlDbType.VarChar, 200) { Value = model.TeachGrade },
				new SqlParameter("@TeachRank", SqlDbType.VarChar, 200) { Value = model.TeachRank },
				new SqlParameter("@OrganRange", SqlDbType.VarChar, 500) { Value = model.OrganRange },
				new SqlParameter("@Instructor", SqlDbType.Int, 4) { Value = model.Instructor },
				new SqlParameter("@Status", SqlDbType.Int, 4) { Value = model.Status },
				new SqlParameter("@ApplyRemark", SqlDbType.VarChar, 500) { Value = model.ApplyRemark },
				new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = model.Display },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@PartitionId", SqlDbType.Int, 4) { Value = model.PartitionId }
			};
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
        }

        /// <summary>
        /// 取得一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Class_Detail GetModel(int id, string where)
        {
            string sql = "select * from [dbo].[Class_Detail] where [Id]=@Id";
            if (!string.IsNullOrEmpty(where))
                sql += " and " + where;
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                if (reader.Read())
                {
                    Class_Detail model = new Class_Detail();
                    ConvertToModel(reader, model);
                    return model;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Class_Detail> GetList(string where, string orderBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from [dbo].[Class_Detail]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            if (!string.IsNullOrEmpty(orderBy))
                sql.Append(" order by " + orderBy);
            List<Class_Detail> list = new List<Class_Detail>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    Class_Detail model = new Class_Detail();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
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
        public List<Class_Detail> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(1) from [dbo].[Class_Detail]");
            if (!string.IsNullOrEmpty(where))
                sb.Append(" where " + where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Class_Detail]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(") as T where [RowNum] between " + start + " and " + end);
            List<Class_Detail> list = new List<Class_Detail>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    Class_Detail model = new Class_Detail();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public DataTable GetTable(string where, string orderBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select [Id],[Title],[TraningId],[PlanId],[SignUpStartTime],[SignUpEndTime],[OpenClassFrom],[OpenClassTo],[ClassForm],[People],[LimitPeopleCnt],[Address],[Content],[ManagerId],[OrganId],[Subject],[StudyLevel],[TeachGrade],[TeachRank],[OrganRange],[Instructor],[Status],[ApplyRemark],[Display],[Delflag],[CreateDate],[PartitionId] from [dbo].[Class_Detail]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            if (!string.IsNullOrEmpty(orderBy))
                sql.Append(" order by " + orderBy);
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        /// <summary>
        /// 获取分页DataTable
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataTable GetTable(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(1) from [dbo].[Class_Detail]");
            if (!string.IsNullOrEmpty(where))
                sb.Append(" where " + where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select [Id],[Title],[TraningId],[PlanId],[SignUpStartTime],[SignUpEndTime],[OpenClassFrom],[OpenClassTo],[ClassForm],[People],[LimitPeopleCnt],[Address],[Content],[ManagerId],[OrganId],[Subject],[StudyLevel],[TeachGrade],[TeachRank],[OrganRange],[Instructor],[Status],[ApplyRemark],[Display],[Delflag],[CreateDate],[PartitionId],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Class_Detail]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(") as T where [RowNum] between " + start + " and " + end);
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        /// <summary>
        /// 取得我的课程记录数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetMyLearnCount(string where)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(1) from");
            sql.Append(" Member_ClassRegister A");
            sql.Append(" join Class_Detail B on A.ClassId=B.Id");
            sql.Append(" join Traning_Detail C on B.TraningId=C.Id");
            sql.Append(" join Organ_Detail D on C.OrganId=D.Id");
            sql.Append(" join Traning_Field E on C.TraingField=E.Id");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            return Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sql.ToString()));
        }

        /// <summary>
        /// 取得我的课程分页数据集
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<MyLearnInfo> GetMyLearnList(int pageSize, int pageIndex, string where, string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select ROW_NUMBER() over (order by " + orderBy + ") as [RowNum],");
            sql.Append("A.Id,A.AccountId,A.CurrentSchedule,A.TotalSchedule,A.IsPass,A.Result,A.TotalScore,A.ApplyRemark,");
            sql.Append("B.Id as ClassId,B.Title as ClassName,B.OpenClassFrom,B.OpenClassTo,");
            sql.Append("C.Id as TrainingId,C.Title as TrainingName,C.Pic,C.OrganId,C.TotalTime,C.OutSideType,");
            sql.Append("D.Title as OrganName,");
            sql.Append("E.Id as FieldId,E.Title as FieldName,");
            sql.Append("case when exists (select 1 from Class_TraningCommentResult where Delflag=0 and ClassID=B.Id and AccountId=A.AccountId) then 1 else 0 end as IsComment");
            sql.Append(" from Member_ClassRegister A");
            sql.Append(" join Class_Detail B on A.ClassId=B.Id");
            sql.Append(" join Traning_Detail C on B.TraningId=C.Id");
            sql.Append(" join Organ_Detail D on C.OrganId=D.Id");
            sql.Append(" join Traning_Field E on C.TraingField=E.Id");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(") as T where [RowNum] between " + start + " and " + end);
            List<MyLearnInfo> list = new List<MyLearnInfo>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    MyLearnInfo model = new MyLearnInfo();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 取得外部课程信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public OutCourseInfo GetOutCourse(string where)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select A.Id,A.OutSideType,A.OutSideLink,A.OutSideContent,");
            sql.Append("B.Title as OutCourseTitle,B.Link as OutCourseLink,B.DisplayEnterBtn");
            sql.Append(" from Traning_Detail A");
            sql.Append(" join Traning_OutCourseType B on A.OutSideType=B.Id");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                if (reader.Read())
                {
                    OutCourseInfo model = new OutCourseInfo();
                    ConvertToModel(reader, model);
                    return model;
                }
                else
                {
                    return null;
                }
            }
        }

        #region 
        private void ConvertToModel(IDataReader reader, Class_Detail model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["Title"] != DBNull.Value)
                model.Title = reader["Title"].ToString();
            if (reader["TraningId"] != DBNull.Value)
                model.TraningId = Convert.ToInt32(reader["TraningId"]);
            if (reader["PlanId"] != DBNull.Value)
                model.PlanId = Convert.ToInt32(reader["PlanId"]);
            if (reader["SignUpStartTime"] != DBNull.Value)
                model.SignUpStartTime = Convert.ToDateTime(reader["SignUpStartTime"]);
            if (reader["SignUpEndTime"] != DBNull.Value)
                model.SignUpEndTime = Convert.ToDateTime(reader["SignUpEndTime"]);
            if (reader["OpenClassFrom"] != DBNull.Value)
                model.OpenClassFrom = Convert.ToDateTime(reader["OpenClassFrom"]);
            if (reader["OpenClassTo"] != DBNull.Value)
                model.OpenClassTo = Convert.ToDateTime(reader["OpenClassTo"]);
            if (reader["ClassForm"] != DBNull.Value)
                model.ClassForm = Convert.ToInt32(reader["ClassForm"]);
            if (reader["People"] != DBNull.Value)
                model.People = Convert.ToInt32(reader["People"]);
            if (reader["LimitPeopleCnt"] != DBNull.Value)
                model.LimitPeopleCnt = Convert.ToInt32(reader["LimitPeopleCnt"]);
            if (reader["Address"] != DBNull.Value)
                model.Address = reader["Address"].ToString();
            if (reader["Content"] != DBNull.Value)
                model.Content = reader["Content"].ToString();
            if (reader["ManagerId"] != DBNull.Value)
                model.ManagerId = Convert.ToInt32(reader["ManagerId"]);
            if (reader["OrganId"] != DBNull.Value)
                model.OrganId = Convert.ToInt32(reader["OrganId"]);
            if (reader["Subject"] != DBNull.Value)
                model.Subject = Convert.ToBoolean(reader["Subject"]);
            if (reader["StudyLevel"] != DBNull.Value)
                model.StudyLevel = Convert.ToBoolean(reader["StudyLevel"]);
            if (reader["TeachGrade"] != DBNull.Value)
                model.TeachGrade = Convert.ToBoolean(reader["TeachGrade"]);
            if (reader["TeachRank"] != DBNull.Value)
                model.TeachRank = Convert.ToBoolean(reader["TeachRank"].ToString());
            if (reader["OrganRange"] != DBNull.Value)
                model.OrganRange = reader["OrganRange"].ToString();
            if (reader["Instructor"] != DBNull.Value)
                model.Instructor = Convert.ToInt32(reader["Instructor"]);
            if (reader["Status"] != DBNull.Value)
                model.Status = Convert.ToInt32(reader["Status"]);
            if (reader["ApplyRemark"] != DBNull.Value)
                model.ApplyRemark = reader["ApplyRemark"].ToString();
            if (reader["Display"] != DBNull.Value)
                model.Display = Convert.ToBoolean(reader["Display"]);
            if (reader["Delflag"] != DBNull.Value)
                model.Delflag = Convert.ToBoolean(reader["Delflag"]);
            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
            if (reader["PartitionId"] != DBNull.Value)
                model.PartitionId = Convert.ToInt32(reader["PartitionId"]);
        }

        private void ConvertToModel(IDataReader reader, MyLearnInfo model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["ClassId"] != DBNull.Value)
                model.ClassId = Convert.ToInt32(reader["ClassId"]);
            if (reader["ClassName"] != DBNull.Value)
                model.ClassName = reader["ClassName"].ToString();
            if (reader["TrainingId"] != DBNull.Value)
                model.TrainingId = Convert.ToInt32(reader["TrainingId"]);
            if (reader["TrainingName"] != DBNull.Value)
                model.TrainingName = reader["TrainingName"].ToString();
            if (reader["AccountId"] != DBNull.Value)
                model.AccountId = Convert.ToInt32(reader["AccountId"]);
            if (reader["Pic"] != DBNull.Value)
                model.Pic = reader["Pic"].ToString();
            if (reader["OrganId"] != DBNull.Value)
                model.OrganId = Convert.ToInt32(reader["OrganId"]);
            if (reader["OrganName"] != DBNull.Value)
                model.OrganName = reader["OrganName"].ToString();
            if (reader["CurrentSchedule"] != DBNull.Value)
                model.CurrentSchedule = Convert.ToInt32(reader["CurrentSchedule"]);
            if (reader["TotalSchedule"] != DBNull.Value)
                model.TotalSchedule = Convert.ToInt32(reader["TotalSchedule"]);
            if (reader["OpenClassFrom"] != DBNull.Value)
                model.OpenClassFrom = Convert.ToDateTime(reader["OpenClassFrom"]);
            if (reader["OpenClassTo"] != DBNull.Value)
                model.OpenClassTo = Convert.ToDateTime(reader["OpenClassTo"]);
            if (reader["FieldId"] != DBNull.Value)
                model.FieldId = Convert.ToInt32(reader["FieldId"]);
            if (reader["FieldName"] != DBNull.Value)
                model.FieldName = reader["FieldName"].ToString();
            if (reader["TotalTime"] != DBNull.Value)
                model.TotalTime = Convert.ToDouble(reader["TotalTime"]);
            if (reader["OutSideType"] != DBNull.Value)
                model.OutSideType = Convert.ToInt32(reader["OutSideType"]);
            if (reader["IsPass"] != DBNull.Value)
                model.IsPass = Convert.ToBoolean(reader["IsPass"]);
            if (reader["Result"] != DBNull.Value)
                model.Result = Convert.ToInt32(reader["Result"]);
            if (reader["TotalScore"] != DBNull.Value)
                model.TotalScore = Convert.ToInt32(reader["TotalScore"]);
            if (reader["ApplyRemark"] != DBNull.Value)
                model.ApplyRemark = reader["ApplyRemark"].ToString();
            if (reader["IsComment"] != DBNull.Value)
                model.IsComment = Convert.ToBoolean(reader["IsComment"]);
        }

        private void ConvertToModel(IDataReader reader, OutCourseInfo model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["OutSideType"] != DBNull.Value)
                model.OutSideType = Convert.ToInt32(reader["OutSideType"]);
            if (reader["OutSideLink"] != DBNull.Value)
                model.OutSideLink = reader["OutSideLink"].ToString();
            if (reader["OutSideContent"] != DBNull.Value)
                model.OutSideContent = reader["OutSideContent"].ToString();
            if (reader["OutCourseTitle"] != DBNull.Value)
                model.OutCourseTitle = reader["OutCourseTitle"].ToString();
            if (reader["OutCourseLink"] != DBNull.Value)
                model.OutCourseLink = reader["OutCourseLink"].ToString();
            if (reader["DisplayEnterBtn"] != DBNull.Value)
                model.DisplayEnterBtn = Convert.ToBoolean(reader["DisplayEnterBtn"]);
        }
        #endregion
    }
}
