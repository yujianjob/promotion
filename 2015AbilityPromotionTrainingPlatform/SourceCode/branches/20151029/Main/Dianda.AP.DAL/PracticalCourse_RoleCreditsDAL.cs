using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Dianda.AP.DAL
{
	public class PracticalCourse_RoleCreditsDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(PracticalCourse_RoleCredits model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("insert into [dbo].[PracticalCourse_RoleCredits] ([Title],[TraingField],[TraingCategory],[TraingTopic],[RoleId],[Credits],[Delflag],[CreateDate])");
			sql.Append(" values (@Title,@TraingField,@TraingCategory,@TraingTopic,@RoleId,@Credits,@Delflag,@CreateDate)");
			sql.Append(" set @Id=@@IDENTITY");
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
				new SqlParameter("@TraingField", SqlDbType.Int, 4) { Value = model.TraingField },
				new SqlParameter("@TraingCategory", SqlDbType.Int, 4) { Value = model.TraingCategory },
				new SqlParameter("@TraingTopic", SqlDbType.Int, 4) { Value = model.TraingTopic },
				new SqlParameter("@RoleId", SqlDbType.Int, 4) { Value = model.RoleId },
				new SqlParameter("@Credits", SqlDbType.Float, 8) { Value = model.Credits },
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
		public int Update(PracticalCourse_RoleCredits model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("update [dbo].[PracticalCourse_RoleCredits] set ");
			sql.Append("[Title]=@Title,[TraingField]=@TraingField,[TraingCategory]=@TraingCategory,[TraingTopic]=@TraingTopic,[RoleId]=@RoleId,[Credits]=@Credits,[Delflag]=@Delflag,[CreateDate]=@CreateDate");
			sql.Append(" where [Id]=@Id");
			SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
				new SqlParameter("@TraingField", SqlDbType.Int, 4) { Value = model.TraingField },
				new SqlParameter("@TraingCategory", SqlDbType.Int, 4) { Value = model.TraingCategory },
				new SqlParameter("@TraingTopic", SqlDbType.Int, 4) { Value = model.TraingTopic },
				new SqlParameter("@RoleId", SqlDbType.Int, 4) { Value = model.RoleId },
				new SqlParameter("@Credits", SqlDbType.Float, 8) { Value = model.Credits },
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
		public PracticalCourse_RoleCredits GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[PracticalCourse_RoleCredits] where [Id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					PracticalCourse_RoleCredits model = new PracticalCourse_RoleCredits();
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
		public List<PracticalCourse_RoleCredits> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[PracticalCourse_RoleCredits]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<PracticalCourse_RoleCredits> list = new List<PracticalCourse_RoleCredits>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					PracticalCourse_RoleCredits model = new PracticalCourse_RoleCredits();
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
		public List<PracticalCourse_RoleCredits> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			StringBuilder sb = new StringBuilder();
			sb.Append("select count(1) from [dbo].[PracticalCourse_RoleCredits]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[PracticalCourse_RoleCredits]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<PracticalCourse_RoleCredits> list = new List<PracticalCourse_RoleCredits>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					PracticalCourse_RoleCredits model = new PracticalCourse_RoleCredits();
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
			sql.Append("select [Id],[Title],[TraingField],[TraingCategory],[TraingTopic],[RoleId],[Credits],[Delflag],[CreateDate] from [dbo].[PracticalCourse_RoleCredits]");
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
			sb.Append("select count(1) from [dbo].[PracticalCourse_RoleCredits]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select [Id],[Title],[TraingField],[TraingCategory],[TraingTopic],[RoleId],[Credits],[Delflag],[CreateDate],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[PracticalCourse_RoleCredits]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

		private void ConvertToModel(IDataReader reader, PracticalCourse_RoleCredits model)
		{
			if (reader["Id"] != DBNull.Value)
				model.Id = Convert.ToInt32(reader["Id"]);
			if (reader["Title"] != DBNull.Value)
				model.Title = reader["Title"].ToString();
			if (reader["TraingField"] != DBNull.Value)
				model.TraingField = Convert.ToInt32(reader["TraingField"]);
			if (reader["TraingCategory"] != DBNull.Value)
				model.TraingCategory = Convert.ToInt32(reader["TraingCategory"]);
			if (reader["TraingTopic"] != DBNull.Value)
				model.TraingTopic = Convert.ToInt32(reader["TraingTopic"]);
			if (reader["RoleId"] != DBNull.Value)
				model.RoleId = Convert.ToInt32(reader["RoleId"]);
			if (reader["Credits"] != DBNull.Value)
				model.Credits = Convert.ToDouble(reader["Credits"]);
			if (reader["Delflag"] != DBNull.Value)
				model.Delflag = Convert.ToBoolean(reader["Delflag"]);
			if (reader["CreateDate"] != DBNull.Value)
				model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
		}

	}
}
