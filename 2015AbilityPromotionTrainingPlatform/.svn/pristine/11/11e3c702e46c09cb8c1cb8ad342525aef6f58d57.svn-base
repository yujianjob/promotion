using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dianda.AP.Model;

namespace Dianda.AP.DAL
{
	public class Member_TeachSubjectDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(Member_TeachSubject model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("insert into [dbo].[Member_TeachSubject] ([AccountId],[TeachSubject],[Delflag],[CreateDate])");
			sql.Append(" values (@AccountId,@TeachSubject,@Delflag,@CreateDate)");
			sql.Append(" set @Id=@@IDENTITY");
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@TeachSubject", SqlDbType.Int, 4) { Value = model.TeachSubject },
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
		public int Update(Member_TeachSubject model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("update [dbo].[Member_TeachSubject] set ");
			sql.Append("[AccountId]=@AccountId,[TeachSubject]=@TeachSubject,[Delflag]=@Delflag,[CreateDate]=@CreateDate");
			sql.Append(" where [Id]=@Id");
			SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@TeachSubject", SqlDbType.Int, 4) { Value = model.TeachSubject },
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
		public Member_TeachSubject GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[Member_TeachSubject] where [Id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					Member_TeachSubject model = new Member_TeachSubject();
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
		/// 取得记录数
		/// </summary>
		/// <param name="where"></param>
		/// <returns></returns>
		public int GetCount(string where)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select count(1) from [dbo].[Member_TeachSubject]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			return Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sql.ToString()));
		}

		/// <summary>
		/// 获取数据集
		/// </summary>
		/// <param name="where"></param>
		/// <param name="orderBy"></param>
		/// <returns></returns>
		public List<Member_TeachSubject> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[Member_TeachSubject]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<Member_TeachSubject> list = new List<Member_TeachSubject>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Member_TeachSubject model = new Member_TeachSubject();
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
		/// <returns></returns>
		public List<Member_TeachSubject> GetList(int pageSize, int pageIndex, string where, string orderBy)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Member_TeachSubject]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<Member_TeachSubject> list = new List<Member_TeachSubject>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Member_TeachSubject model = new Member_TeachSubject();
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
			sql.Append("select [Id],[AccountId],[TeachSubject],[Delflag],[CreateDate] from [dbo].[Member_TeachSubject]");
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
		/// <returns></returns>
		public DataTable GetTable(int pageSize, int pageIndex, string where, string orderBy)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select [Id],[AccountId],[TeachSubject],[Delflag],[CreateDate],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Member_TeachSubject]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

		private void ConvertToModel(IDataReader reader, Member_TeachSubject model)
		{
			if (reader["Id"] != DBNull.Value)
				model.Id = Convert.ToInt32(reader["Id"]);
			if (reader["AccountId"] != DBNull.Value)
				model.AccountId = Convert.ToInt32(reader["AccountId"]);
			if (reader["TeachSubject"] != DBNull.Value)
				model.TeachSubject = Convert.ToInt32(reader["TeachSubject"]);
			if (reader["Delflag"] != DBNull.Value)
				model.Delflag = Convert.ToBoolean(reader["Delflag"]);
			if (reader["CreateDate"] != DBNull.Value)
				model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
		}

	}
}
