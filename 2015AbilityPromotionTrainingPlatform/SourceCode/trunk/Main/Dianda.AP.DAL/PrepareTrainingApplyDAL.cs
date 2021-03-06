﻿using Dianda.AP.Model.Prepare.TrainingApply;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.DAL
{
    public class PrepareTrainingApplyDAL
    {
        /// <summary>
        /// 判断课程是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool CheckExists(string where)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(1) from [Traning_Detail]");
            if (!string.IsNullOrEmpty(where))
                sb.Append(" where " + where);
            return Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString())) > 0;
        }

        /// <summary>
        /// 取得课程总数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetTrainingInfoCount(string where)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(1) from (select * from [dbo].[Traning_Detail]) as T");
            if (!string.IsNullOrEmpty(where))
                sb.Append(" where " + where);
            return Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
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
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select *,");
            sql.Append("(select count(1) from Member_ClassRegister where Delflag=0 and Status=4 and TrainingId=T.Id) as Trainers,");
            sql.Append("(select Title from Organ_Detail where Delflag=0 and Id=T.OrganId) as OrganName,");
            sql.Append("(select Title from Traning_InfoFk where Delflag=0 and CategoryType=6 and Id=T.TeacherTitle) as TeacherTitleName");
            sql.Append(" from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Traning_Detail]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(") as T where [RowNum] between " + start + " and " + end);
            List<TrainingInfo> list = new List<TrainingInfo>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    TrainingInfo model = new TrainingInfo();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 批量更新课程附件
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public void BatchAttach(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                switch (row.RowState)
                {
                    case DataRowState.Added:
                        sql.Append("insert into [dbo].[Traning_Attachment] ([TraningId],[Title],[Link],[Sort],[Display],[Delflag],[CreateDate]) values "
                            + "(" + row["TraningId"] + ","
                            + "'" + row["Title"] + "',"
                            + "'" + row["Link"] + "',"
                            + row["Sort"] + ","
                            + "'" + row["Display"] + "',"
                            + "'" + row["Delflag"] + "',"
                            + "'" + row["CreateDate"] + "') ");
                        break;
                    case DataRowState.Deleted:
                        sql.Append("update [dbo].[Traning_Attachment] set Delflag=1 where [Id]=" + row["Id", DataRowVersion.Original] + " ");
                        break;
                }
            }

            if(!string.IsNullOrEmpty(sql.ToString()))
                MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString());
        }

        /// <summary>
        /// 取得课程教师总数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetTeacherCount(string where)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(1) from");
            sql.Append(" Member_Account A");
            sql.Append(" join PlatformManager_Detail B on A.Id=B.AccountId");
            sql.Append(" join Organ_Detail C on A.OrganId=C.Id");
            sql.Append(" left join Member_BaseInfo D on D.AccountId=A.Id");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            return Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sql.ToString()));
        }

        /// <summary>
        /// 取得课程教师分页数据集
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public DataTable GetTeacherList(int pageSize, int pageIndex, string where, string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select ROW_NUMBER() over (order by " + orderBy + ") as [RowNum],");
            sql.Append("A.*,");
            sql.Append("B.Id as ManagerId,");
            sql.Append("C.Title as OrganName,");
            sql.Append("D.Mobile from");
            sql.Append(" Member_Account A");
            sql.Append(" join PlatformManager_Detail B on A.Id=B.AccountId");
            sql.Append(" join Organ_Detail C on B.OrganId=C.Id");
            sql.Append(" left join Member_BaseInfo D on D.AccountId=A.Id");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(") as T where [RowNum] between " + start + " and " + end);
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        /// <summary>
        /// 批量安排教师
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public void BatchSelectTeacher(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                switch (row.RowState)
                {
                    case DataRowState.Added:
                        sql.Append("insert into [dbo].[Traning_Teacher] ([TraningId],[PlatformManagerId],[Status],[Delflag],[CreateDate]) values "
                            + "(" + row["TraningId"] + ","
                            + row["PlatformManagerId"] + ","
                            + row["Status"] + ","
                            + "'" + row["Delflag"] + "',"
                            + "'" + row["CreateDate"] + "') ");
                        break;
                    case DataRowState.Deleted:
                        sql.Append("update [dbo].[Traning_Teacher] set Delflag=1 where [Id]=" + row["Id", DataRowVersion.Original] + " ");
                        break;
                }
            }

            if (!string.IsNullOrEmpty(sql.ToString()))
                MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString());
        }

        private void ConvertToModel(IDataReader reader, TrainingInfo model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["Title"] != DBNull.Value)
                model.Title = reader["Title"].ToString();
            if (reader["Content"] != DBNull.Value)
                model.Content = reader["Content"].ToString();
            if (reader["Pic"] != DBNull.Value)
                model.Pic = reader["Pic"].ToString();
            if (reader["Creater"] != DBNull.Value)
                model.Creater = Convert.ToInt32(reader["Creater"]);
            if (reader["OrganId"] != DBNull.Value)
                model.OrganId = Convert.ToInt32(reader["OrganId"]);
            if (reader["TraingField"] != DBNull.Value)
                model.TraingField = Convert.ToInt32(reader["TraingField"]);
            if (reader["TraingCategory"] != DBNull.Value)
                model.TraingCategory = Convert.ToInt32(reader["TraingCategory"]);
            if (reader["TraingTopic"] != DBNull.Value)
                model.TraingTopic = Convert.ToInt32(reader["TraingTopic"]);
            if (reader["TraningObject"] != DBNull.Value)
                model.TraningObject = reader["TraningObject"].ToString();
            if (reader["Subject"] != DBNull.Value)
                model.Subject = reader["Subject"].ToString();
            if (reader["StudyLevel"] != DBNull.Value)
                model.StudyLevel = reader["StudyLevel"].ToString();
            if (reader["TotalTime"] != DBNull.Value)
                model.TotalTime = Convert.ToDouble(reader["TotalTime"]);
            if (reader["TrainingForm"] != DBNull.Value)
                model.TrainingForm = Convert.ToInt32(reader["TrainingForm"]);
            if (reader["TeacherTitle"] != DBNull.Value)
                model.TeacherTitle = Convert.ToInt32(reader["TeacherTitle"]);
            if (reader["TeacherName"] != DBNull.Value)
                model.TeacherName = reader["TeacherName"].ToString();
            if (reader["TeacherFrom"] != DBNull.Value)
                model.TeacherFrom = reader["TeacherFrom"].ToString();
            if (reader["TeacherPic"] != DBNull.Value)
                model.TeacherPic = reader["TeacherPic"].ToString();
            if (reader["Outline"] != DBNull.Value)
                model.Outline = reader["Outline"].ToString();
            if (reader["PartitionId"] != DBNull.Value)
                model.PartitionId = Convert.ToInt32(reader["PartitionId"]);
            if (reader["OutSideType"] != DBNull.Value)
                model.OutSideType = Convert.ToInt32(reader["OutSideType"]);
            if (reader["OutSideLink"] != DBNull.Value)
                model.OutSideLink = reader["OutSideLink"].ToString();
            if (reader["Range"] != DBNull.Value)
                model.Range = Convert.ToInt32(reader["Range"]);
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
            if (reader["ParentOrganId"] != DBNull.Value)
                model.ParentOrganId = Convert.ToInt32(reader["ParentOrganId"]);
            if (reader["OrganName"] != DBNull.Value)
                model.OrganName = reader["OrganName"].ToString();
            //if (reader["SubjectName"] != DBNull.Value)
            //    model.SubjectName = reader["SubjectName"].ToString();
            if (reader["Trainers"] != DBNull.Value)
                model.Trainers = Convert.ToInt32(reader["Trainers"]);
            if (reader["TeacherTitleName"] != DBNull.Value)
                model.TeacherTitleName = reader["TeacherTitleName"].ToString();
            if (reader["CanEdit"] != DBNull.Value)
                model.CanEdit = Convert.ToBoolean(reader["CanEdit"]);
        }
    }
}
