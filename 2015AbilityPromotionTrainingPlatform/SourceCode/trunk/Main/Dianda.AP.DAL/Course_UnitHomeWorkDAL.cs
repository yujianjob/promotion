using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dianda.AP.Model;

namespace Dianda.AP.DAL
{
	public class Course_UnitHomeWorkDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(Course_UnitHomeWork model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("insert into [dbo].[Course_UnitHomeWork] ([UnitContent],[ClassId],[Content],[AccountId],[AttList],[Display],[Delflag],[CreateDate],[Score],[ScoreCreater])");
			sql.Append(" values (@UnitContent,@ClassId,@Content,@AccountId,@AttList,@Display,@Delflag,@CreateDate,@Score,@ScoreCreater)");
			sql.Append(" set @Id=@@IDENTITY");
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@UnitContent", SqlDbType.Int, 4) { Value = model.UnitContent },
				new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = model.ClassId },
				new SqlParameter("@Content", SqlDbType.VarChar, 2000) { Value = model.Content },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@AttList", SqlDbType.VarChar, 2000) { Value = model.AttList },
				new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = model.Display },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@Score", SqlDbType.Float, 8) { Value = model.Score },
				new SqlParameter("@ScoreCreater", SqlDbType.Int, 4) { Value = model.ScoreCreater }
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
		public int Update(Course_UnitHomeWork model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("update [dbo].[Course_UnitHomeWork] set ");
			sql.Append("[UnitContent]=@UnitContent,[ClassId]=@ClassId,[Content]=@Content,[AccountId]=@AccountId,[AttList]=@AttList,[Display]=@Display,[Delflag]=@Delflag,[CreateDate]=@CreateDate,[Score]=@Score,[ScoreCreater]=@ScoreCreater");
			sql.Append(" where [Id]=@Id");
			SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@UnitContent", SqlDbType.Int, 4) { Value = model.UnitContent },
				new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = model.ClassId },
				new SqlParameter("@Content", SqlDbType.VarChar, 2000) { Value = model.Content },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@AttList", SqlDbType.VarChar, 2000) { Value = model.AttList },
				new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = model.Display },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@Score", SqlDbType.Float, 8) { Value = model.Score },
				new SqlParameter("@ScoreCreater", SqlDbType.Int, 4) { Value = model.ScoreCreater }
			};
			return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
		}

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public Course_UnitHomeWork GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[Course_UnitHomeWork] where [Id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					Course_UnitHomeWork model = new Course_UnitHomeWork();
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
		public List<Course_UnitHomeWork> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[Course_UnitHomeWork]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<Course_UnitHomeWork> list = new List<Course_UnitHomeWork>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Course_UnitHomeWork model = new Course_UnitHomeWork();
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
		public List<Course_UnitHomeWork> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			StringBuilder sb = new StringBuilder();
			sb.Append("select count(1) from [dbo].[Course_UnitHomeWork]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Course_UnitHomeWork]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<Course_UnitHomeWork> list = new List<Course_UnitHomeWork>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Course_UnitHomeWork model = new Course_UnitHomeWork();
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
			sql.Append("select [Id],[UnitContent],[ClassId],[Content],[AccountId],[AttList],[Display],[Delflag],[CreateDate],[Score],[ScoreCreater] from [dbo].[Course_UnitHomeWork]");
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
			sb.Append("select count(1) from [dbo].[Course_UnitHomeWork]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select [Id],[UnitContent],[ClassId],[Content],[AccountId],[AttList],[Display],[Delflag],[CreateDate],[Score],[ScoreCreater],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Course_UnitHomeWork]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

		private void ConvertToModel(IDataReader reader, Course_UnitHomeWork model)
		{
			if (reader["Id"] != DBNull.Value)
				model.Id = Convert.ToInt32(reader["Id"]);
			if (reader["UnitContent"] != DBNull.Value)
				model.UnitContent = Convert.ToInt32(reader["UnitContent"]);
			if (reader["ClassId"] != DBNull.Value)
				model.ClassId = Convert.ToInt32(reader["ClassId"]);
			if (reader["Content"] != DBNull.Value)
				model.Content = reader["Content"].ToString();
			if (reader["AccountId"] != DBNull.Value)
				model.AccountId = Convert.ToInt32(reader["AccountId"]);
			if (reader["AttList"] != DBNull.Value)
				model.AttList = reader["AttList"].ToString();
			if (reader["Display"] != DBNull.Value)
				model.Display = Convert.ToBoolean(reader["Display"]);
			if (reader["Delflag"] != DBNull.Value)
				model.Delflag = Convert.ToBoolean(reader["Delflag"]);
			if (reader["CreateDate"] != DBNull.Value)
				model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
			if (reader["Score"] != DBNull.Value)
				model.Score = Convert.ToDouble(reader["Score"]);
			if (reader["ScoreCreater"] != DBNull.Value)
				model.ScoreCreater = Convert.ToInt32(reader["ScoreCreater"]);
		}

	}
}
