using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Dianda.AP.DAL
{
	public class Member_PracticalCourseDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(Member_PracticalCourse model)
		{
			StringBuilder sql = new StringBuilder();
            sql.Append("insert into [dbo].[Member_PracticalCourse] ([AccountId],[PracticalCourseId],[OrganId],[Creater],[Status],[ApplyRemark],[RoleId],[Delflag],[CreateDate])");
            sql.Append(" values (@AccountId,@PracticalCourseId,@OrganId,@Creater,@Status,@ApplyRemark,@RoleId,@Delflag,@CreateDate)");
			sql.Append(" set @Id=@@IDENTITY");
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@PracticalCourseId", SqlDbType.Int, 4) { Value = model.PracticalCourseId },
				new SqlParameter("@OrganId", SqlDbType.Int, 4) { Value = model.OrganId },
				new SqlParameter("@Creater", SqlDbType.Int, 4) { Value = model.Creater },
				new SqlParameter("@Status", SqlDbType.Int, 4) { Value = model.Status },
				new SqlParameter("@ApplyRemark", SqlDbType.VarChar, 500) { Value = model.ApplyRemark },
                new SqlParameter("@RoleId", SqlDbType.Int, 4) { Value = model.RoleId },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate }
			};
			int result = Convert.ToInt32(MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams));
			model.Id = Convert.ToInt32(cmdParams[0].Value);
            return model.Id;
		}

		/// <summary>
		/// 更新一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Update(Member_PracticalCourse model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("update [dbo].[Member_PracticalCourse] set ");
            sql.Append("[AccountId]=@AccountId,[PracticalCourseId]=@PracticalCourseId,[OrganId]=@OrganId,[Creater]=@Creater,[Status]=@Status,[ApplyRemark]=@ApplyRemark,[RoleId]=@RoleId,[Delflag]=@Delflag,[CreateDate]=@CreateDate");
			sql.Append(" where [Id]=@Id");
			SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@PracticalCourseId", SqlDbType.Int, 4) { Value = model.PracticalCourseId },
				new SqlParameter("@OrganId", SqlDbType.Int, 4) { Value = model.OrganId },
				new SqlParameter("@Creater", SqlDbType.Int, 4) { Value = model.Creater },
				new SqlParameter("@Status", SqlDbType.Int, 4) { Value = model.Status },
				new SqlParameter("@ApplyRemark", SqlDbType.VarChar, 500) { Value = model.ApplyRemark },
                new SqlParameter("@RoleId", SqlDbType.Int, 4) { Value = model.RoleId },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate }
			};
			return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
		}

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public Member_PracticalCourse GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[Member_PracticalCourse] where [Id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					Member_PracticalCourse model = new Member_PracticalCourse();
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
		public List<Member_PracticalCourse> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[Member_PracticalCourse]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<Member_PracticalCourse> list = new List<Member_PracticalCourse>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Member_PracticalCourse model = new Member_PracticalCourse();
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
		public List<Member_PracticalCourse> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			StringBuilder sb = new StringBuilder();
			sb.Append("select count(1) from [dbo].[Member_PracticalCourse]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Member_PracticalCourse]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<Member_PracticalCourse> list = new List<Member_PracticalCourse>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Member_PracticalCourse model = new Member_PracticalCourse();
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
			sql.Append("select [Id],[AccountId],[PracticalCourseId],[OrganId],[Creater],[Status],[ApplyRemark],[Delflag],[CreateDate] from [dbo].[Member_PracticalCourse]");
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
			sb.Append("select count(1) from [dbo].[Member_PracticalCourse]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select [Id],[AccountId],[PracticalCourseId],[OrganId],[Creater],[Status],[ApplyRemark],[Delflag],[CreateDate],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Member_PracticalCourse]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

		private void ConvertToModel(IDataReader reader, Member_PracticalCourse model)
		{
			if (reader["Id"] != DBNull.Value)
				model.Id = Convert.ToInt32(reader["Id"]);
			if (reader["AccountId"] != DBNull.Value)
				model.AccountId = Convert.ToInt32(reader["AccountId"]);
			if (reader["PracticalCourseId"] != DBNull.Value)
				model.PracticalCourseId = Convert.ToInt32(reader["PracticalCourseId"]);
			if (reader["OrganId"] != DBNull.Value)
				model.OrganId = Convert.ToInt32(reader["OrganId"]);
			if (reader["Creater"] != DBNull.Value)
				model.Creater = Convert.ToInt32(reader["Creater"]);
			if (reader["Status"] != DBNull.Value)
				model.Status = Convert.ToInt32(reader["Status"]);
			if (reader["ApplyRemark"] != DBNull.Value)
				model.ApplyRemark = reader["ApplyRemark"].ToString();
			if (reader["Delflag"] != DBNull.Value)
				model.Delflag = Convert.ToBoolean(reader["Delflag"]);
			if (reader["CreateDate"] != DBNull.Value)
				model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
            if (reader["RoleId"] != DBNull.Value)
                model.RoleId = Convert.ToInt32(reader["RoleId"]);
		}

        public List<PracticalCourseListModel> GetMPCourseList(PracticalCourseSearchModel pc,int OrganId, int pageIndex, int pageSize, out int total)
        {
            List<PracticalCourseListModel> list = new List<PracticalCourseListModel>();

            string condition = " and OrganId='" + OrganId + "'";// " and (delflag = 0) ";
            if (!string.IsNullOrEmpty(pc.SearchTitle))
                condition += " and (Nickname like '%" + pc.SearchTitle + "%' or TeacherNo  like '%" + pc.SearchTitle + "%' or CredentialsNumber  like '%" + pc.SearchTitle + "%' or Title like'%" + pc.SearchTitle + "%' )";

            if (pc.State != -1)
                condition += " and Status = " + pc.State;

            if(!string.IsNullOrEmpty(pc.Where))
                condition += pc.Where;
            total = 0;
            IDataParameter[] parms = new IDataParameter[] { 
                new SqlParameter("@QueryFields", "*"),
                new SqlParameter("@QueryTb", "v_MemPraCourseList"),
                new SqlParameter("@Condition", condition),
                new SqlParameter("@SortCondition", " id desc  "),
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@PageSize", pageSize)
            };
            using (IDataReader dr = MSEntLibSqlHelper.ExecuteDataReaderByStoredProc("sp_pager", parms))
            {
                while (dr.Read())
                {
                    PracticalCourseListModel m = new PracticalCourseListModel();
                    m.Id = Convert.ToInt32(dr["id"]);
                    m.Title = dr["Title"].ToString();
                    m.UserName = dr["UserName"].ToString();
                    m.Nickname = dr["Nickname"].ToString();
                    m.TeacherNo = dr["TeacherNo"].ToString();
                    m.CreateDate = Convert.ToDateTime(dr["CreateDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["TraingCategory"].ToString()))
                    {
                        m.TraingCategory = Convert.ToInt32(dr["TraingCategory"]);
                    }
                    if (!string.IsNullOrEmpty(dr["TraingCategory"].ToString()))
                    {
                        m.TraingTopic = Convert.ToInt32(dr["TraingTopic"]);
                    }
                    if (dr["OrganId"] != null)
                    {
                        m.OrganId = Convert.ToInt32(dr["OrganId"].ToString());
                    }
                    if (dr["Credits"] != null)
                    {
                        m.Credits = dr["Credits"].ToString();
                    }
                    if (dr["OrganId"] != null)
                    {
                        m.OrganId = Convert.ToInt32(dr["OrganId"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dr["Creater"].ToString()))
                    {
                        m.Creater = Convert.ToInt32(dr["Creater"].ToString());
                    }
                    else {
                        m.Creater = Convert.ToInt32(dr["AccountId"].ToString());
                    }
                    m.Status = Convert.ToInt32(dr["Status"].ToString());

                    list.Add(m);
                }
                if (dr.NextResult() && dr.Read())
                {
                    total = Convert.ToInt32(dr[0]);
                }
            }
            return list;
        }


        public MPracticalCourseModel GetMPCourseModel(int id)
        {
            string sql = "select * from [dbo].[Member_PracticalCourse] where [Id]=@Id";
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                if (reader.Read())
                {
                    MPracticalCourseModel model = new MPracticalCourseModel();
                    ConvertToModel(reader, model);
                    return model;
                }
                else
                {
                    return null;
                }
            }
        }
	}
}
