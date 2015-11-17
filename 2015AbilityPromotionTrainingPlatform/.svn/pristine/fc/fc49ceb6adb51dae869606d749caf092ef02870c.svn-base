using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dianda.AP.Model;

namespace Dianda.AP.DAL
{
	public class Member_ClassContentTimeRecordDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(Member_ClassContentTimeRecord model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("insert into [dbo].[Member_ClassContentTimeRecord] ([ClassId],[TrainingId],[AccountId],[StartTime],[EndTime],[Delflag],[CreateDate],[UnitContent])");
			sql.Append(" values (@ClassId,@TrainingId,@AccountId,@StartTime,@EndTime,@Delflag,@CreateDate,@UnitContent)");
			sql.Append(" set @Id=@@IDENTITY");
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = model.ClassId },
				new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = model.TrainingId },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@StartTime", SqlDbType.DateTime, 8) { Value = model.StartTime },
				new SqlParameter("@EndTime", SqlDbType.DateTime, 8) { Value = model.EndTime },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@UnitContent", SqlDbType.Int, 4) { Value = model.UnitContent }
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
		public int Update(Member_ClassContentTimeRecord model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("update [dbo].[Member_ClassContentTimeRecord] set ");
			sql.Append("[ClassId]=@ClassId,[TrainingId]=@TrainingId,[AccountId]=@AccountId,[StartTime]=@StartTime,[EndTime]=@EndTime,[Delflag]=@Delflag,[CreateDate]=@CreateDate,[UnitContent]=@UnitContent");
			sql.Append(" where [Id]=@Id");
			SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = model.ClassId },
				new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = model.TrainingId },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@StartTime", SqlDbType.DateTime, 8) { Value = model.StartTime },
				new SqlParameter("@EndTime", SqlDbType.DateTime, 8) { Value = model.EndTime },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@UnitContent", SqlDbType.Int, 4) { Value = model.UnitContent }
			};
			return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
		}

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public Member_ClassContentTimeRecord GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[Member_ClassContentTimeRecord] where [Id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					Member_ClassContentTimeRecord model = new Member_ClassContentTimeRecord();
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
		public List<Member_ClassContentTimeRecord> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[Member_ClassContentTimeRecord]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<Member_ClassContentTimeRecord> list = new List<Member_ClassContentTimeRecord>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Member_ClassContentTimeRecord model = new Member_ClassContentTimeRecord();
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
		public List<Member_ClassContentTimeRecord> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			StringBuilder sb = new StringBuilder();
			sb.Append("select count(1) from [dbo].[Member_ClassContentTimeRecord]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Member_ClassContentTimeRecord]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<Member_ClassContentTimeRecord> list = new List<Member_ClassContentTimeRecord>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Member_ClassContentTimeRecord model = new Member_ClassContentTimeRecord();
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
			sql.Append("select [Id],[ClassId],[TrainingId],[AccountId],[StartTime],[EndTime],[Delflag],[CreateDate],[UnitContent] from [dbo].[Member_ClassContentTimeRecord]");
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
			sb.Append("select count(1) from [dbo].[Member_ClassContentTimeRecord]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select [Id],[ClassId],[TrainingId],[AccountId],[StartTime],[EndTime],[Delflag],[CreateDate],[UnitContent],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Member_ClassContentTimeRecord]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

		private void ConvertToModel(IDataReader reader, Member_ClassContentTimeRecord model)
		{
			if (reader["Id"] != DBNull.Value)
				model.Id = Convert.ToInt32(reader["Id"]);
			if (reader["ClassId"] != DBNull.Value)
				model.ClassId = Convert.ToInt32(reader["ClassId"]);
			if (reader["TrainingId"] != DBNull.Value)
				model.TrainingId = Convert.ToInt32(reader["TrainingId"]);
			if (reader["AccountId"] != DBNull.Value)
				model.AccountId = Convert.ToInt32(reader["AccountId"]);
			if (reader["StartTime"] != DBNull.Value)
				model.StartTime = Convert.ToDateTime(reader["StartTime"]);
			if (reader["EndTime"] != DBNull.Value)
				model.EndTime = Convert.ToDateTime(reader["EndTime"]);
			if (reader["Delflag"] != DBNull.Value)
				model.Delflag = Convert.ToBoolean(reader["Delflag"]);
			if (reader["CreateDate"] != DBNull.Value)
				model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
			if (reader["UnitContent"] != DBNull.Value)
				model.UnitContent = Convert.ToInt32(reader["UnitContent"]);
		}

	}
}
