using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Dianda.AP.DAL
{
	public class PlatformManager_RoleDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(PlatformManager_Role model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("insert into [dbo].[PlatformManager_Role] ([Title],[Content],[PagePath],[sort],[MenuText],[MenuPath],[ParentId],[Delflag],[CreateDate],[IsFolder])");
			sql.Append(" values (@Title,@Content,@PagePath,@sort,@MenuText,@MenuPath,@ParentId,@Delflag,@CreateDate,@IsFolder)");
			sql.Append(" set @Id=@@IDENTITY");
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@Title", SqlDbType.NVarChar, 40) { Value = model.Title },
				new SqlParameter("@Content", SqlDbType.VarChar, 300) { Value = model.Content },
				new SqlParameter("@PagePath", SqlDbType.VarChar, 500) { Value = model.PagePath },
				new SqlParameter("@sort", SqlDbType.Int, 4) { Value = model.sort },
				new SqlParameter("@MenuText", SqlDbType.VarChar, 20) { Value = model.MenuText },
				new SqlParameter("@MenuPath", SqlDbType.VarChar, 300) { Value = model.MenuPath },
				new SqlParameter("@ParentId", SqlDbType.Int, 4) { Value = model.ParentId },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@IsFolder", SqlDbType.Bit, 1) { Value = model.IsFolder }
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
		public int Update(PlatformManager_Role model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("update [dbo].[PlatformManager_Role] set ");
			sql.Append("[Title]=@Title,[Content]=@Content,[PagePath]=@PagePath,[sort]=@sort,[MenuText]=@MenuText,[MenuPath]=@MenuPath,[ParentId]=@ParentId,[Delflag]=@Delflag,[CreateDate]=@CreateDate,[IsFolder]=@IsFolder");
			sql.Append(" where [Id]=@Id");
			SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@Title", SqlDbType.NVarChar, 40) { Value = model.Title },
				new SqlParameter("@Content", SqlDbType.VarChar, 300) { Value = model.Content },
				new SqlParameter("@PagePath", SqlDbType.VarChar, 500) { Value = model.PagePath },
				new SqlParameter("@sort", SqlDbType.Int, 4) { Value = model.sort },
				new SqlParameter("@MenuText", SqlDbType.VarChar, 20) { Value = model.MenuText },
				new SqlParameter("@MenuPath", SqlDbType.VarChar, 300) { Value = model.MenuPath },
				new SqlParameter("@ParentId", SqlDbType.Int, 4) { Value = model.ParentId },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@IsFolder", SqlDbType.Bit, 1) { Value = model.IsFolder }
			};
			return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
		}

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public PlatformManager_Role GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[PlatformManager_Role] where [Id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					PlatformManager_Role model = new PlatformManager_Role();
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
		public List<PlatformManager_Role> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[PlatformManager_Role]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<PlatformManager_Role> list = new List<PlatformManager_Role>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					PlatformManager_Role model = new PlatformManager_Role();
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
		public List<PlatformManager_Role> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			StringBuilder sb = new StringBuilder();
			sb.Append("select count(1) from [dbo].[PlatformManager_Role]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[PlatformManager_Role]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<PlatformManager_Role> list = new List<PlatformManager_Role>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					PlatformManager_Role model = new PlatformManager_Role();
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
			sql.Append("select [Id],[Title],[Content],[PagePath],[sort],[MenuText],[MenuPath],[ParentId],[Delflag],[CreateDate],[IsFolder] from [dbo].[PlatformManager_Role]");
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
			sb.Append("select count(1) from [dbo].[PlatformManager_Role]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select [Id],[Title],[Content],[PagePath],[sort],[MenuText],[MenuPath],[ParentId],[Delflag],[CreateDate],[IsFolder],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[PlatformManager_Role]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

		private void ConvertToModel(IDataReader reader, PlatformManager_Role model)
		{
			if (reader["Id"] != DBNull.Value)
				model.Id = Convert.ToInt32(reader["Id"]);
			if (reader["Title"] != DBNull.Value)
				model.Title = reader["Title"].ToString();
			if (reader["Content"] != DBNull.Value)
				model.Content = reader["Content"].ToString();
			if (reader["PagePath"] != DBNull.Value)
				model.PagePath = reader["PagePath"].ToString();
			if (reader["sort"] != DBNull.Value)
				model.sort = Convert.ToInt32(reader["sort"]);
			if (reader["MenuText"] != DBNull.Value)
				model.MenuText = reader["MenuText"].ToString();
			if (reader["MenuPath"] != DBNull.Value)
				model.MenuPath = reader["MenuPath"].ToString();
			if (reader["ParentId"] != DBNull.Value)
				model.ParentId = Convert.ToInt32(reader["ParentId"]);
			if (reader["Delflag"] != DBNull.Value)
				model.Delflag = Convert.ToBoolean(reader["Delflag"]);
			if (reader["CreateDate"] != DBNull.Value)
				model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
			if (reader["IsFolder"] != DBNull.Value)
				model.IsFolder = Convert.ToBoolean(reader["IsFolder"]);
		}

	}
}
