using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Dianda.AP.DAL
{
	public class Traning_InfoFkDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(Traning_InfoFk model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("insert into [dbo].[Traning_InfoFk] ([CategoryType],[Title],[Remark],[ParentId],[Sort],[Display],[Delflag],[CreateDate])");
			sql.Append(" values (@CategoryType,@Title,@Remark,@ParentId,@Sort,@Display,@Delflag,@CreateDate)");
			sql.Append(" set @Id=@@IDENTITY");
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@CategoryType", SqlDbType.Int, 4) { Value = model.CategoryType },
				new SqlParameter("@Title", SqlDbType.VarChar, 50) { Value = model.Title },
				new SqlParameter("@Remark", SqlDbType.VarChar, 500) { Value = model.Remark },
				new SqlParameter("@ParentId", SqlDbType.Int, 4) { Value = model.ParentId },
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
		public int Update(Traning_InfoFk model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("update [dbo].[Traning_InfoFk] set ");
			sql.Append("[CategoryType]=@CategoryType,[Title]=@Title,[Remark]=@Remark,[ParentId]=@ParentId,[Sort]=@Sort,[Display]=@Display,[Delflag]=@Delflag,[CreateDate]=@CreateDate");
			sql.Append(" where [Id]=@Id");
			SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@CategoryType", SqlDbType.Int, 4) { Value = model.CategoryType },
				new SqlParameter("@Title", SqlDbType.VarChar, 50) { Value = model.Title },
				new SqlParameter("@Remark", SqlDbType.VarChar, 500) { Value = model.Remark },
				new SqlParameter("@ParentId", SqlDbType.Int, 4) { Value = model.ParentId },
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
		public Traning_InfoFk GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[Traning_InfoFk] where [Id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					Traning_InfoFk model = new Traning_InfoFk();
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
		public List<Traning_InfoFk> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[Traning_InfoFk]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<Traning_InfoFk> list = new List<Traning_InfoFk>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Traning_InfoFk model = new Traning_InfoFk();
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
		public List<Traning_InfoFk> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			StringBuilder sb = new StringBuilder();
			sb.Append("select count(1) from [dbo].[Traning_InfoFk]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Traning_InfoFk]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<Traning_InfoFk> list = new List<Traning_InfoFk>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Traning_InfoFk model = new Traning_InfoFk();
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
			sql.Append("select [Id],[CategoryType],[Title],[Remark],[ParentId],[Sort],[Display],[Delflag],[CreateDate] from [dbo].[Traning_InfoFk]");
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
			sb.Append("select count(1) from [dbo].[Traning_InfoFk]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select [Id],[CategoryType],[Title],[Remark],[ParentId],[Sort],[Display],[Delflag],[CreateDate],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Traning_InfoFk]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

		private void ConvertToModel(IDataReader reader, Traning_InfoFk model)
		{
			if (reader["Id"] != DBNull.Value)
				model.Id = Convert.ToInt32(reader["Id"]);
			if (reader["CategoryType"] != DBNull.Value)
				model.CategoryType = Convert.ToInt32(reader["CategoryType"]);
			if (reader["Title"] != DBNull.Value)
				model.Title = reader["Title"].ToString();
			if (reader["Remark"] != DBNull.Value)
				model.Remark = reader["Remark"].ToString();
			if (reader["ParentId"] != DBNull.Value)
				model.ParentId = Convert.ToInt32(reader["ParentId"]);
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
