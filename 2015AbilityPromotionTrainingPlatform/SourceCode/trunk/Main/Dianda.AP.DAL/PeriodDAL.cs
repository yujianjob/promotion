using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dianda.AP.Model;
using System.Data.SqlClient;

namespace Dianda.AP.DAL
{
    public class PeriodDAL
    {
        public DataTable GetTrainingFie()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select Id,Title from Traning_Field where DisPlay = 1 and Delflag = 0 ");
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        public DataTable GetSearch(int AccountId,int PlanId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select d.Title as FidTitle,b.Title as DetailTitle,b.TotalTime as Credits, b.CreateDate,b.Range as Range ");
            sql.Append(" from Member_ClassRegister a ");
            sql.Append(" left join Traning_Detail b on a.TrainingId = b.Id ");
            sql.Append(" left join dbo.Traning_Field d on b.TraingField = d.Id ");
            sql.Append(" where a.AccountId = " + AccountId + " and a.PlanId = " + PlanId + " and a.Result = 1 and b.Delflag = 0 ");
            //sql.Append(" and exists (select 1 from Class_TraningCommentResult where Delflag=0 and ClassID=A.ClassId and AccountId=A.AccountId) ");

            StringBuilder sb = new StringBuilder();
            sb.Append(" select '实践应用课程' as FidTitle,b.Title as DetailTitle,sum(c.Credits) as Credits,a.CreateDate,'0' as Range  ");
            sb.Append(" from Member_PracticalCourse a ");
            sb.Append(" left join PracticalCourse_Detail b on a.PracticalCourseId = b.Id ");
            sb.Append(" left join PracticalCourse_RoleCredits c on a.RoleId = c.RoleId ");
            sb.Append(" AND b.TraingField = c.TraingField AND b.TraingCategory = c.TraingCategory AND b.TraingTopic = c.TraingTopic  ");
            sb.Append(" where a.AccountId = '" + AccountId + "' and b.PlanId = '" + PlanId + "' and a.Status = 2 and a.Delflag = 0 and b.Delflag = 0 and c.Delflag = 0 group by b.Title,a.CreateDate ");
            DataTable table=MSEntLibSqlHelper.ExecuteDataSetBySql(sb.ToString()).Tables[0];
            DataTable dt=MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["FidTitle"] = table.Rows[i]["FidTitle"].ToString();
                dr["DetailTitle"] = table.Rows[i]["DetailTitle"].ToString();
                dr["Credits"] = table.Rows[i]["Credits"].ToString();
                dr["CreateDate"] = table.Rows[i]["CreateDate"].ToString();
                dr["Range"] = table.Rows[i]["Range"].ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public DataTable GetListTo(int pageSize, int pageIndex, string where, string orderBy, out int recordCount, int PlanId)
        {

            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(distinct a.AccountId) from dbo.Member_ClassRegister a left join Member_PlanOverall b on a.AccountId = b.AccountId and a.PlanId = b.PlanId left join Member_Account c on a.AccountId = c.Id left join Member_BaseInfo d on a.AccountId = d.AccountId where 1=1 ");
            if (!string.IsNullOrEmpty(where))
                sb.Append(where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;

            DataTable fiedt = GetTrainingFie();
            DataTable dt = new DataTable();
            dt.Columns.Add("AccountId");
            dt.Columns.Add("RealName");
            dt.Columns.Add("TeacherNo");
            dt.Columns.Add("Result");
            for (int i = 0; i < fiedt.Rows.Count; i++)
            {
                dt.Columns.Add(fiedt.Rows[i][1].ToString());
            }

            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from ( ");
            sql.Append(" select *,ROW_NUMBER() over (order by " + orderBy + ") as RowNum from ( ");
            sql.Append(" select distinct a.AccountId, ");
            sql.Append(" c.Nickname as RealName,b.Result,d.TeacherNo ");
            sql.Append(" from Member_ClassRegister a  ");
            sql.Append(" left join Member_PlanOverall b on a.AccountId = b.AccountId and a.PlanId = b.PlanId ");
            sql.Append(" left join Member_Account c on a.AccountId = c.Id ");
            sql.Append(" left join Member_BaseInfo d on a.AccountId = d.AccountId where 1=1 ");
            //sql.Append(" and exists (select 1 from Class_TraningCommentResult where Delflag=0 and ClassID=A.ClassId and AccountId=A.AccountId) ");
            if (!string.IsNullOrEmpty(where))
            {
                sql.Append(where);
            }
            sql.Append(")as d)as T where RowNum between @start and @end");
            SqlParameter[] para ={
                                    new SqlParameter("@start",start),
                                    new SqlParameter("@end",end)
                                 };
            DataTable adt = MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString(),para).Tables[0];

            for (int i = 0; i < adt.Rows.Count; i++)
            {
                DataTable cdt = GetCredits(Convert.ToInt32(adt.Rows[i]["AccountId"].ToString()), PlanId);
                DataRow dr = dt.NewRow();
                dr["AccountId"] = adt.Rows[i]["AccountId"].ToString();
                dr["RealName"] = adt.Rows[i]["RealName"].ToString();
                dr["TeacherNo"] = adt.Rows[i]["TeacherNo"].ToString();
                dr["Result"] = adt.Rows[i]["Result"].ToString();
                for (int k = 0; k < fiedt.Rows.Count; k++)
                {
                    DataTable tdt = GetTrainingRedit(Convert.ToInt32(adt.Rows[i]["AccountId"].ToString()), Convert.ToInt32(fiedt.Rows[k][0].ToString()), PlanId);
                    string Credits = "";
                    string MinValue = "";
                    if (fiedt.Rows.Count == cdt.Rows.Count)
                    {
                        for (int y = 0; y < cdt.Rows.Count; y++)
                        {
                            if (cdt.Rows[y][0].ToString() == fiedt.Rows[k][0].ToString())
                            {
                                MinValue = cdt.Rows[y][1].ToString();
                            }
                        }
                    }
                    else
                    {
                        MinValue = "0";
                    }
                    if (tdt.Rows.Count == 0)
                    {
                        Credits = "0";
                    }
                    else
                    {
                        Credits = tdt.Rows[0][0].ToString();
                    }

                    dr[fiedt.Rows[k][1].ToString()] = Credits + "/" + MinValue;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public DataTable GetCredits(int AccountId, int PlanId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select TraningField,MinValue from Training_Credits where OrganId in ");
            sb.Append(" (select ParentId from Organ_Detail where Id in ");
            sb.Append(" (select OrganId from Member_Account where Id = " + AccountId + ")) and TraningField <> -1 and PlanId=" + PlanId + " ");
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sb.ToString()).Tables[0];
        }

        public DataTable GetTrainingRedit(int AccountId, int TraningField, int PlanId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select (case when SUM(a.Credits) is null then 0 else SUM(a.Credits) end) + (case when (select SUM(Credits) ");
            sb.Append(" from Member_PlanExemption where AccountId = '" + AccountId + "' ");
            sb.Append(" and PlanId = '" + PlanId + "' and PEType = '" + TraningField + "' and Delflag = 0 ) IS null then 0 else (select SUM(Credits) ");
            sb.Append(" from Member_PlanExemption where AccountId = '" + AccountId + "' ");
            sb.Append(" and PlanId = '" + PlanId + "' and PEType = '" + TraningField + "') end) as Credits from Member_TrainingRedit a ");
            sb.Append(" where a.AccountId =" + AccountId + " and a.TrainingField = " + TraningField + " and a.PlanId = " + PlanId + " ");
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sb.ToString()).Tables[0];
        }

        public DataTable List(int pageSize, int pageIndex, string where, string orderBy, out int recordCount, int PlanId)
        {
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(distinct a.AccountId) from dbo.Member_ClassRegister a left join Member_PlanOverall b on a.AccountId = b.AccountId left join Member_Account c on a.AccountId = c.Id left join Member_BaseInfo d on a.AccountId = d.AccountId where 1=1 ");
            if (!string.IsNullOrEmpty(where))
                sb.Append(where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;

            DataTable fiedt = GetTrainingFie();
            DataTable dt = new DataTable();
            dt.Columns.Add("AccountId");
            dt.Columns.Add("RealName");
            dt.Columns.Add("TeacherNo");
            dt.Columns.Add("Result");
            dt.Columns.Add("CertificateCode");
            for (int i = 0; i < fiedt.Rows.Count; i++)
            {
                dt.Columns.Add(fiedt.Rows[i][1].ToString());
            }

            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from ( ");
            sql.Append(" select *,ROW_NUMBER() over (order by " + orderBy + ") as RowNum from ( ");
            sql.Append(" select distinct a.AccountId, ");
            sql.Append(" c.NickName,b.Result,d.TeacherNo,b.CertificateCode ");
            sql.Append(" from Member_ClassRegister a  ");
            sql.Append(" left join Member_PlanOverall b on a.AccountId = b.AccountId ");
            sql.Append(" left join Member_Account c on a.AccountId = c.Id ");
            sql.Append(" left join Member_BaseInfo d on a.AccountId = d.AccountId where 1=1 ");
            if (!string.IsNullOrEmpty(where))
            {
                sql.Append(where);
            }
            sql.Append(")as d)as T where RowNum between " + start + " and " + end);
            DataTable adt = MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];

            for (int i = 0; i < adt.Rows.Count; i++)
            {
                DataTable cdt = GetCredits(Convert.ToInt32(adt.Rows[i]["AccountId"].ToString()), PlanId);
                DataRow dr = dt.NewRow();
                dr["AccountId"] = adt.Rows[i]["AccountId"].ToString();
                dr["RealName"] = adt.Rows[i]["NickName"].ToString();
                dr["TeacherNo"] = adt.Rows[i]["TeacherNo"].ToString();
                dr["Result"] = adt.Rows[i]["Result"].ToString();
                dr["CertificateCode"] = adt.Rows[i]["CertificateCode"].ToString();
                for (int k = 0; k < fiedt.Rows.Count; k++)
                {
                    DataTable tdt = GetTrainingRedit(Convert.ToInt32(adt.Rows[i]["AccountId"].ToString()), Convert.ToInt32(fiedt.Rows[k][0].ToString()), PlanId);
                    string Credits = "";
                    string MinValue = "";
                    if (fiedt.Rows.Count == cdt.Rows.Count)
                    {
                        for (int y = 0; y < cdt.Rows.Count; y++)
                        {
                            if (cdt.Rows[y][0].ToString() == fiedt.Rows[k][0].ToString())
                            {
                                MinValue = cdt.Rows[y][1].ToString();
                            }
                        }
                    }
                    else
                    {
                        MinValue = "0";
                    }
                    if (tdt.Rows.Count == 0)
                    {
                        Credits = "0";
                    }
                    else
                    {
                        Credits = tdt.Rows[0][0].ToString();
                    }

                    dr[fiedt.Rows[k][1].ToString()] = Credits + "/" + MinValue;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void ConvertToModel(IDataReader reader, PeriodModel model)
        {
            if (reader["AccountId"] != DBNull.Value)
                model.AccountId = Convert.ToInt32(reader["AccountId"]);

            if (reader["RealName"] != DBNull.Value)
                model.RealName = reader["RealName"].ToString();

            if (reader["TeacherNo"] != DBNull.Value)
                model.TeacherNo = reader["TeacherNo"].ToString();

            if (reader["Value"] != DBNull.Value)
                model.Value = reader["Value"].ToString();

            if (reader["Result"] != DBNull.Value)
                model.Result = Convert.ToInt32(reader["Result"]);
        }
    }
}
