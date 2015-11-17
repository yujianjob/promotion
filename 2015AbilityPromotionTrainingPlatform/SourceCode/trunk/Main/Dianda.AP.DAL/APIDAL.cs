﻿using Dianda.AP.Model;
using Dianda.AP.Model.API;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.DAL
{
    public class APIDAL
    {
        #region 学校
        public int AddOrgan(Organ_Detail model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into [dbo].[Organ_Detail] ([Title],[ParentId],[OType],[Remark],[Delflag],[CreateDate],[PartitionId],[StudyRegionId],[OutSourceId])");
            sql.Append(" values (@Title,@ParentId,@OType,@Remark,@Delflag,@CreateDate,@PartitionId,@StudyRegionId,@OutSourceId)");
            sql.Append(" set @Id=@@IDENTITY");
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
				new SqlParameter("@ParentId", SqlDbType.Int, 4) { Value = model.ParentId },
				new SqlParameter("@OType", SqlDbType.Int, 4) { Value = model.OType },
				new SqlParameter("@Remark", SqlDbType.VarChar, 50) { Value = model.Remark },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@PartitionId", SqlDbType.Int, 4) { Value = model.PartitionId },
				new SqlParameter("@StudyRegionId", SqlDbType.Int, 4) { Value = model.StudyRegionId },
				new SqlParameter("@OutSourceId", SqlDbType.Int, 4) { Value = model.OutSourceId }
			};
            int result = Convert.ToInt32(MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams));
            model.Id = Convert.ToInt32(cmdParams[0].Value);
            return result;
        }

        public int UpdateOrgan(Organ_Detail model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Organ_Detail] set ");
            sql.Append("[Title]=@Title,[ParentId]=@ParentId,[OType]=@OType,[Remark]=@Remark,[Delflag]=@Delflag,[CreateDate]=@CreateDate,[PartitionId]=@PartitionId,[StudyRegionId]=@StudyRegionId,[OutSourceId]=@OutSourceId");
            sql.Append(" where [Id]=@Id");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
				new SqlParameter("@ParentId", SqlDbType.Int, 4) { Value = model.ParentId },
				new SqlParameter("@OType", SqlDbType.Int, 4) { Value = model.OType },
				new SqlParameter("@Remark", SqlDbType.VarChar, 50) { Value = model.Remark },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@PartitionId", SqlDbType.Int, 4) { Value = model.PartitionId },
				new SqlParameter("@StudyRegionId", SqlDbType.Int, 4) { Value = model.StudyRegionId },
				new SqlParameter("@OutSourceId", SqlDbType.Int, 4) { Value = model.OutSourceId }
			};
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
        }

        public int DeleteOrgan(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Organ_Detail] set Delflag=1 where Id=@Id");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
        }

        public Organ_Detail GetOrganByOutId(int outId)
        {
            string sql = "select * from [dbo].[Organ_Detail] where [OutSourceId]=@OutId and Delflag=0";
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@OutId", SqlDbType.Int, 4) { Value = outId }
			};
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                if (reader.Read())
                {
                    Organ_Detail model = new Organ_Detail();
                    ConvertToModel(reader, model);
                    return model;
                }
                else
                {
                    return null;
                }
            }
        }

        private void ConvertToModel(IDataReader reader, Organ_Detail model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["Title"] != DBNull.Value)
                model.Title = reader["Title"].ToString();
            if (reader["ParentId"] != DBNull.Value)
                model.ParentId = Convert.ToInt32(reader["ParentId"]);
            if (reader["OType"] != DBNull.Value)
                model.OType = Convert.ToInt32(reader["OType"]);
            if (reader["Remark"] != DBNull.Value)
                model.Remark = reader["Remark"].ToString();
            if (reader["Delflag"] != DBNull.Value)
                model.Delflag = Convert.ToBoolean(reader["Delflag"]);
            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
            if (reader["PartitionId"] != DBNull.Value)
                model.PartitionId = Convert.ToInt32(reader["PartitionId"]);
            if (reader["StudyRegionId"] != DBNull.Value)
                model.StudyRegionId = Convert.ToInt32(reader["StudyRegionId"]);
            if (reader["OutSourceId"] != DBNull.Value)
                model.OutSourceId = Convert.ToInt32(reader["OutSourceId"]);
        }
        #endregion

        #region 教师
        public int AddMemberAccount(Member_Account model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into [dbo].[Member_Account] ([UserName],[Password],[Email],[Status],[RegisterIP],[OrganId],[Pic],[Nickname],[IsRealName],[originUserId],[origin],[OriginUserName],[IsNeedReName],[Delflag],[CreateDate],[OutSourceId])");
            sql.Append(" values (@UserName,@Password,@Email,@Status,@RegisterIP,@OrganId,@Pic,@Nickname,@IsRealName,@originUserId,@origin,@OriginUserName,@IsNeedReName,@Delflag,@CreateDate,@OutSourceId)");
            sql.Append(" set @Id=@@IDENTITY");
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@UserName", SqlDbType.VarChar, 50) { Value = model.UserName },
				new SqlParameter("@Password", SqlDbType.VarChar, 100) { Value = model.Password },
				new SqlParameter("@Email", SqlDbType.VarChar, 200) { Value = model.Email },
				new SqlParameter("@Status", SqlDbType.Int, 4) { Value = model.Status },
				new SqlParameter("@RegisterIP", SqlDbType.VarChar, 20) { Value = model.RegisterIP },
				new SqlParameter("@OrganId", SqlDbType.Int, 4) { Value = model.OrganId },
				new SqlParameter("@Pic", SqlDbType.VarChar, 200) { Value = model.Pic },
				new SqlParameter("@Nickname", SqlDbType.VarChar, 50) { Value = model.Nickname },
				new SqlParameter("@IsRealName", SqlDbType.Bit, 1) { Value = model.IsRealName },
				new SqlParameter("@originUserId", SqlDbType.VarChar, 50) { Value = model.originUserId },
				new SqlParameter("@origin", SqlDbType.Int, 4) { Value = model.origin },
				new SqlParameter("@OriginUserName", SqlDbType.VarChar, 50) { Value = model.OriginUserName },
				new SqlParameter("@IsNeedReName", SqlDbType.Bit, 1) { Value = model.IsNeedReName },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@OutSourceId", SqlDbType.Int, 4) { Value = model.OutSourceId }
			};
            int result = Convert.ToInt32(MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams));
            model.Id = Convert.ToInt32(cmdParams[0].Value);
            return result;
        }

        public int AddMemberBaseInfo(MemberBaseInfo model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into [dbo].[Member_BaseInfo] ([AccountId],[TeacherNo],[GroupId],[RealName],[Mobile],[Phone],[Address],[PostCode],[CredentialsType],[CredentialsNumber],[Sex],[Birthday],[Nation],[PoliticalStatus],[Delflag],[CreateDate],[RegionId],[StudySection],[Organid],[Job],[WorkRank],[TeachDate],[TeachStudySection],[TeachSubject],[TeachGrade],[TraningType],[TraningObject],[EduLevel],[EduDegree],[EduMajor],[EduMajorOhter],[GraduateSchool],[GraduateTime])");
            sql.Append(" values (@AccountId,@TeacherNo,@GroupId,@RealName,@Mobile,@Phone,@Address,@PostCode,@CredentialsType,@CredentialsNumber,@Sex,@Birthday,@Nation,@PoliticalStatus,@Delflag,@CreateDate,@RegionId,@StudySection,@Organid,@Job,@WorkRank,@TeachDate,@TeachStudySection,@TeachSubject,@TeachGrade,@TraningType,@TraningObject,@EduLevel,@EduDegree,@EduMajor,@EduMajorOhter,@GraduateSchool,@GraduateTime)");
            sql.Append(" set @Id=@@IDENTITY");
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@TeacherNo", SqlDbType.VarChar, 100) { Value = model.TeacherNo },
				new SqlParameter("@GroupId", SqlDbType.Int, 4) { Value = model.GroupId },
				new SqlParameter("@RealName", SqlDbType.VarChar, 120) { Value = model.RealName },
				new SqlParameter("@Mobile", SqlDbType.VarChar, 30) { Value = model.Mobile },
				new SqlParameter("@Phone", SqlDbType.VarChar, 30) { Value = model.Phone },
				new SqlParameter("@Address", SqlDbType.VarChar, 300) { Value = model.Address },
				new SqlParameter("@PostCode", SqlDbType.VarChar, 50) { Value = model.PostCode },
				new SqlParameter("@CredentialsType", SqlDbType.Int, 4) { Value = model.CredentialsType },
				new SqlParameter("@CredentialsNumber", SqlDbType.VarChar, 30) { Value = model.CredentialsNumber },
				new SqlParameter("@Sex", SqlDbType.Int, 4) { Value = model.Sex },
				new SqlParameter("@Birthday", SqlDbType.DateTime, 8) { Value = model.Birthday },
				new SqlParameter("@Nation", SqlDbType.Int, 4) { Value = model.Nation },
				new SqlParameter("@PoliticalStatus", SqlDbType.Int, 4) { Value = model.PoliticalStatus },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@RegionId", SqlDbType.Int, 4) { Value = model.RegionId },
				new SqlParameter("@StudySection", SqlDbType.VarChar, 200) { Value = model.StudySection },
				new SqlParameter("@Organid", SqlDbType.Int, 4) { Value = model.Organid },
				new SqlParameter("@Job", SqlDbType.Int, 4) { Value = model.Job },
				new SqlParameter("@WorkRank", SqlDbType.Int, 4) { Value = model.WorkRank },
				new SqlParameter("@TeachDate", SqlDbType.DateTime, 8) { Value = model.TeachDate },
				new SqlParameter("@TeachStudySection", SqlDbType.VarChar, 200) { Value = model.TeachStudySection },
				new SqlParameter("@TeachSubject", SqlDbType.VarChar, 200) { Value = model.TeachSubject },
				new SqlParameter("@TeachGrade", SqlDbType.VarChar, 200) { Value = model.TeachGrade },
				new SqlParameter("@TraningType", SqlDbType.Int, 4) { Value = model.TraningType },
				new SqlParameter("@TraningObject", SqlDbType.Int, 4) { Value = model.TraningObject },
				new SqlParameter("@EduLevel", SqlDbType.Int, 4) { Value = model.EduLevel },
				new SqlParameter("@EduDegree", SqlDbType.Int, 4) { Value = model.EduDegree },
				new SqlParameter("@EduMajor", SqlDbType.Int, 4) { Value = model.EduMajor },
				new SqlParameter("@EduMajorOhter", SqlDbType.VarChar, 200) { Value = model.EduMajorOhter },
				new SqlParameter("@GraduateSchool", SqlDbType.VarChar, 200) { Value = model.GraduateSchool },
				new SqlParameter("@GraduateTime", SqlDbType.DateTime, 8) { Value = model.GraduateTime }
			};
            int result = Convert.ToInt32(MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams));
            model.Id = Convert.ToInt32(cmdParams[0].Value);
            return result;
        }

        public int UpdateMemberAccount(Member_Account model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Member_Account] set ");
            sql.Append("[UserName]=@UserName,[Password]=@Password,[Email]=@Email,[Status]=@Status,[RegisterIP]=@RegisterIP,[OrganId]=@OrganId,[Pic]=@Pic,[Nickname]=@Nickname,[IsRealName]=@IsRealName,[originUserId]=@originUserId,[origin]=@origin,[OriginUserName]=@OriginUserName,[IsNeedReName]=@IsNeedReName,[Delflag]=@Delflag,[CreateDate]=@CreateDate,[OutSourceId]=@OutSourceId");
            sql.Append(" where [Id]=@Id");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@UserName", SqlDbType.VarChar, 50) { Value = model.UserName },
				new SqlParameter("@Password", SqlDbType.VarChar, 100) { Value = model.Password },
				new SqlParameter("@Email", SqlDbType.VarChar, 200) { Value = model.Email },
				new SqlParameter("@Status", SqlDbType.Int, 4) { Value = model.Status },
				new SqlParameter("@RegisterIP", SqlDbType.VarChar, 20) { Value = model.RegisterIP },
				new SqlParameter("@OrganId", SqlDbType.Int, 4) { Value = model.OrganId },
				new SqlParameter("@Pic", SqlDbType.VarChar, 200) { Value = model.Pic },
				new SqlParameter("@Nickname", SqlDbType.VarChar, 50) { Value = model.Nickname },
				new SqlParameter("@IsRealName", SqlDbType.Bit, 1) { Value = model.IsRealName },
				new SqlParameter("@originUserId", SqlDbType.VarChar, 50) { Value = model.originUserId },
				new SqlParameter("@origin", SqlDbType.Int, 4) { Value = model.origin },
				new SqlParameter("@OriginUserName", SqlDbType.VarChar, 50) { Value = model.OriginUserName },
				new SqlParameter("@IsNeedReName", SqlDbType.Bit, 1) { Value = model.IsNeedReName },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@OutSourceId", SqlDbType.Int, 4) { Value = model.OutSourceId }
			};
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
        }

        public int UpdateMemberBaseInfo(MemberBaseInfo model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Member_BaseInfo] set ");
            sql.Append("[TeacherNo]=@TeacherNo,[GroupId]=@GroupId,[RealName]=@RealName,[Mobile]=@Mobile,[Phone]=@Phone,[Address]=@Address,[PostCode]=@PostCode,[CredentialsType]=@CredentialsType,[CredentialsNumber]=@CredentialsNumber,[Sex]=@Sex,[Birthday]=@Birthday,[Nation]=@Nation,[PoliticalStatus]=@PoliticalStatus,[Delflag]=@Delflag,[CreateDate]=@CreateDate,[RegionId]=@RegionId,[StudySection]=@StudySection,[Organid]=@Organid,[Job]=@Job,[WorkRank]=@WorkRank,[TeachDate]=@TeachDate,[TeachStudySection]=@TeachStudySection,[TeachSubject]=@TeachSubject,[TeachGrade]=@TeachGrade,[TraningType]=@TraningType,[TraningObject]=@TraningObject,[EduLevel]=@EduLevel,[EduDegree]=@EduDegree,[EduMajor]=@EduMajor,[EduMajorOhter]=@EduMajorOhter,[GraduateSchool]=@GraduateSchool,[GraduateTime]=@GraduateTime");
            sql.Append(" where [AccountId]=@AccountId");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@TeacherNo", SqlDbType.VarChar, 100) { Value = model.TeacherNo },
				new SqlParameter("@GroupId", SqlDbType.Int, 4) { Value = model.GroupId },
				new SqlParameter("@RealName", SqlDbType.VarChar, 120) { Value = model.RealName },
				new SqlParameter("@Mobile", SqlDbType.VarChar, 30) { Value = model.Mobile },
				new SqlParameter("@Phone", SqlDbType.VarChar, 30) { Value = model.Phone },
				new SqlParameter("@Address", SqlDbType.VarChar, 300) { Value = model.Address },
				new SqlParameter("@PostCode", SqlDbType.VarChar, 50) { Value = model.PostCode },
				new SqlParameter("@CredentialsType", SqlDbType.Int, 4) { Value = model.CredentialsType },
				new SqlParameter("@CredentialsNumber", SqlDbType.VarChar, 30) { Value = model.CredentialsNumber },
				new SqlParameter("@Sex", SqlDbType.Int, 4) { Value = model.Sex },
				new SqlParameter("@Birthday", SqlDbType.DateTime, 8) { Value = model.Birthday },
				new SqlParameter("@Nation", SqlDbType.Int, 4) { Value = model.Nation },
				new SqlParameter("@PoliticalStatus", SqlDbType.Int, 4) { Value = model.PoliticalStatus },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@RegionId", SqlDbType.Int, 4) { Value = model.RegionId },
				new SqlParameter("@StudySection", SqlDbType.VarChar, 200) { Value = model.StudySection },
				new SqlParameter("@Organid", SqlDbType.Int, 4) { Value = model.Organid },
				new SqlParameter("@Job", SqlDbType.Int, 4) { Value = model.Job },
				new SqlParameter("@WorkRank", SqlDbType.Int, 4) { Value = model.WorkRank },
				new SqlParameter("@TeachDate", SqlDbType.DateTime, 8) { Value = model.TeachDate },
				new SqlParameter("@TeachStudySection", SqlDbType.VarChar, 200) { Value = model.TeachStudySection },
				new SqlParameter("@TeachSubject", SqlDbType.VarChar, 200) { Value = model.TeachSubject },
				new SqlParameter("@TeachGrade", SqlDbType.VarChar, 200) { Value = model.TeachGrade },
				new SqlParameter("@TraningType", SqlDbType.Int, 4) { Value = model.TraningType },
				new SqlParameter("@TraningObject", SqlDbType.Int, 4) { Value = model.TraningObject },
				new SqlParameter("@EduLevel", SqlDbType.Int, 4) { Value = model.EduLevel },
				new SqlParameter("@EduDegree", SqlDbType.Int, 4) { Value = model.EduDegree },
				new SqlParameter("@EduMajor", SqlDbType.Int, 4) { Value = model.EduMajor },
				new SqlParameter("@EduMajorOhter", SqlDbType.VarChar, 200) { Value = model.EduMajorOhter },
				new SqlParameter("@GraduateSchool", SqlDbType.VarChar, 200) { Value = model.GraduateSchool },
				new SqlParameter("@GraduateTime", SqlDbType.DateTime, 8) { Value = model.GraduateTime }
			};
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
        }

        public int DeleteMemberAccount(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Member_Account] set [Delflag]=1 where [Id]=@Id");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
        }

        public int DeleteMemberBaseInfo(int accountId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Member_BaseInfo] set [Delflag]=1 where [AccountId]=@AccountId");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = accountId }
			};
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
        }

        public Member_Account GetMemberAccountByOutId(int outId)
        {
            string sql = "select * from [dbo].[Member_Account] where [OutSourceId]=@OutId and Delflag=0";
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@OutId", SqlDbType.Int, 4) { Value = outId }
			};
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                if (reader.Read())
                {
                    Member_Account model = new Member_Account();
                    ConvertToModel(reader, model);
                    return model;
                }
                else
                {
                    return null;
                }
            }
        }

        public Member_Account GetMemberAccountByUserName(string userName)
        {
            string sql = "select * from [dbo].[Member_Account] where [UserName]=@UserName collate Chinese_PRC_CS_AS and Delflag=0";
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@UserName", SqlDbType.VarChar, 50) { Value = userName }
			};
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                if (reader.Read())
                {
                    Member_Account model = new Member_Account();
                    ConvertToModel(reader, model);
                    return model;
                }
                else
                {
                    return null;
                }
            }
        }

        public Member_BaseInfo GetMemberInfoByAccountId(int accountId)
        {
            string sql = "select * from [dbo].[Member_BaseInfo] where [AccountId]=@AccountId and Delflag=0";
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = accountId }
			};
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                if (reader.Read())
                {
                    Member_BaseInfo model = new Member_BaseInfo();
                    ConvertToModel(reader, model);
                    return model;
                }
                else
                {
                    return null;
                }
            }
        }

        //更新学科
        public void UpdateSubject(int accountId, string source, string target)
        {
            List<string> added = CollectionCut(target, source);
            List<string> deleted = CollectionCut(source, target);
            if (added.Count() > 0)
            {
                foreach (string s in added)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("insert into [dbo].[Member_TeachSubject] ([AccountId],[TeachSubject])");
                    sql.Append(" values (@AccountId,@SourceId)");
                    SqlParameter[] cmdParams = new SqlParameter[]{
				        new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = accountId },
				        new SqlParameter("@SourceId", SqlDbType.Int, 4) { Value = s }
			        };
                    MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
                }
            }
            if (deleted.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in deleted)
                {
                    sb.Append(s + ",");
                }
                sb.Remove(sb.Length - 1, 1);
                MSEntLibSqlHelper.ExecuteNonQueryBySql("update Member_TeachSubject set Delflag=1"
                    + " where Delflag=0 and AccountId=" + accountId + " and TeachSubject in (" + sb.ToString() + ")");
            }
        }

        //删除学科
        public void DeleteSubject(int accountId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update Member_TeachSubject set Delflag=1 where Delflag=0 and AccountId=" + accountId);
            MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString());
        }

        //更新学段
        public void UpdateSection(int accountId, string source, string target)
        {
            List<string> added = CollectionCut(target, source);
            List<string> deleted = CollectionCut(source, target);
            if (added.Count() > 0)
            {
                foreach (string s in added)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("insert into [dbo].[Member_StudySection] ([AccountId],[StudySection])");
                    sql.Append(" values (@AccountId,@SourceId)");
                    SqlParameter[] cmdParams = new SqlParameter[]{
				        new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = accountId },
				        new SqlParameter("@SourceId", SqlDbType.Int, 4) { Value = s }
			        };
                    MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
                }
            }
            if (deleted.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in deleted)
                {
                    sb.Append(s + ",");
                }
                sb.Remove(sb.Length - 1, 1);
                MSEntLibSqlHelper.ExecuteNonQueryBySql("update Member_StudySection set Delflag=1"
                    + " where Delflag=0 and AccountId=" + accountId + " and StudySection in (" + sb.ToString() + ")");
            }
        }

        //删除学段
        public void DeleteSection(int accountId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update Member_StudySection set Delflag=1 where Delflag=0 and AccountId=" + accountId);
            MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString());
        }

        //更新年级
        public void UpdateGrade(int accountId, string source, string target)
        {
            List<string> added = CollectionCut(target, source);
            List<string> deleted = CollectionCut(source, target);
            if (added.Count() > 0)
            {
                foreach (string s in added)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("insert into [dbo].[Member_TeachGrade] ([AccountId],[TeachGrade])");
                    sql.Append(" values (@AccountId,@SourceId)");
                    SqlParameter[] cmdParams = new SqlParameter[]{
				        new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = accountId },
				        new SqlParameter("@SourceId", SqlDbType.Int, 4) { Value = s }
			        };
                    MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
                }
            }
            if (deleted.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in deleted)
                {
                    sb.Append(s + ",");
                }
                sb.Remove(sb.Length - 1, 1);
                MSEntLibSqlHelper.ExecuteNonQueryBySql("update Member_TeachGrade set Delflag=1"
                    + " where Delflag=0 and AccountId=" + accountId + " and TeachGrade in (" + sb.ToString() + ")");
            }
        }

        //删除年级
        public void DeleteGrade(int accountId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update Member_TeachGrade set Delflag=1 where Delflag=0 and AccountId=" + accountId);
            MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString());
        }

        //更新职务
        public void UpdateJob(int accountId, string source, string target)
        {
            List<string> added = CollectionCut(target, source);
            List<string> deleted = CollectionCut(source, target);
            if (added.Count() > 0)
            {
                foreach (string s in added)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("insert into [dbo].[Member_Job] ([AccountId],[Job])");
                    sql.Append(" values (@AccountId,@SourceId)");
                    SqlParameter[] cmdParams = new SqlParameter[]{
				        new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = accountId },
				        new SqlParameter("@SourceId", SqlDbType.Int, 4) { Value = s }
			        };
                    MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
                }
            }
            if (deleted.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in deleted)
                {
                    sb.Append(s + ",");
                }
                sb.Remove(sb.Length - 1, 1);
                MSEntLibSqlHelper.ExecuteNonQueryBySql("update Member_Job set Delflag=1"
                    + " where Delflag=0 and AccountId=" + accountId + " and Job in (" + sb.ToString() + ")");
            }
        }

        //删除职务
        public void DeleteJob(int accountId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update Member_Job set Delflag=1 where Delflag=0 and AccountId=" + accountId);
            MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString());
        }

        //更新职称
        public void UpdateRank(int accountId, string source, string target)
        {
            List<string> added = CollectionCut(target, source);
            List<string> deleted = CollectionCut(source, target);
            if (added.Count() > 0)
            {
                foreach (string s in added)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("insert into [dbo].[Member_WorkRank] ([AccountId],[WorkRank])");
                    sql.Append(" values (@AccountId,@SourceId)");
                    SqlParameter[] cmdParams = new SqlParameter[]{
				        new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = accountId },
				        new SqlParameter("@SourceId", SqlDbType.Int, 4) { Value = s }
			        };
                    MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
                }
            }
            if (deleted.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in deleted)
                {
                    sb.Append(s + ",");
                }
                sb.Remove(sb.Length - 1, 1);
                MSEntLibSqlHelper.ExecuteNonQueryBySql("update Member_WorkRank set Delflag=1"
                    + " where Delflag=0 and AccountId=" + accountId + " and WorkRank in (" + sb.ToString() + ")");
            }
        }

        //删除职称
        public void DeleteRank(int accountId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update Member_WorkRank set Delflag=1 where Delflag=0 and AccountId=" + accountId);
            MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString());
        }

        //更新培训形式
        public void UpdateTrainingType(int accountId, string source, string target)
        {
            List<string> added = CollectionCut(target, source);
            List<string> deleted = CollectionCut(source, target);
            if (added.Count() > 0)
            {
                foreach (string s in added)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("insert into [dbo].[Member_TraningType] ([AccountId],[TraningType])");
                    sql.Append(" values (@AccountId,@SourceId)");
                    SqlParameter[] cmdParams = new SqlParameter[]{
				        new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = accountId },
				        new SqlParameter("@SourceId", SqlDbType.Int, 4) { Value = s }
			        };
                    MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
                }
            }
            if (deleted.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in deleted)
                {
                    sb.Append(s + ",");
                }
                sb.Remove(sb.Length - 1, 1);
                MSEntLibSqlHelper.ExecuteNonQueryBySql("update Member_TraningType set Delflag=1"
                    + " where Delflag=0 and AccountId=" + accountId + " and TraningType in (" + sb.ToString() + ")");
            }
        }

        //删除培训形式
        public void DeleteTrainingType(int accountId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update Member_TraningType set Delflag=1 where Delflag=0 and AccountId=" + accountId);
            MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString());
        }

        //更新权限
        public void UpdateGroup(int accountId, int quxian, int school, string source, string target)
        {
            List<string> added = CollectionCut(target, source);
            List<string> deleted = CollectionCut(source, target);
            if (added.Count() > 0)
            {
                foreach (string s in added)
                {
                    int organId;
                    if (s == "1")
                    {
                        organId = 1;
                    }
                    else if (s == "2")
                    {
                        organId = quxian;
                    }
                    else
                    {
                        organId = school;
                    }
                    StringBuilder sql = new StringBuilder();
                    sql.Append("insert into [dbo].[PlatformManager_Detail] ([AccountId],[PartitionId],[Status],[OrganId],[Level],[GroupId])");
                    sql.Append(" values (@AccountId,1,1,@OrganId,0,@SourceId)");
                    SqlParameter[] cmdParams = new SqlParameter[]{
				        new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = accountId },
                        new SqlParameter("@OrganId", SqlDbType.Int, 4) { Value = organId },
				        new SqlParameter("@SourceId", SqlDbType.Int, 4) { Value = s }
			        };
                    MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
                }
            }
            if (deleted.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in deleted)
                {
                    sb.Append(s + ",");
                }
                sb.Remove(sb.Length - 1, 1);
                MSEntLibSqlHelper.ExecuteNonQueryBySql("update PlatformManager_Detail set Delflag=1"
                    + " where Delflag=0 and AccountId=" + accountId + " and GroupId in (" + sb.ToString() + ")");
            }
        }

        //删除权限
        public void DeleteGroup(int accountId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update PlatformManager_Detail set Delflag=1 where Delflag=0 and AccountId=" + accountId);
            MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString());
        }

        //将外部编号转为内部Id
        public string ConvertKey(string table, string outKey)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(outKey) && !string.IsNullOrEmpty(table))
            {
                StringBuilder sql = new StringBuilder();
                switch (table)
                {
                    case "Member_TeachSubject"://学科
                        sql.Append("select Id from [Traning_InfoFk] where CategoryType=3 and OutSourceId in (" + outKey + ")");
                        break;
                    case "Member_StudySection"://学段
                        sql.Append("select Id from [Traning_InfoFk] where CategoryType=4 and OutSourceId in (" + outKey + ")");
                        break;
                    case "Member_TeachGrade"://年级
                        sql.Append("select Id from [Traning_InfoFk] where CategoryType=7 and OutSourceId in (" + outKey + ")");
                        break;
                    case "Member_Job"://职务
                        sql.Append("select Id from [Traning_InfoFk] where CategoryType=10 and OutSourceId in (" + outKey + ")");
                        break;
                    case "Member_WorkRank"://职称
                        sql.Append("select Id from [Traning_InfoFk] where CategoryType=8 and OutSourceId in (" + outKey + ")");
                        break;
                    case "Member_TraningType"://培训类型
                        sql.Append("select Id from [Traning_InfoFk] where CategoryType=5 and OutSourceId in (" + outKey + ")");
                        break;
                    case "Organ"://机构
                        sql.Append("select top 1.Id from [Organ_Detail] where OutSourceId=" + outKey);
                        break;
                    case "Nation"://民族
                        sql.Append("select top 1.Id from [Member_InfoFK] where InfoType=1 and OutSourceId=" + outKey);
                        break;
                    case "Political"://政治面貌
                        sql.Append("select top 1.Id from [Member_InfoFK] where InfoType=2 and OutSourceId=" + outKey);
                        break;
                    case "Region"://学区
                        sql.Append("select top 1.Id from [Organ_StudyRegion] where OutSourceId=" + outKey);
                        break;
                    default:
                        return "";

                }

                using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
                {
                    while (reader.Read())
                    {
                        list.Add(reader[0].ToString());
                    }
                }
            }

            if (list.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in list)
                {
                    sb.Append(s + ",");
                }
                sb.Remove(sb.Length - 1, 1);
                return sb.ToString();
            }
            else
            {
                return "";
            }
        }

        //根据用户编号取得外键Id字符串
        public string GetKeyByAccountId(string table, int accountId)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(table) && accountId != 0)
            {
                StringBuilder sql = new StringBuilder();
                switch (table)
                {
                    case "Member_TeachSubject"://学科
                        sql.Append("select TeachSubject from [Member_TeachSubject] where Delflag=0 and AccountId=" + accountId);
                        break;
                    case "Member_StudySection"://学段
                        sql.Append("select StudySection from [Member_StudySection] where Delflag=0 and AccountId=" + accountId);
                        break;
                    case "Member_TeachGrade"://年级
                        sql.Append("select TeachGrade from [Member_TeachGrade] where Delflag=0 and AccountId=" + accountId);
                        break;
                    case "Member_Job"://职务
                        sql.Append("select Job from [Member_Job] where Delflag=0 and AccountId=" + accountId);
                        break;
                    case "Member_WorkRank"://职称
                        sql.Append("select WorkRank from [Member_WorkRank] where Delflag=0 and AccountId=" + accountId);
                        break;
                    case "Member_TraningType"://培训类型
                        sql.Append("select TraningType from [Member_TraningType] where Delflag=0 and AccountId=" + accountId);
                        break;
                    case "PlatformManager_Detail"://权限
                        sql.Append("select GroupId from [PlatformManager_Detail] where Delflag=0 and AccountId=" + accountId);
                        break;
                    default:
                        return "";
                }

                using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
                {
                    while (reader.Read())
                    {
                        list.Add(reader[0].ToString());
                    }
                }
            }

            if (list.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in list)
                {
                    sb.Append(s + ",");
                }
                sb.Remove(sb.Length - 1, 1);
                return sb.ToString();
            }
            else
            {
                return "";
            }
        }

        //求a和b的差集
        private List<string> CollectionCut(string a, string b)
        {
            List<string> list_A = new List<string>();
            List<string> list_B = new List<string>();
            if (!string.IsNullOrEmpty(a))
                list_A.AddRange(a.Split(','));
            if (!string.IsNullOrEmpty(b))
                list_B.AddRange(b.Split(','));
            return list_A.Where(x => !list_B.Exists(y => y == x)).ToList();
        }

        private void ConvertToModel(IDataReader reader, Member_Account model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["UserName"] != DBNull.Value)
                model.UserName = reader["UserName"].ToString();
            if (reader["Password"] != DBNull.Value)
                model.Password = reader["Password"].ToString();
            if (reader["Email"] != DBNull.Value)
                model.Email = reader["Email"].ToString();
            if (reader["Status"] != DBNull.Value)
                model.Status = Convert.ToInt32(reader["Status"]);
            if (reader["RegisterIP"] != DBNull.Value)
                model.RegisterIP = reader["RegisterIP"].ToString();
            if (reader["OrganId"] != DBNull.Value)
                model.OrganId = Convert.ToInt32(reader["OrganId"]);
            if (reader["Pic"] != DBNull.Value)
                model.Pic = reader["Pic"].ToString();
            if (reader["Nickname"] != DBNull.Value)
                model.Nickname = reader["Nickname"].ToString();
            if (reader["IsRealName"] != DBNull.Value)
                model.IsRealName = Convert.ToBoolean(reader["IsRealName"]);
            if (reader["originUserId"] != DBNull.Value)
                model.originUserId = reader["originUserId"].ToString();
            if (reader["origin"] != DBNull.Value)
                model.origin = Convert.ToInt32(reader["origin"]);
            if (reader["OriginUserName"] != DBNull.Value)
                model.OriginUserName = reader["OriginUserName"].ToString();
            if (reader["IsNeedReName"] != DBNull.Value)
                model.IsNeedReName = Convert.ToBoolean(reader["IsNeedReName"]);
            if (reader["Delflag"] != DBNull.Value)
                model.Delflag = Convert.ToBoolean(reader["Delflag"]);
            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
            if (reader["OutSourceId"] != DBNull.Value)
                model.OutSourceId = Convert.ToInt32(reader["OutSourceId"]);
        }

        private void ConvertToModel(IDataReader reader, Member_BaseInfo model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["AccountId"] != DBNull.Value)
                model.AccountId = Convert.ToInt32(reader["AccountId"]);
            if (reader["TeacherNo"] != DBNull.Value)
                model.TeacherNo = reader["TeacherNo"].ToString();
            if (reader["GroupId"] != DBNull.Value)
                model.GroupId = Convert.ToInt32(reader["GroupId"]);
            if (reader["RealName"] != DBNull.Value)
                model.RealName = reader["RealName"].ToString();
            if (reader["Mobile"] != DBNull.Value)
                model.Mobile = reader["Mobile"].ToString();
            if (reader["Phone"] != DBNull.Value)
                model.Phone = reader["Phone"].ToString();
            if (reader["Address"] != DBNull.Value)
                model.Address = reader["Address"].ToString();
            if (reader["PostCode"] != DBNull.Value)
                model.PostCode = reader["PostCode"].ToString();
            if (reader["CredentialsType"] != DBNull.Value)
                model.CredentialsType = Convert.ToInt32(reader["CredentialsType"]);
            if (reader["CredentialsNumber"] != DBNull.Value)
                model.CredentialsNumber = reader["CredentialsNumber"].ToString();
            if (reader["Sex"] != DBNull.Value)
                model.Sex = Convert.ToInt32(reader["Sex"]);
            if (reader["Birthday"] != DBNull.Value)
                model.Birthday = Convert.ToDateTime(reader["Birthday"]);
            if (reader["Nation"] != DBNull.Value)
                model.Nation = Convert.ToInt32(reader["Nation"]);
            if (reader["PoliticalStatus"] != DBNull.Value)
                model.PoliticalStatus = Convert.ToInt32(reader["PoliticalStatus"]);
            if (reader["Delflag"] != DBNull.Value)
                model.Delflag = Convert.ToBoolean(reader["Delflag"]);
            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
            if (reader["RegionId"] != DBNull.Value)
                model.RegionId = Convert.ToInt32(reader["RegionId"]);
            if (reader["StudySection"] != DBNull.Value)
                model.StudySection = reader["StudySection"].ToString();
            if (reader["Organid"] != DBNull.Value)
                model.Organid = Convert.ToInt32(reader["Organid"]);
            if (reader["Job"] != DBNull.Value)
                model.Job = Convert.ToInt32(reader["Job"]);
            if (reader["WorkRank"] != DBNull.Value)
                model.WorkRank = Convert.ToInt32(reader["WorkRank"]);
            if (reader["TeachDate"] != DBNull.Value)
                model.TeachDate = Convert.ToDateTime(reader["TeachDate"]);
            if (reader["TeachStudySection"] != DBNull.Value)
                model.TeachStudySection = reader["TeachStudySection"].ToString();
            if (reader["TeachSubject"] != DBNull.Value)
                model.TeachSubject = reader["TeachSubject"].ToString();
            if (reader["TeachGrade"] != DBNull.Value)
                model.TeachGrade = reader["TeachGrade"].ToString();
            if (reader["TraningType"] != DBNull.Value)
                model.TraningType = Convert.ToInt32(reader["TraningType"]);
            if (reader["TraningObject"] != DBNull.Value)
                model.TraningObject = Convert.ToInt32(reader["TraningObject"]);
            if (reader["EduLevel"] != DBNull.Value)
                model.EduLevel = Convert.ToInt32(reader["EduLevel"]);
            if (reader["EduDegree"] != DBNull.Value)
                model.EduDegree = Convert.ToInt32(reader["EduDegree"]);
            if (reader["EduMajor"] != DBNull.Value)
                model.EduMajor = Convert.ToInt32(reader["EduMajor"]);
            if (reader["EduMajorOhter"] != DBNull.Value)
                model.EduMajorOhter = reader["EduMajorOhter"].ToString();
            if (reader["GraduateSchool"] != DBNull.Value)
                model.GraduateSchool = reader["GraduateSchool"].ToString();
            if (reader["GraduateTime"] != DBNull.Value)
                model.GraduateTime = Convert.ToDateTime(reader["GraduateTime"]);
        }
        #endregion

    }
}