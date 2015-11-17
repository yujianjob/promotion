using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.DAL
{
    public class LearnMyTestDAL
    {
        /// <summary>
        /// 取得一条测试信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public MyTestRound GetRound(int id, string where)
        {
            string sql = "select * from [2015NlCP].[dbo].[account] where [Id]=@Id";
            if (!string.IsNullOrEmpty(where))
                sql += " and " + where;
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                if (reader.Read())
                {
                    MyTestRound model = new MyTestRound();
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
        /// 取得测试信息列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<MyTestRound> GetRoundList(string where, string orderBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from [2015NlCP].[dbo].[account]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            if (!string.IsNullOrEmpty(orderBy))
                sql.Append(" order by " + orderBy);
            List<MyTestRound> list = new List<MyTestRound>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    MyTestRound model = new MyTestRound();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        public List<MyTestRound> V_GetRoundList(string where, string orderBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from [2015NlCP].[dbo].[V_account]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            if (!string.IsNullOrEmpty(orderBy))
                sql.Append(" order by " + orderBy);
            List<MyTestRound> list = new List<MyTestRound>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    MyTestRound model = new MyTestRound();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 取得一条测试分数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public MyTestScore GetScore(int id, string where)
        {
            string sql = "select * from [2015NlCP].[dbo].[score] where [Id]=@Id";
            if (!string.IsNullOrEmpty(where))
                sql += " and " + where;
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id }
			};
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                if (reader.Read())
                {
                    MyTestScore model = new MyTestScore();
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
        /// 取得测试分数列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<MyTestScore> GetScoreList(string where, string orderBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from [2015NlCP].[dbo].[score]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            if (!string.IsNullOrEmpty(orderBy))
                sql.Append(" order by " + orderBy);
            List<MyTestScore> list = new List<MyTestScore>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    MyTestScore model = new MyTestScore();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        private void ConvertToModel(IDataReader reader, MyTestRound model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["UserId"] != DBNull.Value)
                model.UserId = Convert.ToInt32(reader["UserId"]);
            if (reader["UploadDateTime"] != DBNull.Value)
                model.UploadDate = Convert.ToDateTime(reader["UploadDateTime"]);
        }

        private void ConvertToModel(IDataReader reader, MyTestScore model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["UserId"] != DBNull.Value)
                model.UserId = Convert.ToInt32(reader["UserId"]);
            if (reader["DimensionId"] != DBNull.Value)
                model.DimensionId = Convert.ToInt32(reader["DimensionId"]);
            if (reader["Score"] != DBNull.Value)
                model.Score = reader["Score"].ToString();
        }
    }
}
