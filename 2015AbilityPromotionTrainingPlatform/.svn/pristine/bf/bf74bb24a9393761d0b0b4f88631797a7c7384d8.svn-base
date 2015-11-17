using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Dianda.AP.DAL
{
	public class Traning_ApplyApplicationDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(Traning_ApplyApplication model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("insert into [dbo].[Traning_ApplyApplication] ([TraningId],[Status],[Remark],[Creater],[Delflag],[CreateDate],[OrganId])");
			sql.Append(" values (@TraningId,@Status,@Remark,@Creater,@Delflag,@CreateDate,@OrganId)");
			sql.Append(" set @Id=@@IDENTITY");
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@TraningId", SqlDbType.Int, 4) { Value = model.TraningId },
				new SqlParameter("@Status", SqlDbType.Int, 4) { Value = model.Status },
				new SqlParameter("@Remark", SqlDbType.VarChar, 500) { Value = model.Remark },
				new SqlParameter("@Creater", SqlDbType.Int, 4) { Value = model.Creater },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@OrganId", SqlDbType.Int, 4) { Value = model.OrganId }
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
		public int Update(Traning_ApplyApplication model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("update [dbo].[Traning_ApplyApplication] set ");
			sql.Append("[TraningId]=@TraningId,[Status]=@Status,[Remark]=@Remark,[Creater]=@Creater,[Delflag]=@Delflag,[CreateDate]=@CreateDate,[OrganId]=@OrganId");
			sql.Append(" where [Id]=@Id");
			SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@TraningId", SqlDbType.Int, 4) { Value = model.TraningId },
				new SqlParameter("@Status", SqlDbType.Int, 4) { Value = model.Status },
				new SqlParameter("@Remark", SqlDbType.VarChar, 500) { Value = model.Remark },
				new SqlParameter("@Creater", SqlDbType.Int, 4) { Value = model.Creater },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@OrganId", SqlDbType.Int, 4) { Value = model.OrganId }
			};
			return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
		}

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public Traning_ApplyApplication GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[Traning_ApplyApplication] where [Id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					Traning_ApplyApplication model = new Traning_ApplyApplication();
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
		public List<Traning_ApplyApplication> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[Traning_ApplyApplication]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<Traning_ApplyApplication> list = new List<Traning_ApplyApplication>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Traning_ApplyApplication model = new Traning_ApplyApplication();
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
		public List<Traning_ApplyApplication> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			StringBuilder sb = new StringBuilder();
			sb.Append("select count(1) from [dbo].[Traning_ApplyApplication]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Traning_ApplyApplication]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<Traning_ApplyApplication> list = new List<Traning_ApplyApplication>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Traning_ApplyApplication model = new Traning_ApplyApplication();
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
			sql.Append("select [Id],[TraningId],[Status],[Remark],[Creater],[Delflag],[CreateDate],[OrganId] from [dbo].[Traning_ApplyApplication]");
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
			sb.Append("select count(1) from [dbo].[Traning_ApplyApplication]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select [Id],[TraningId],[Status],[Remark],[Creater],[Delflag],[CreateDate],[OrganId],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Traning_ApplyApplication]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

		private void ConvertToModel(IDataReader reader, Traning_ApplyApplication model)
		{
			if (reader["Id"] != DBNull.Value)
				model.Id = Convert.ToInt32(reader["Id"]);
			if (reader["TraningId"] != DBNull.Value)
				model.TraningId = Convert.ToInt32(reader["TraningId"]);
			if (reader["Status"] != DBNull.Value)
				model.Status = Convert.ToInt32(reader["Status"]);
			if (reader["Remark"] != DBNull.Value)
				model.Remark = reader["Remark"].ToString();
			if (reader["Creater"] != DBNull.Value)
				model.Creater = Convert.ToInt32(reader["Creater"]);
			if (reader["Delflag"] != DBNull.Value)
				model.Delflag = Convert.ToBoolean(reader["Delflag"]);
			if (reader["CreateDate"] != DBNull.Value)
				model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
			if (reader["OrganId"] != DBNull.Value)
				model.OrganId = Convert.ToInt32(reader["OrganId"]);
		}

	}
}
