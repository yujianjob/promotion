using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Dianda.AP.DAL
{
	public class Traning_AttachmentDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(Traning_Attachment model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("insert into [dbo].[Traning_Attachment] ([TraningId],[Title],[Link],[Sort],[Display],[Delflag],[CreateDate])");
			sql.Append(" values (@TraningId,@Title,@Link,@Sort,@Display,@Delflag,@CreateDate)");
			sql.Append(" set @Id=@@IDENTITY");
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@TraningId", SqlDbType.Int, 4) { Value = model.TraningId },
				new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
				new SqlParameter("@Link", SqlDbType.VarChar, 500) { Value = model.Link },
				new SqlParameter("@Sort", SqlDbType.Int, 4) { Value = model.Sort },
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
		public int Update(Traning_Attachment model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("update [dbo].[Traning_Attachment] set ");
			sql.Append("[TraningId]=@TraningId,[Title]=@Title,[Link]=@Link,[Sort]=@Sort,[Display]=@Display,[Delflag]=@Delflag,[CreateDate]=@CreateDate");
			sql.Append(" where [Id]=@Id");
			SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@TraningId", SqlDbType.Int, 4) { Value = model.TraningId },
				new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
				new SqlParameter("@Link", SqlDbType.VarChar, 500) { Value = model.Link },
				new SqlParameter("@Sort", SqlDbType.Int, 4) { Value = model.Sort },
				new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = model.Display },
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
		public Traning_Attachment GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[Traning_Attachment] where [Id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					Traning_Attachment model = new Traning_Attachment();
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
		public List<Traning_Attachment> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[Traning_Attachment]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<Traning_Attachment> list = new List<Traning_Attachment>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Traning_Attachment model = new Traning_Attachment();
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
		public List<Traning_Attachment> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			StringBuilder sb = new StringBuilder();
			sb.Append("select count(1) from [dbo].[Traning_Attachment]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Traning_Attachment]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<Traning_Attachment> list = new List<Traning_Attachment>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Traning_Attachment model = new Traning_Attachment();
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
			sql.Append("select [Id],[TraningId],[Title],[Link],[Sort],[Display],[Delflag],[CreateDate] from [dbo].[Traning_Attachment]");
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
			sb.Append("select count(1) from [dbo].[Traning_Attachment]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select [Id],[TraningId],[Title],[Link],[Sort],[Display],[Delflag],[CreateDate],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Traning_Attachment]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

		private void ConvertToModel(IDataReader reader, Traning_Attachment model)
		{
			if (reader["Id"] != DBNull.Value)
				model.Id = Convert.ToInt32(reader["Id"]);
			if (reader["TraningId"] != DBNull.Value)
				model.TraningId = Convert.ToInt32(reader["TraningId"]);
			if (reader["Title"] != DBNull.Value)
				model.Title = reader["Title"].ToString();
			if (reader["Link"] != DBNull.Value)
				model.Link = reader["Link"].ToString();
			if (reader["Sort"] != DBNull.Value)
				model.Sort = Convert.ToInt32(reader["Sort"]);
			if (reader["Display"] != DBNull.Value)
				model.Display = Convert.ToBoolean(reader["Display"]);
			if (reader["Delflag"] != DBNull.Value)
				model.Delflag = Convert.ToBoolean(reader["Delflag"]);
			if (reader["CreateDate"] != DBNull.Value)
				model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
		}

	}
}