using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dianda.AP.Model;
using Dianda.AP.Model.Course.CourseCreate;

namespace Dianda.AP.DAL
{
    public class Course_UnitTestDAL
    {
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Course_UnitTest model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into [dbo].[Course_UnitTest] ([TrainingId],[Verson],[Content],[QTtype],[Question],[Answer],[Credit],[Display],[Delflag],[CreateDate])");
            sql.Append(" values (@TrainingId,@Verson,@Content,@QTtype,@Question,@Answer,@Credit,@Display,@Delflag,@CreateDate)");
            sql.Append(" set @Id=@@IDENTITY");
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = model.TrainingId },
				new SqlParameter("@Verson", SqlDbType.VarChar, 20) { Value = model.Verson },
				new SqlParameter("@Content", SqlDbType.VarChar, 2000) { Value = model.Content },
				new SqlParameter("@QTtype", SqlDbType.Int, 4) { Value = model.QTtype },
				new SqlParameter("@Question", SqlDbType.VarChar, 8000) { Value = model.Question },
				new SqlParameter("@Answer", SqlDbType.VarChar, 100) { Value = model.Answer },
				new SqlParameter("@Credit", SqlDbType.Float, 8) { Value = model.Credit },
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
        public int Update(Course_UnitTest model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Course_UnitTest] set ");
            sql.Append("[TrainingId]=@TrainingId,[Verson]=@Verson,[Content]=@Content,[QTtype]=@QTtype,[Question]=@Question,[Answer]=@Answer,[Credit]=@Credit,[Display]=@Display,[Delflag]=@Delflag,[CreateDate]=@CreateDate");
            sql.Append(" where [Id]=@Id");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = model.TrainingId },
				new SqlParameter("@Verson", SqlDbType.VarChar, 20) { Value = model.Verson },
				new SqlParameter("@Content", SqlDbType.VarChar, 2000) { Value = model.Content },
				new SqlParameter("@QTtype", SqlDbType.Int, 4) { Value = model.QTtype },
				new SqlParameter("@Question", SqlDbType.VarChar, 8000) { Value = model.Question },
				new SqlParameter("@Answer", SqlDbType.VarChar, 100) { Value = model.Answer },
				new SqlParameter("@Credit", SqlDbType.Float, 8) { Value = model.Credit },
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
        public Course_UnitTest GetModel(int id, string where)
        {
            string sql = "select * from [dbo].[Course_UnitTest] where [Id]=@Id";
            if (!string.IsNullOrEmpty(where))
                sql += " and " + where;
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                if (reader.Read())
                {
                    Course_UnitTest model = new Course_UnitTest();
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
        /// 删除一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Course_UnitTest] set ");
            sql.Append("Delflag = 1");
            sql.Append(" where [Id]=@Id");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Course_UnitTest> GetList(string where, string orderBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from [dbo].[Course_UnitTest]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            if (!string.IsNullOrEmpty(orderBy))
                sql.Append(" order by " + orderBy);
            List<Course_UnitTest> list = new List<Course_UnitTest>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    Course_UnitTest model = new Course_UnitTest();
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
        public List<Course_UnitTest> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(1) from [dbo].[Course_UnitTest]");
            if (!string.IsNullOrEmpty(where))
                sb.Append(" where " + where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Course_UnitTest]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(") as T where [RowNum] between " + start + " and " + end);
            List<Course_UnitTest> list = new List<Course_UnitTest>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    Course_UnitTest model = new Course_UnitTest();
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
            sql.Append("select [Id],[TrainingId],[Verson],[Content],[QTtype],[Question],[Answer],[Credit],[Display],[Delflag],[CreateDate] from [dbo].[Course_UnitTest]");
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
            sb.Append("select count(1) from [dbo].[Course_UnitTest]");
            if (!string.IsNullOrEmpty(where))
                sb.Append(" where " + where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select [Id],[TrainingId],[Verson],[Content],[QTtype],[Question],[Answer],[Credit],[Display],[Delflag],[CreateDate],ROW_NUMBER() over (order by " + orderBy + ") as [RowNum] from [dbo].[Course_UnitTest]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(") as T where [RowNum] between " + start + " and " + end);
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        private void ConvertToModel(IDataReader reader, Course_UnitTest model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["TrainingId"] != DBNull.Value)
                model.TrainingId = Convert.ToInt32(reader["TrainingId"]);
            if (reader["Verson"] != DBNull.Value)
                model.Verson = reader["Verson"].ToString();
            if (reader["Content"] != DBNull.Value)
                model.Content = reader["Content"].ToString();
            if (reader["QTtype"] != DBNull.Value)
                model.QTtype = Convert.ToInt32(reader["QTtype"]);
            if (reader["Question"] != DBNull.Value)
                model.Question = reader["Question"].ToString();
            if (reader["Answer"] != DBNull.Value)
                model.Answer = reader["Answer"].ToString();
            if (reader["Credit"] != DBNull.Value)
                model.Credit = Convert.ToDouble(reader["Credit"]);
            if (reader["Display"] != DBNull.Value)
                model.Display = Convert.ToBoolean(reader["Display"]);
            if (reader["Delflag"] != DBNull.Value)
                model.Delflag = Convert.ToBoolean(reader["Delflag"]);
            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
        }

        /// <summary>
        /// 判断指定课程是否已经添加试题
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool IsExistsExamQues(int Id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from dbo.Course_UnitTest where TrainingId=@TrainingId and Display = 1 and Delflag = 0");
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = Id }
			};

            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString(), cmdParams))
            {
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取指定课程的结业考试试题信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Course_UnitTest> GetExamQuesInfo(int id)
        {
            string sql = @"select * from dbo.Course_UnitTest where TrainingId=@TrainingId and Display = 1 and Delflag = 0";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = id }
            };

            List<Course_UnitTest> list = new List<Course_UnitTest>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    Course_UnitTest model = new Course_UnitTest();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 更新一条结业考试试题记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateCourseTest(Course_Test model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update [dbo].[Course_UnitTest] set ");
            sql.Append("[Content]=@Content,[QTtype]=@QTtype,[Question]=@Question,[Answer]=@Answer,[Credit]=@Credit");
            sql.Append(" where [Id]=@Id");
            SqlParameter[] cmdParams = new SqlParameter[] {
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = model.Id },
				new SqlParameter("@Content", SqlDbType.VarChar, 2000) { Value = model.Content },
				new SqlParameter("@QTtype", SqlDbType.Int, 4) { Value = model.QTtype },
				new SqlParameter("@Question", SqlDbType.VarChar, 8000) { Value = model.Question },
				new SqlParameter("@Answer", SqlDbType.VarChar, 100) { Value = model.Answer },
				new SqlParameter("@Credit", SqlDbType.Float, 8) { Value = model.Credit },
			};
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), cmdParams);
        }

        public int AddExem(List<Course_UnitTest> list)
        {
            int result = -1;
            foreach (Course_UnitTest c in list)
            {
                StringBuilder sqlBuild = new StringBuilder();
                sqlBuild.Append("insert into [dbo].[Course_UnitTest] ([TrainingId],[Verson],[Content],[QTtype],[Question],[Answer],[Credit],[Display],[Delflag],[CreateDate])");
                sqlBuild.Append(" values (@TrainingId,@Verson,@Content,@QTtype,@Question,@Answer,@Credit,@Display,@Delflag,@CreateDate)");
                sqlBuild.Append(" set @Id=@@IDENTITY");
                SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = c.Id, Direction = ParameterDirection.Output },
				new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = c.TrainingId },
				new SqlParameter("@Verson", SqlDbType.VarChar, 20) { Value = c.Verson },
				new SqlParameter("@Content", SqlDbType.VarChar, 2000) { Value = c.Content },
				new SqlParameter("@QTtype", SqlDbType.Int, 4) { Value = c.QTtype },
				new SqlParameter("@Question", SqlDbType.VarChar, 8000) { Value = c.Question },
				new SqlParameter("@Answer", SqlDbType.VarChar, 100) { Value = c.Answer },
				new SqlParameter("@Credit", SqlDbType.Float, 8) { Value = c.Credit },
				new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = c.Display },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = c.Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = c.CreateDate }
			    };
                result = Convert.ToInt32(MSEntLibSqlHelper.ExecuteNonQueryBySql(sqlBuild.ToString(), cmdParams));
            }
            return result;
        }


        /// <summary>
        /// 更新指定课程结业考试试题版本号
        /// </summary>
        /// <param name="TrainingId"></param>
        /// <param name="Verson"></param>
        /// <returns></returns>
        public int SetVerson(int TrainingId, string Verson)
        {
            int result = -1;

            //查询指定课程下的结业考试试题信息
            string sql = @"select * from [dbo].[Course_UnitTest] where TrainingId=@TrainingId and Display = 1 and Delflag = 0";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = TrainingId }
            };

            List<Course_UnitTest> list = new List<Course_UnitTest>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    Course_UnitTest model1 = new Course_UnitTest();
                    ConvertToModel(reader, model1);
                    model1.Verson = Verson;
                    list.Add(model1);
                }
            }

            //将指定课程下的结业考试试题display置为0
            StringBuilder sqlBuild = new StringBuilder();
            sqlBuild.Append("update [dbo].[Course_UnitTest] set display = 0 where TrainingId=@TrainingId and display = 1 and delflag = 0;");
            cmdParams = new SqlParameter[] {
				new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = TrainingId }
			};
            result = MSEntLibSqlHelper.ExecuteNonQueryBySql(sqlBuild.ToString(), cmdParams);

            //新增新版本号的数据
            for (int i = 0; i < list.Count; i++)
            {
                sqlBuild = new StringBuilder();
                sqlBuild.Append("insert into [dbo].[Course_UnitTest] ([TrainingId],[Verson],[Content],[QTtype],[Question],[Answer],[Credit],[Display],[Delflag],[CreateDate])");
                sqlBuild.Append(" values (@TrainingId,@Verson,@Content,@QTtype,@Question,@Answer,@Credit,@Display,@Delflag,@CreateDate)");
                sqlBuild.Append(" set @Id=@@IDENTITY");
                cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = list[i].Id, Direction = ParameterDirection.Output },
				new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = list[i].TrainingId },
				new SqlParameter("@Verson", SqlDbType.VarChar, 20) { Value = list[i].Verson },
				new SqlParameter("@Content", SqlDbType.VarChar, 2000) { Value = list[i].Content },
				new SqlParameter("@QTtype", SqlDbType.Int, 4) { Value = list[i].QTtype },
				new SqlParameter("@Question", SqlDbType.VarChar, 8000) { Value = list[i].Question },
				new SqlParameter("@Answer", SqlDbType.VarChar, 100) { Value = list[i].Answer },
				new SqlParameter("@Credit", SqlDbType.Float, 8) { Value = list[i].Credit },
				new SqlParameter("@Display", SqlDbType.Bit, 1) { Value = list[i].Display },
				new SqlParameter("@Delflag", SqlDbType.Bit, 1) { Value = list[i].Delflag },
				new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = list[i].CreateDate }
			    };
                result = Convert.ToInt32(MSEntLibSqlHelper.ExecuteNonQueryBySql(sqlBuild.ToString(), cmdParams));
            }

            return result;
        }
    }
}
