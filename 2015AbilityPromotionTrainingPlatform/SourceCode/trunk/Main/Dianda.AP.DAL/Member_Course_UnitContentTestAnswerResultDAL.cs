using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dianda.AP.Model;

namespace Dianda.AP.DAL
{
	public class Member_Course_UnitContentTestAnswerResultDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(Member_Course_UnitContentTestAnswerResult model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("insert into [dbo].[Member_Course_UnitContentTestAnswerResult] ([Verson],[UnitContent],[ClassId],[Score],[QuestionCnt],[RightAnswer],[WrongAnswer],[Result],[AccountId],[Delflag],[CreateDate])");
			sql.Append(" values (@Verson,@UnitContent,@ClassId,@Score,@QuestionCnt,@RightAnswer,@WrongAnswer,@Result,@AccountId,@Delflag,@CreateDate)");
			sql.Append(" set @Id=@@IDENTITY");
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@Verson", SqlDbType.VarChar, 200) { Value = model.Verson },
				new SqlParameter("@UnitContent", SqlDbType.Int, 4) { Value = model.UnitContent },
				new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = model.ClassId },
				new SqlParameter("@Score", SqlDbType.Float, 8) { Value = model.Score },
				new SqlParameter("@QuestionCnt", SqlDbType.Int, 4) { Value = model.QuestionCnt },
				new SqlParameter("@RightAnswer", SqlDbType.Int, 4) { Value = model.RightAnswer },
				new SqlParameter("@WrongAnswer", SqlDbType.Int, 4) { Value = model.WrongAnswer },
				new SqlParameter("@Result", SqlDbType.Bit, 1) { Value = model.Result },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
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
		public int Update(Member_Course_UnitContentTestAnswerResult model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("update [dbo].[Member_Course_UnitContentTestAnswerResult] set ");
			sql.Append("[Verson]=@Verson,[UnitContent]=@UnitContent,[ClassId]=@ClassId,[Score]=@Score,[QuestionCnt]=@QuestionCnt,[RightAnswer]=@RightAnswer,[WrongAnswer]=@WrongAnswer,[Result]=@Result,[AccountId]=@AccountId,[Delflag]=@Delflag,[CreateDate]=@CreateDate");
			sql.Append(" where [Id]=@Id");
			SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@Verson", SqlDbType.VarChar, 200) { Value = model.Verson },
				new SqlParameter("@UnitContent", SqlDbType.Int, 4) { Value = model.UnitContent },
				new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = model.ClassId },
				new SqlParameter("@Score", SqlDbType.Float, 8) { Value = model.Score },
				new SqlParameter("@QuestionCnt", SqlDbType.Int, 4) { Value = model.QuestionCnt },
				new SqlParameter("@RightAnswer", SqlDbType.Int, 4) { Value = model.RightAnswer },
				new SqlParameter("@WrongAnswer", SqlDbType.Int, 4) { Value = model.WrongAnswer },
				new SqlParameter("@Result", SqlDbType.Bit, 1) { Value = model.Result },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
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
		public Member_Course_UnitContentTestAnswerResult GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[Member_Course_UnitContentTestAnswerResult] where [Id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					Member_Course_UnitContentTestAnswerResult model = new Member_Course_UnitContentTestAnswerResult();
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
		public List<Member_Course_UnitContentTestAnswerResult> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[Member_Course_UnitContentTestAnswerResult]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<Member_Course_UnitContentTestAnswerResult> list = new List<Member_Course_UnitContentTestAnswerResult>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Member_Course_UnitContentTestAnswerResult model = new Member_Course_UnitContentTestAnswerResult();
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
		public List<Member_Course_UnitContentTestAnswerResult> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			StringBuilder sb = new StringBuilder();
			sb.Append("select count(1) from [dbo].[Member_Course_UnitContentTestAnswerResult]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Member_Course_UnitContentTestAnswerResult]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<Member_Course_UnitContentTestAnswerResult> list = new List<Member_Course_UnitContentTestAnswerResult>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Member_Course_UnitContentTestAnswerResult model = new Member_Course_UnitContentTestAnswerResult();
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
			sql.Append("select [Id],[Verson],[UnitContent],[ClassId],[Score],[QuestionCnt],[RightAnswer],[WrongAnswer],[Result],[AccountId],[Delflag],[CreateDate] from [dbo].[Member_Course_UnitContentTestAnswerResult]");
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
			sb.Append("select count(1) from [dbo].[Member_Course_UnitContentTestAnswerResult]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select [Id],[Verson],[UnitContent],[ClassId],[Score],[QuestionCnt],[RightAnswer],[WrongAnswer],[Result],[AccountId],[Delflag],[CreateDate],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Member_Course_UnitContentTestAnswerResult]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

		private void ConvertToModel(IDataReader reader, Member_Course_UnitContentTestAnswerResult model)
		{
			if (reader["Id"] != DBNull.Value)
				model.Id = Convert.ToInt32(reader["Id"]);
			if (reader["Verson"] != DBNull.Value)
				model.Verson = reader["Verson"].ToString();
			if (reader["UnitContent"] != DBNull.Value)
				model.UnitContent = Convert.ToInt32(reader["UnitContent"]);
			if (reader["ClassId"] != DBNull.Value)
				model.ClassId = Convert.ToInt32(reader["ClassId"]);
			if (reader["Score"] != DBNull.Value)
				model.Score = Convert.ToDouble(reader["Score"]);
			if (reader["QuestionCnt"] != DBNull.Value)
				model.QuestionCnt = Convert.ToInt32(reader["QuestionCnt"]);
			if (reader["RightAnswer"] != DBNull.Value)
				model.RightAnswer = Convert.ToInt32(reader["RightAnswer"]);
			if (reader["WrongAnswer"] != DBNull.Value)
				model.WrongAnswer = Convert.ToInt32(reader["WrongAnswer"]);
			if (reader["Result"] != DBNull.Value)
				model.Result = Convert.ToBoolean(reader["Result"]);
			if (reader["AccountId"] != DBNull.Value)
				model.AccountId = Convert.ToInt32(reader["AccountId"]);
			if (reader["Delflag"] != DBNull.Value)
				model.Delflag = Convert.ToBoolean(reader["Delflag"]);
			if (reader["CreateDate"] != DBNull.Value)
				model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
		}

	}
}
