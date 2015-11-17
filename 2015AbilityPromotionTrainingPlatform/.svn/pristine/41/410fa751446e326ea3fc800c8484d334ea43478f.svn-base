using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dianda.AP.Model;

namespace Dianda.AP.DAL
{
	public class Organ_StudyRegionDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(Organ_StudyRegion model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("insert into [dbo].[Organ_StudyRegion] ([Title],[ParentId],[PartitionId],[Delflag],[CreateDate],[OutSourceId])");
			sql.Append(" values (@Title,@ParentId,@PartitionId,@Delflag,@CreateDate,@OutSourceId)");
			sql.Append(" set @Id=@@IDENTITY");
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
				new SqlParameter("@ParentId", SqlDbType.Int, 4) { Value = model.ParentId },
				new SqlParameter("@PartitionId", SqlDbType.Int, 4) { Value = model.PartitionId },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@OutSourceId", SqlDbType.Int, 4) { Value = model.OutSourceId }
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
		public int Update(Organ_StudyRegion model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("update [dbo].[Organ_StudyRegion] set ");
			sql.Append("[Title]=@Title,[ParentId]=@ParentId,[PartitionId]=@PartitionId,[Delflag]=@Delflag,[CreateDate]=@CreateDate,[OutSourceId]=@OutSourceId");
			sql.Append(" where [Id]=@Id");
			SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
				new SqlParameter("@ParentId", SqlDbType.Int, 4) { Value = model.ParentId },
				new SqlParameter("@PartitionId", SqlDbType.Int, 4) { Value = model.PartitionId },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@OutSourceId", SqlDbType.Int, 4) { Value = model.OutSourceId }
			};
			return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
		}

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public Organ_StudyRegion GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[Organ_StudyRegion] where [Id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					Organ_StudyRegion model = new Organ_StudyRegion();
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
			sql.Append("select count(1) from [dbo].[Organ_StudyRegion]");
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
		public List<Organ_StudyRegion> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[Organ_StudyRegion]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<Organ_StudyRegion> list = new List<Organ_StudyRegion>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Organ_StudyRegion model = new Organ_StudyRegion();
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
		public List<Organ_StudyRegion> GetList(int pageSize, int pageIndex, string where, string orderBy)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Organ_StudyRegion]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<Organ_StudyRegion> list = new List<Organ_StudyRegion>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Organ_StudyRegion model = new Organ_StudyRegion();
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
			sql.Append("select [Id],[Title],[ParentId],[PartitionId],[Delflag],[CreateDate],[OutSourceId] from [dbo].[Organ_StudyRegion]");
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
			sql.Append("select * from (select [Id],[Title],[ParentId],[PartitionId],[Delflag],[CreateDate],[OutSourceId],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Organ_StudyRegion]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

		private void ConvertToModel(IDataReader reader, Organ_StudyRegion model)
		{
			if (reader["Id"] != DBNull.Value)
				model.Id = Convert.ToInt32(reader["Id"]);
			if (reader["Title"] != DBNull.Value)
				model.Title = reader["Title"].ToString();
			if (reader["ParentId"] != DBNull.Value)
				model.ParentId = Convert.ToInt32(reader["ParentId"]);
			if (reader["PartitionId"] != DBNull.Value)
				model.PartitionId = Convert.ToInt32(reader["PartitionId"]);
			if (reader["Delflag"] != DBNull.Value)
				model.Delflag = Convert.ToBoolean(reader["Delflag"]);
			if (reader["CreateDate"] != DBNull.Value)
				model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
			if (reader["OutSourceId"] != DBNull.Value)
				model.OutSourceId = Convert.ToInt32(reader["OutSourceId"]);
		}

	}
}
