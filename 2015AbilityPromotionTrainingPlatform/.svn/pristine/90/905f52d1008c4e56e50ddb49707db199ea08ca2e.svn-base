using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Dianda.AP.DAL
{
	public class PracticalCourse_AttachmentDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Add(PracticalCourse_Attachment model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("insert into [dbo].[PracticalCourse_Attachment] ([PracticalCourseId],[Title],[Link],[Sort],[Display],[Delflag],[CreateDate])");
			sql.Append(" values (@PracticalCourseId,@Title,@Link,@Sort,@Display,@Delflag,@CreateDate)");
			sql.Append(" set @Id=@@IDENTITY");
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@PracticalCourseId", SqlDbType.Int, 4) { Value = model.PracticalCourseId },
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
		public int Update(PracticalCourse_Attachment model)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("update [dbo].[PracticalCourse_Attachment] set ");
			sql.Append("[PracticalCourseId]=@PracticalCourseId,[Title]=@Title,[Link]=@Link,[Sort]=@Sort,[Display]=@Display,[Delflag]=@Delflag,[CreateDate]=@CreateDate");
			sql.Append(" where [Id]=@Id");
			SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@PracticalCourseId", SqlDbType.Int, 4) { Value = model.PracticalCourseId },
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
		public PracticalCourse_Attachment GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[PracticalCourse_Attachment] where [Id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					PracticalCourse_Attachment model = new PracticalCourse_Attachment();
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
		public List<PracticalCourse_Attachment> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[PracticalCourse_Attachment]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<PracticalCourse_Attachment> list = new List<PracticalCourse_Attachment>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					PracticalCourse_Attachment model = new PracticalCourse_Attachment();
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
		public List<PracticalCourse_Attachment> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			StringBuilder sb = new StringBuilder();
			sb.Append("select count(1) from [dbo].[PracticalCourse_Attachment]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[PracticalCourse_Attachment]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<PracticalCourse_Attachment> list = new List<PracticalCourse_Attachment>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					PracticalCourse_Attachment model = new PracticalCourse_Attachment();
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
			sql.Append("select [Id],[PracticalCourseId],[Title],[Link],[Sort],[Display],[Delflag],[CreateDate] from [dbo].[PracticalCourse_Attachment]");
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
			sb.Append("select count(1) from [dbo].[PracticalCourse_Attachment]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select [Id],[PracticalCourseId],[Title],[Link],[Sort],[Display],[Delflag],[CreateDate],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[PracticalCourse_Attachment]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

		private void ConvertToModel(IDataReader reader, PracticalCourse_Attachment model)
		{
			if (reader["Id"] != DBNull.Value)
				model.Id = Convert.ToInt32(reader["Id"]);
			if (reader["PracticalCourseId"] != DBNull.Value)
				model.PracticalCourseId = Convert.ToInt32(reader["PracticalCourseId"]);
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




        /// <summary>
        /// 批量更新课程附件
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public void BatchAttach(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                switch (row.RowState)
                {
                    case DataRowState.Added:
                        sql.Append("insert into [dbo].[PracticalCourse_Attachment] ([PracticalCourseId],[Title],[Link],[Sort],[Display],[Delflag],[CreateDate]) values "
                            + "(" + row["PracticalCourseId"] + ","
                            + "'" + row["Title"] + "',"
                            + "'" + row["Link"] + "',"
                            + row["Sort"] + ","
                            + "'" + row["Display"] + "',"
                            + "'" + row["Delflag"] + "',"
                            + "'" + row["CreateDate"] + "') ");
                        break;
                    case DataRowState.Deleted:
                        sql.Append("update [dbo].[PracticalCourse_Attachment] set Delflag=1 where [Id]=" + row["Id", DataRowVersion.Original] + " ");
                        break;
                }
            }
            if (!string.IsNullOrEmpty(sql.ToString()))
            MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString());
        }

       
	}
}
