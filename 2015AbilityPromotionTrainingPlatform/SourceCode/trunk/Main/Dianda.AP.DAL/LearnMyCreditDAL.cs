using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.DAL
{
    public class LearnMyCreditDAL
    {
        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Training_Credits> GetList(string where, string orderBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from [dbo].[Training_Credits]");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            if (!string.IsNullOrEmpty(orderBy))
                sql.Append(" order by " + orderBy);
            List<Training_Credits> list = new List<Training_Credits>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    Training_Credits model = new Training_Credits();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 取得已获学分
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetReceivedCredit(string where)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select C.TraingField,C.Range,SUM(C.TotalTime) as Credit from");
            sql.Append(" Member_ClassRegister A");
            sql.Append(" join Class_Detail B on A.ClassId=B.Id");
            sql.Append(" join Traning_Detail C on B.TraningId=C.Id");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(" group by C.TraingField,C.Range");
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        /// <summary>
        /// 取得在学课程学分（非实践）
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetTrainingCredit(string where)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select C.TraingField,C.Range,SUM(C.TotalTime) as Credit from");
            sql.Append(" Member_ClassRegister A");
            sql.Append(" join Class_Detail B on A.ClassId=B.Id");
            sql.Append(" join Traning_Detail C on B.TraningId=C.Id");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(" group by C.TraingField,C.Range");
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        /// <summary>
        /// 取得在学实践学分
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetPracticeCredit(string where)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select TraingField,SUM(Credits) as Credit from");
            sql.Append(" (select C.TraingField,C.Credits from");
            sql.Append(" PracticalCourse_Detail A");
            sql.Append(" join Member_PracticalCourse B on A.Id=B.PracticalCourseId");
            sql.Append(" join PracticalCourse_RoleCredits C on B.RoleId=C.RoleId and A.TraingTopic=C.TraingTopic");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(") as T group by TraingField");
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        #region
        private void ConvertToModel(IDataReader reader, Training_Credits model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["PlanId"] != DBNull.Value)
                model.PlanId = Convert.ToInt32(reader["PlanId"]);
            if (reader["TraningField"] != DBNull.Value)
                model.TraningField = Convert.ToInt32(reader["TraningField"]);
            if (reader["Level"] != DBNull.Value)
                model.Level = Convert.ToInt32(reader["Level"]);
            if (reader["MinValue"] != DBNull.Value)
                model.MinValue = Convert.ToDouble(reader["MinValue"]);
            if (reader["MaxValue"] != DBNull.Value)
                model.MaxValue = Convert.ToDouble(reader["MaxValue"]);
            if (reader["Sort"] != DBNull.Value)
                model.Sort = Convert.ToInt32(reader["Sort"]);
            if (reader["Delflag"] != DBNull.Value)
                model.Delflag = Convert.ToBoolean(reader["Delflag"]);
            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
        }
        #endregion

    }
}
