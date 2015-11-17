using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dianda.AP.Model;
using Dianda.AP.Model.Learn.LearnNote;

namespace Dianda.AP.DAL
{
	public class Member_ClassContentNoteDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(Member_ClassContentNote model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("insert into [dbo].[Member_ClassContentNote] ([ClassId],[TrainingId],[AccountId],[UnitContent],[Content],[Delflag])");
			sql.Append(" values (@ClassId,@TrainingId,@AccountId,@UnitContent,@Content,@Delflag)");
			sql.Append(" set @Id=@@IDENTITY");
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = model.ClassId },
				new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = model.TrainingId },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@UnitContent", SqlDbType.Int, 4) { Value = model.UnitContent },
				new SqlParameter("@Content", SqlDbType.VarChar, 8000) { Value = model.Content },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag }
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
		public int Update(Member_ClassContentNote model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("update [dbo].[Member_ClassContentNote] set ");
			sql.Append("[ClassId]=@ClassId,[TrainingId]=@TrainingId,[AccountId]=@AccountId,[UnitContent]=@UnitContent,[Content]=@Content,[Delflag]=@Delflag,[CreateDate]=@CreateDate");
			sql.Append(" where [Id]=@Id");
			SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = model.ClassId },
				new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = model.TrainingId },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@UnitContent", SqlDbType.Int, 4) { Value = model.UnitContent },
				new SqlParameter("@Content", SqlDbType.VarChar, 8000) { Value = model.Content },
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
		public Member_ClassContentNote GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[Member_ClassContentNote] where [Id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					Member_ClassContentNote model = new Member_ClassContentNote();
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
		public List<Member_ClassContentNote> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[Member_ClassContentNote]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<Member_ClassContentNote> list = new List<Member_ClassContentNote>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Member_ClassContentNote model = new Member_ClassContentNote();
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
		public List<Member_ClassContentNote> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			StringBuilder sb = new StringBuilder();
			sb.Append("select count(1) from [dbo].[Member_ClassContentNote]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + " DESC) as [RowNum] from [dbo].[Member_ClassContentNote]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<Member_ClassContentNote> list = new List<Member_ClassContentNote>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					Member_ClassContentNote model = new Member_ClassContentNote();
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
			sql.Append("select [Id],[ClassId],[TrainingId],[AccountId],[UnitContent],[Content],[Delflag],[CreateDate] from [dbo].[Member_ClassContentNote]");
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
			sb.Append("select count(1) from [dbo].[Member_ClassContentNote]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select [Id],[ClassId],[TrainingId],[AccountId],[UnitContent],[Content],[Delflag],[CreateDate],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Member_ClassContentNote]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

		private void ConvertToModel(IDataReader reader, Member_ClassContentNote model)
		{
			if (reader["Id"] != DBNull.Value)
				model.Id = Convert.ToInt32(reader["Id"]);
			if (reader["ClassId"] != DBNull.Value)
				model.ClassId = Convert.ToInt32(reader["ClassId"]);
			if (reader["TrainingId"] != DBNull.Value)
				model.TrainingId = Convert.ToInt32(reader["TrainingId"]);
			if (reader["AccountId"] != DBNull.Value)
				model.AccountId = Convert.ToInt32(reader["AccountId"]);
			if (reader["UnitContent"] != DBNull.Value)
				model.UnitContent = Convert.ToInt32(reader["UnitContent"]);
			if (reader["Content"] != DBNull.Value)
				model.Content = reader["Content"].ToString();
			if (reader["Delflag"] != DBNull.Value)
				model.Delflag = Convert.ToBoolean(reader["Delflag"]);
			if (reader["CreateDate"] != DBNull.Value)
				model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
		}

        /// <summary>
        /// 获取我的笔记的总条数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetMemberClassContentNoteCount(string where)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(1) from [dbo].[Member_ClassContentNote]");
            if (!string.IsNullOrEmpty(where))
                sb.Append(" where " + where);
            return Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
        }

        /// <summary>
        /// 删除[我的笔记]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(Member_ClassContentNote model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Member_ClassContentNote] set delflag = 1 where Id = @Id");
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id } };
            int result = Convert.ToInt32(MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams));
            return result;
        }

        /// <summary>
        /// 根据指定章获取笔记信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<LearnNoteInfo> GetNotesInfoById(int id)
        {
            string sql = @"select A.Id as Id,A.Content as Content,A.CreateDate as CreateDate from Member_ClassContentNote as A,dbo.Course_UnitContent as B,dbo.Course_UnitDetail as C
                         where A.UnitContent = B.Id and B.UnitId = C.Id and (C.Id =@id or C.ParentId =@id) 
                         and A.Delflag = 0 and B.Delflag = 0 and C.Delflag = 0 ";

            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@id", SqlDbType.Int, 4) { Value = id }
			};
            List<LearnNoteInfo> list = new List<LearnNoteInfo>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    LearnNoteInfo model = new LearnNoteInfo();
                    model.Id = Convert.ToInt32(reader["id"]);
                    model.Content = reader["Content"].ToString();
                    model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);

                    list.Add(model);
                }
            }

            return list;
        }
    }
}
