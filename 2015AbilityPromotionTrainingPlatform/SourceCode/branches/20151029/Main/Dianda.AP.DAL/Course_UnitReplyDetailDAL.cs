using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dianda.AP.Model;

namespace Dianda.AP.DAL
{
    public class Course_UnitReplyDetailDAL
    {
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Course_UnitReplyDetail model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into [dbo].[Course_UnitReplyDetail] ([UnitContent],[ClassId],[Content],[AccountId],[ParentReplyId],[AttList],[Display],[Delflag],[CreateDate])");
            sql.Append(" values (@UnitContent,@ClassId,@Content,@AccountId,@ParentReplyId,@AttList,@Display,@Delflag,@CreateDate)");
            sql.Append(" set @Id=@@IDENTITY");
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@UnitContent", SqlDbType.Int, 4) { Value = model.UnitContent },
				new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = model.ClassId },
				new SqlParameter("@Content", SqlDbType.VarChar, 2000) { Value = model.Content },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@ParentReplyId", SqlDbType.Int, 4) { Value = model.ParentReplyId },
				new SqlParameter("@AttList", SqlDbType.VarChar, 2000) { Value = model.AttList },
				new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = model.Display },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate }
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
        public int Update(Course_UnitReplyDetail model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Course_UnitReplyDetail] set ");
            sql.Append("[UnitContent]=@UnitContent,[ClassId]=@ClassId,[Content]=@Content,[AccountId]=@AccountId,[ParentReplyId]=@ParentReplyId,[AttList]=@AttList,[Display]=@Display,[Delflag]=@Delflag,[CreateDate]=@CreateDate");
            sql.Append(" where [Id]=@Id");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@UnitContent", SqlDbType.Int, 4) { Value = model.UnitContent },
				new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = model.ClassId },
				new SqlParameter("@Content", SqlDbType.VarChar, 2000) { Value = model.Content },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@ParentReplyId", SqlDbType.Int, 4) { Value = model.ParentReplyId },
				new SqlParameter("@AttList", SqlDbType.VarChar, 2000) { Value = model.AttList },
				new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = model.Display },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate }
			};
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
        }

        /// <summary>
        /// 删除话题,下面的回复修改Delflag = 1
        /// </summary>
        /// <param name="iId"></param>
        /// <returns></returns>
        public int Update(int iId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Course_UnitReplyDetail] set [Delflag] = 1 ,[CreateDate] = GETDATE()");
            sql.Append(" where [Id]=@Id");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = iId }
			};

            int iResult = MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);

            if (iResult > 0)
            {
                //修改该话题下的回复 Delflag = 1
                StringBuilder sqlReply = new StringBuilder();
                sqlReply.Append("update [dbo].[Course_UnitReplyDetail] set [Delflag] = 1 ,[CreateDate] = GETDATE()");
                sqlReply.Append(" where [ParentReplyId]=@ParentReplyId");
                SqlParameter[] cmdParamsReply = new SqlParameter[] { 
                    new SqlParameter("@ParentReplyId", SqlDbType.Int, 4) { Value = iId } 
                };

                MSEntLibSqlHelper.ExecuteNonQueryBySql(sqlReply.ToString(), cmdParamsReply);
            }

            return iResult;
        }

        /// <summary>
        /// 取得一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Course_UnitReplyDetail GetModel(int id, string where)
        {
            string sql = "select * from [dbo].[Course_UnitReplyDetail] where [Id]=@Id";
            if (!string.IsNullOrEmpty(where))
                sql += " and " + where;
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                if (reader.Read())
                {
                    Course_UnitReplyDetail model = new Course_UnitReplyDetail();
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
        public List<Course_UnitReplyDetail> GetList(string where, string orderBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from [dbo].[Course_UnitReplyDetail]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            if (!string.IsNullOrEmpty(orderBy))
                sql.Append(" order by " + orderBy);
            List<Course_UnitReplyDetail> list = new List<Course_UnitReplyDetail>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    Course_UnitReplyDetail model = new Course_UnitReplyDetail();
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
        public List<Course_UnitReplyDetail> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(1) from [dbo].[Course_UnitReplyDetail]");
            if (!string.IsNullOrEmpty(where))
                sb.Append(" where " + where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Course_UnitReplyDetail]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(") as T where [RowNum] between " + start + " and " + end);
            List<Course_UnitReplyDetail> list = new List<Course_UnitReplyDetail>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    Course_UnitReplyDetail model = new Course_UnitReplyDetail();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取[讨论主题]数据集总数
        /// </summary>
        /// <param name="iClassId"></param>
        /// <param name="iUnitContent"></param>
        /// <returns></returns>
        public int GetListTopicTotalCount(int iClassId, int iUnitContent)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" Select Count(1)
                                     from Course_UnitReplyDetail 
                                     left join Member_Account on Course_UnitReplyDetail.AccountId=Member_Account.Id
                                     left join Organ_Detail on Member_Account.OrganId = Organ_Detail.Id
                                     where Course_UnitReplyDetail.Display = 1 AND Course_UnitReplyDetail.Delflag = 0 AND Course_UnitReplyDetail.ParentReplyId = 0
                                     AND Course_UnitReplyDetail.ClassId = {0} AND Course_UnitReplyDetail.UnitContent = {1}",
            iClassId, iUnitContent);

            return Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sql.ToString()));
        }

        /// <summary>
        /// 获取[讨论主题]分页数据集
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="iClassId"></param>
        /// <param name="iUnitContent"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<Course_UnitReplyDetailOther> GetListTopicOther(int pageSize, int pageIndex, int iClassId, int iUnitContent, out int recordCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@" Select Count(1)
                                     from Course_UnitReplyDetail 
                                     left join Member_Account on Course_UnitReplyDetail.AccountId=Member_Account.Id
                                     left join Organ_Detail on Member_Account.OrganId = Organ_Detail.Id
                                     where Course_UnitReplyDetail.Display = 1 AND Course_UnitReplyDetail.Delflag = 0 AND Course_UnitReplyDetail.ParentReplyId = 0
                                     AND Course_UnitReplyDetail.ClassId = {0} AND Course_UnitReplyDetail.UnitContent = {1}",
            iClassId, iUnitContent);

            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" Select * from ( select *,ROW_NUMBER() over (order by ParentReplyId,CreateDate) as [RowNum] from
                             ( select Member_Account.Nickname,Member_Account.Pic,Organ_Detail.Id as OrganDetailId, Organ_Detail.Title as OrganDetailTitle, Course_UnitReplyDetail.* 
                             from Course_UnitReplyDetail 
                             left join Member_Account on Course_UnitReplyDetail.AccountId=Member_Account.Id
                             left join Organ_Detail on Member_Account.OrganId = Organ_Detail.Id
                             where Course_UnitReplyDetail.Display = 1 AND Course_UnitReplyDetail.Delflag = 0 AND Course_UnitReplyDetail.ParentReplyId = 0
                             AND Course_UnitReplyDetail.ClassId = {0} AND Course_UnitReplyDetail.UnitContent = {1}) as T1",
            iClassId, iUnitContent);
            sql.Append(") as T where [RowNum] between " + start + " and " + end);
            List<Course_UnitReplyDetailOther> list = new List<Course_UnitReplyDetailOther>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    Course_UnitReplyDetailOther model = new Course_UnitReplyDetailOther();
                    ConvertToModelOther(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取[讨论主题]下所有回复
        /// </summary>
        /// <param name="iClassId"></param>
        /// <param name="iUnitContent"></param>
        /// <param name="iParentReplyId"></param>
        /// <returns></returns>
        public List<Course_UnitReplyDetailOther> GetListReplyOther(int iClassId, int iUnitContent, int iParentReplyId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" Select Member_Account.Nickname,Member_Account.Pic,Organ_Detail.Id as OrganDetailId, Organ_Detail.Title as OrganDetailTitle, Course_UnitReplyDetail.* 
                                     from Course_UnitReplyDetail 
                                     left join Member_Account on Course_UnitReplyDetail.AccountId=Member_Account.Id
                                     left join Organ_Detail on Member_Account.OrganId = Organ_Detail.Id
                                     where Course_UnitReplyDetail.Display = 1 AND Course_UnitReplyDetail.Delflag = 0 
                                     AND Course_UnitReplyDetail.ParentReplyId = {2}
                                     AND Course_UnitReplyDetail.ClassId = {0} AND Course_UnitReplyDetail.UnitContent = {1} 
                                     Order by CreateDate asc",
            iClassId, iUnitContent, iParentReplyId);

            List<Course_UnitReplyDetailOther> list = new List<Course_UnitReplyDetailOther>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    Course_UnitReplyDetailOther model = new Course_UnitReplyDetailOther();
                    ConvertToModelOther(reader, model);
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
            sql.Append("select [Id],[UnitContent],[ClassId],[Content],[AccountId],[ParentReplyId],[AttList],[Display],[Delflag],[CreateDate] from [dbo].[Course_UnitReplyDetail]");
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
            sb.Append("select count(1) from [dbo].[Course_UnitReplyDetail]");
            if (!string.IsNullOrEmpty(where))
                sb.Append(" where " + where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select [Id],[UnitContent],[ClassId],[Content],[AccountId],[ParentReplyId],[AttList],[Display],[Delflag],[CreateDate],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Course_UnitReplyDetail]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(") as T where [RowNum] between " + start + " and " + end);
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        private void ConvertToModel(IDataReader reader, Course_UnitReplyDetail model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["UnitContent"] != DBNull.Value)
                model.UnitContent = Convert.ToInt32(reader["UnitContent"]);
            if (reader["ClassId"] != DBNull.Value)
                model.ClassId = Convert.ToInt32(reader["ClassId"]);
            if (reader["Content"] != DBNull.Value)
                model.Content = reader["Content"].ToString();
            if (reader["AccountId"] != DBNull.Value)
                model.AccountId = Convert.ToInt32(reader["AccountId"]);
            if (reader["ParentReplyId"] != DBNull.Value)
                model.ParentReplyId = Convert.ToInt32(reader["ParentReplyId"]);
            if (reader["AttList"] != DBNull.Value)
                model.AttList = reader["AttList"].ToString();
            if (reader["Display"] != DBNull.Value)
                model.Display = Convert.ToBoolean(reader["Display"]);
            if (reader["Delflag"] != DBNull.Value)
                model.Delflag = Convert.ToBoolean(reader["Delflag"]);
            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
        }

        private void ConvertToModelOther(IDataReader reader, Course_UnitReplyDetailOther modelOther)
        {
            if (reader["NickName"] != DBNull.Value)
                modelOther.NickName = Convert.ToString(reader["NickName"]);
            if (reader["Pic"] != DBNull.Value)
                modelOther.Pic = Convert.ToString(reader["Pic"]);
            if (reader["OrganDetailId"] != DBNull.Value)
                modelOther.OrganDetailId = Convert.ToInt32(reader["OrganDetailId"]);
            if (reader["OrganDetailTitle"] != DBNull.Value)
                modelOther.OrganDetailTitle = Convert.ToString(reader["OrganDetailTitle"]);


            var model = new Course_UnitReplyDetail();
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["UnitContent"] != DBNull.Value)
                model.UnitContent = Convert.ToInt32(reader["UnitContent"]);
            if (reader["ClassId"] != DBNull.Value)
                model.ClassId = Convert.ToInt32(reader["ClassId"]);
            if (reader["Content"] != DBNull.Value)
                model.Content = reader["Content"].ToString();
            if (reader["AccountId"] != DBNull.Value)
                model.AccountId = Convert.ToInt32(reader["AccountId"]);
            if (reader["ParentReplyId"] != DBNull.Value)
                model.ParentReplyId = Convert.ToInt32(reader["ParentReplyId"]);
            if (reader["AttList"] != DBNull.Value)
                model.AttList = reader["AttList"].ToString();
            if (reader["Display"] != DBNull.Value)
                model.Display = Convert.ToBoolean(reader["Display"]);
            if (reader["Delflag"] != DBNull.Value)
                model.Delflag = Convert.ToBoolean(reader["Delflag"]);
            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);

            modelOther.CourseUnitReplyDetail = model;
        }
    }
}
