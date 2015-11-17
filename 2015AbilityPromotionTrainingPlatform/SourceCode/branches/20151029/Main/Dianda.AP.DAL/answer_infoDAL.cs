using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dianda.AP.Model;

namespace Dianda.AP.DAL
{
	public class answer_infoDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(answer_info model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("insert into [dbo].[answer_info] ([userid],[qid],[answer],[reason],[num])");
			sql.Append(" values (@userid,@qid,@answer,@reason,@num)");
			sql.Append(" set @id=@@IDENTITY");
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@id", SqlDbType.Int, 4) { Value = model.id, Direction = ParameterDirection.Output },
				new SqlParameter("@userid", SqlDbType.Int, 4) { Value = model.userid },
				new SqlParameter("@qid", SqlDbType.Int, 4) { Value = model.qid },
				new SqlParameter("@answer", SqlDbType.VarChar, 10) { Value = model.answer },
				new SqlParameter("@reason", SqlDbType.VarChar, 400) { Value = model.reason },
				new SqlParameter("@num", SqlDbType.Int, 4) { Value = model.num }
			};
			int result = Convert.ToInt32(MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams));
			model.id = Convert.ToInt32(cmdParams[0].Value);
			return result;
		}

		/// <summary>
		/// 更新一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Update(answer_info model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("update [dbo].[answer_info] set ");
			sql.Append("[userid]=@userid,[qid]=@qid,[answer]=@answer,[reason]=@reason,[num]=@num");
			sql.Append(" where [id]=@id");
			SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@id", SqlDbType.Int, 4) { Value = model.id },
				new SqlParameter("@userid", SqlDbType.Int, 4) { Value = model.userid },
				new SqlParameter("@qid", SqlDbType.Int, 4) { Value = model.qid },
				new SqlParameter("@answer", SqlDbType.VarChar, 10) { Value = model.answer },
				new SqlParameter("@reason", SqlDbType.VarChar, 400) { Value = model.reason },
				new SqlParameter("@num", SqlDbType.Int, 4) { Value = model.num }
			};
			return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
		}

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public answer_info GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[answer_info] where [id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					answer_info model = new answer_info();
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
		public List<answer_info> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[answer_info]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<answer_info> list = new List<answer_info>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					answer_info model = new answer_info();
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
		public List<answer_info> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			StringBuilder sb = new StringBuilder();
			sb.Append("select count(1) from [dbo].[answer_info]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[answer_info]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<answer_info> list = new List<answer_info>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					answer_info model = new answer_info();
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
			sql.Append("select [id],[userid],[qid],[answer],[reason],[num] from [dbo].[answer_info]");
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
			sb.Append("select count(1) from [dbo].[answer_info]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select [id],[userid],[qid],[answer],[reason],[num],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[answer_info]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

		private void ConvertToModel(IDataReader reader, answer_info model)
		{
			if (reader["id"] != DBNull.Value)
				model.id = Convert.ToInt32(reader["id"]);
			if (reader["userid"] != DBNull.Value)
				model.userid = Convert.ToInt32(reader["userid"]);
			if (reader["qid"] != DBNull.Value)
				model.qid = Convert.ToInt32(reader["qid"]);
			if (reader["answer"] != DBNull.Value)
				model.answer = reader["answer"].ToString();
			if (reader["reason"] != DBNull.Value)
				model.reason = reader["reason"].ToString();
			if (reader["num"] != DBNull.Value)
				model.num = Convert.ToInt32(reader["num"]);
		}

	}
}
