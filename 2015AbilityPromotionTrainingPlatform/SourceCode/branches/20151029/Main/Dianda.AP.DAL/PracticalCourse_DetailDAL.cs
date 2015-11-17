using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Dianda.AP.DAL
{
	public class PracticalCourse_DetailDAL
	{
		/// <summary>
		/// 新增一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
        public int Add(PracticalCourse_Detail model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into [dbo].[PracticalCourse_Detail] ([Title],[TraingField],[Content],[IsBatch],[OrganId],[AccountId],[PlanId],[People],[Display],[Delflag],[CreateDate],[TraingCategory],[TraingTopic],[NationalCoursId])");
            sql.Append(" values (@Title,@TraingField,@Content,@IsBatch,@OrganId,@AccountId,@PlanId,@People,@Display,@Delflag,@CreateDate,@TraingCategory,@TraingTopic,@NationalCoursId)");
            sql.Append(" set @Id=@@IDENTITY");
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
				new SqlParameter("@TraingField", SqlDbType.Int, 4) { Value = model.TraingField },
				new SqlParameter("@Content", SqlDbType.VarChar, 8000) { Value = model.Content },
				new SqlParameter("@IsBatch", SqlDbType.Bit, 1) { Value = model.IsBatch },
				new SqlParameter("@OrganId", SqlDbType.Int, 4) { Value = model.OrganId },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@PlanId", SqlDbType.Int, 4) { Value = model.PlanId },
				new SqlParameter("@People", SqlDbType.Int, 4) { Value = model.People },
				new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = model.Display },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@TraingCategory", SqlDbType.Int, 4) { Value = model.TraingCategory },
				new SqlParameter("@TraingTopic", SqlDbType.Int, 4) { Value = model.TraingTopic },
				new SqlParameter("@NationalCoursId", SqlDbType.Int, 4) { Value = model.NationalCoursId }
			};
            int result = Convert.ToInt32(MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams));
            model.Id = Convert.ToInt32(cmdParams[0].Value);
            return model.Id;
        }

        //public int Add(MPracticalCourseModel model)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("insert into [dbo].[PracticalCourse_Detail] ([Title],[TraingField],[TraingCategory],[TraingTopic],[Content],[IsBatch],[OrganId],[AccountId],[PlanId],[People],[Display],[Delflag],[CreateDate])");
        //    sql.Append(" values (@Title,@TraingField,@TraingCategory,@TraingTopic,@Content,@IsBatch,@OrganId,@AccountId,@PlanId,@People,@Display,@Delflag,@CreateDate)");
        //    sql.Append(" set @Id=@@IDENTITY");
        //    SqlParameter[] cmdParams = new SqlParameter[]{
        //        new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
        //        new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
        //        new SqlParameter("@TraingField", SqlDbType.Int, 4) { Value = model.TraingField },
        //        new SqlParameter("@TraingCategory", SqlDbType.Int, 4) { Value = model.TraingCategory },
        //        new SqlParameter("@TraingTopic", SqlDbType.Int, 4) { Value = model.TraingTopic },
        //        new SqlParameter("@Content", SqlDbType.VarChar, 8000) { Value = model.Content },
        //        new SqlParameter("@IsBatch", SqlDbType.Bit, 1) { Value = model.IsBatch },
        //        new SqlParameter("@OrganId", SqlDbType.Int, 4) { Value = model.OrganId },
        //        new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
        //        new SqlParameter("@PlanId", SqlDbType.Int, 4) { Value = model.PlanId },
        //        new SqlParameter("@People", SqlDbType.Int, 4) { Value = model.People },
        //        new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = model.Display },
        //        new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
        //        new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate }
        //    };
        //    int result = Convert.ToInt32(MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams));
        //    model.Id = Convert.ToInt32(cmdParams[0].Value);
        //    return model.Id;
        //}

        //public int Add(MPracticalCourseModels model)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("insert into [dbo].[PracticalCourse_Detail] ([Title],[TraingField],[TraingCategory],[TraingTopic],[Content],[IsBatch],[OrganId],[AccountId],[PlanId],[People],[Display],[Delflag],[CreateDate])");
        //    sql.Append(" values (@Title,@TraingField,@TraingCategory,@TraingTopic,@Content,@IsBatch,@OrganId,@AccountId,@PlanId,@People,@Display,@Delflag,@CreateDate)");
        //    sql.Append(" set @Id=@@IDENTITY");
        //    SqlParameter[] cmdParams = new SqlParameter[]{
        //        new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
        //        new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
        //        new SqlParameter("@TraingField", SqlDbType.Int, 4) { Value = model.TraingField },
        //        new SqlParameter("@TraingCategory", SqlDbType.Int, 4) { Value = model.TraingCategory },
        //        new SqlParameter("@TraingTopic", SqlDbType.Int, 4) { Value = model.TraingTopic },
        //        new SqlParameter("@Content", SqlDbType.VarChar, 8000) { Value = model.Content },
        //        new SqlParameter("@IsBatch", SqlDbType.Bit, 1) { Value = model.IsBatch },
        //        new SqlParameter("@OrganId", SqlDbType.Int, 4) { Value = model.OrganId },
        //        new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
        //        new SqlParameter("@PlanId", SqlDbType.Int, 4) { Value = model.PlanId },
        //        new SqlParameter("@People", SqlDbType.Int, 4) { Value = model.People },
        //        new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = model.Display },
        //        new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
        //        new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate }
        //    };
        //    int result = Convert.ToInt32(MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams));
        //    model.Id = Convert.ToInt32(cmdParams[0].Value);
        //    return model.Id;
        //}
		/// <summary>
		/// 更新一条记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
        public int Update(PracticalCourse_Detail model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[PracticalCourse_Detail] set ");
            sql.Append("[Title]=@Title,[TraingField]=@TraingField,[Content]=@Content,[IsBatch]=@IsBatch,[OrganId]=@OrganId,[AccountId]=@AccountId,[PlanId]=@PlanId,[People]=@People,[Display]=@Display,[Delflag]=@Delflag,[CreateDate]=@CreateDate,[TraingCategory]=@TraingCategory,[TraingTopic]=@TraingTopic,[NationalCoursId]=@NationalCoursId");
            sql.Append(" where [Id]=@Id");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@Title", SqlDbType.VarChar, 200) { Value = model.Title },
				new SqlParameter("@TraingField", SqlDbType.Int, 4) { Value = model.TraingField },
				new SqlParameter("@Content", SqlDbType.VarChar, 8000) { Value = model.Content },
				new SqlParameter("@IsBatch", SqlDbType.Bit, 1) { Value = model.IsBatch },
				new SqlParameter("@OrganId", SqlDbType.Int, 4) { Value = model.OrganId },
				new SqlParameter("@AccountId", SqlDbType.Int, 4) { Value = model.AccountId },
				new SqlParameter("@PlanId", SqlDbType.Int, 4) { Value = model.PlanId },
				new SqlParameter("@People", SqlDbType.Int, 4) { Value = model.People },
				new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = model.Display },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = model.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = model.CreateDate },
				new SqlParameter("@TraingCategory", SqlDbType.Int, 4) { Value = model.TraingCategory },
				new SqlParameter("@TraingTopic", SqlDbType.Int, 4) { Value = model.TraingTopic },
				new SqlParameter("@NationalCoursId", SqlDbType.Int, 4) { Value = model.NationalCoursId }
			};
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
        }

		/// <summary>
		/// 取得一条记录
		/// </summary>
		/// <param name="id"></param>
		/// <param name="where"></param>
		/// <returns></returns>
		public PracticalCourse_Detail GetModel(int id, string where)
		{
			string sql = "select * from [dbo].[PracticalCourse_Detail] where [Id]=@Id";
			if (!string.IsNullOrEmpty(where))
				sql += " and " + where;
			SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
			{
				if (reader.Read())
				{
					PracticalCourse_Detail model = new PracticalCourse_Detail();
					ConvertToModel(reader, model);
					return model;
				}
				else
				{
					return null;
				}
			}
		}


        public PracticalCourseEdit GetPracticeModel(int MemberPracticeId)
        {
            string sql = "select pcd.*,mpc.RoleId,mpc.Id as MemberPCourseid,mpc.Status,mpc.ApplyRemark from  Member_PracticalCourse mpc left join [PracticalCourse_Detail] pcd on mpc.PracticalCourseId=pcd.Id where mpc.Id=@Id";
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = MemberPracticeId }
			};
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                if (reader.Read())
                {
                    PracticalCourseEdit model = new PracticalCourseEdit();
                    ConvertToModel2(reader, model);
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
		public List<PracticalCourse_Detail> GetList(string where, string orderBy)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from [dbo].[PracticalCourse_Detail]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			if (!string.IsNullOrEmpty(orderBy))
				sql.Append(" order by " + orderBy);
			List<PracticalCourse_Detail> list = new List<PracticalCourse_Detail>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					PracticalCourse_Detail model = new PracticalCourse_Detail();
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
		public List<PracticalCourse_Detail> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
		{
			if (string.IsNullOrEmpty(orderBy))
				throw new ArgumentNullException();
			StringBuilder sb = new StringBuilder();
			sb.Append("select count(1) from [dbo].[PracticalCourse_Detail]");
			if (!string.IsNullOrEmpty(where))
				sb.Append(" where " + where);
			recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
			int start = (pageIndex - 1) * pageSize + 1;
			int end = pageIndex * pageSize;
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[PracticalCourse_Detail]");
			if (!string.IsNullOrEmpty(where))
				sql.Append(" where " + where);
			sql.Append(") as T where [RowNum] between " + start + " and " + end);
			List<PracticalCourse_Detail> list = new List<PracticalCourse_Detail>();
			using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
			{
				while (reader.Read())
				{
					PracticalCourse_Detail model = new PracticalCourse_Detail();
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
            sql.Append("select [Id],[Title],[TraingField],[Content],[IsBatch],[OrganId],[AccountId],[PlanId],[People],[Display],[Delflag],[CreateDate],[TraingCategory],[TraingTopic],[NationalCoursId] from [dbo].[PracticalCourse_Detail]");
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
            sb.Append("select count(1) from [dbo].[PracticalCourse_Detail]");
            if (!string.IsNullOrEmpty(where))
                sb.Append(" where " + where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select [Id],[Title],[TraingField],[Content],[IsBatch],[OrganId],[AccountId],[PlanId],[People],[Display],[Delflag],[CreateDate],[TraingCategory],[TraingTopic],[NationalCoursId],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[PracticalCourse_Detail]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(") as T where [RowNum] between " + start + " and " + end);
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
		}

        private void ConvertToModel(IDataReader reader, PracticalCourse_Detail model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["Title"] != DBNull.Value)
                model.Title = reader["Title"].ToString();
            if (reader["TraingField"] != DBNull.Value)
                model.TraingField = Convert.ToInt32(reader["TraingField"]);
            if (reader["Content"] != DBNull.Value)
                model.Content = reader["Content"].ToString();
            if (reader["IsBatch"] != DBNull.Value)
                model.IsBatch = Convert.ToBoolean(reader["IsBatch"]);
            if (reader["OrganId"] != DBNull.Value)
                model.OrganId = Convert.ToInt32(reader["OrganId"]);
            if (reader["AccountId"] != DBNull.Value)
                model.AccountId = Convert.ToInt32(reader["AccountId"]);
            if (reader["PlanId"] != DBNull.Value)
                model.PlanId = Convert.ToInt32(reader["PlanId"]);
            if (reader["People"] != DBNull.Value)
                model.People = Convert.ToInt32(reader["People"]);
            if (reader["Display"] != DBNull.Value)
                model.Display = Convert.ToBoolean(reader["Display"]);
            if (reader["Delflag"] != DBNull.Value)
                model.Delflag = Convert.ToBoolean(reader["Delflag"]);
            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
            if (reader["TraingCategory"] != DBNull.Value)
                model.TraingCategory = Convert.ToInt32(reader["TraingCategory"]);
            if (reader["TraingTopic"] != DBNull.Value)
                model.TraingTopic = Convert.ToInt32(reader["TraingTopic"]);
            if (reader["NationalCoursId"] != DBNull.Value)
                model.NationalCoursId = Convert.ToInt32(reader["NationalCoursId"]);
        }

        private void ConvertToModel2(IDataReader reader, PracticalCourseEdit model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["Title"] != DBNull.Value)
                model.Title = reader["Title"].ToString();
            if (reader["TraingField"] != DBNull.Value)
                model.TraingField = Convert.ToInt32(reader["TraingField"]);
            if (reader["Content"] != DBNull.Value)
                model.Content = reader["Content"].ToString();
            if (reader["IsBatch"] != DBNull.Value)
                model.IsBatch = Convert.ToBoolean(reader["IsBatch"]);
            if (reader["OrganId"] != DBNull.Value)
                model.OrganId = Convert.ToInt32(reader["OrganId"]);
            if (reader["AccountId"] != DBNull.Value)
                model.AccountId = Convert.ToInt32(reader["AccountId"]);
            if (reader["PlanId"] != DBNull.Value)
                model.PlanId = Convert.ToInt32(reader["PlanId"]);
            if (reader["People"] != DBNull.Value)
                model.People = Convert.ToInt32(reader["People"]);
            if (reader["Display"] != DBNull.Value)
                model.Display = Convert.ToBoolean(reader["Display"]);
            if (reader["Delflag"] != DBNull.Value)
                model.Delflag = Convert.ToBoolean(reader["Delflag"]);
            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
            if (reader["TraingCategory"] != DBNull.Value)
                model.TraingCategory = Convert.ToInt32(reader["TraingCategory"]);
            if (reader["TraingTopic"] != DBNull.Value)
                model.TraingTopic = Convert.ToInt32(reader["TraingTopic"]);
            if (reader["RoleId"] != DBNull.Value)
                model.Role = Convert.ToInt32(reader["RoleId"]);
            if (reader["memberPCourseid"] != DBNull.Value)
                model.MemberPCourseid = Convert.ToInt32(reader["memberPCourseid"]);
            if (reader["NationalCoursId"] != DBNull.Value)
                model.NationalCoursId = Convert.ToInt32(reader["NationalCoursId"]);
            if (reader["Status"] != DBNull.Value)
                model.Status = Convert.ToInt32(reader["Status"]);
            if (reader["ApplyRemark"] != DBNull.Value)
                model.ApplyRemark = reader["ApplyRemark"].ToString();

        }
	}
}
