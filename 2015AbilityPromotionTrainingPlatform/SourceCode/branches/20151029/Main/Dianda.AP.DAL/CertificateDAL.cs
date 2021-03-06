﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianda.AP.Model;
using System.Data;
using System.Data.SqlClient;

namespace Dianda.AP.DAL
{
    public class CertificateDAL
    {

        public DataTable GetTrainingFie()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select Id,Title from Traning_Field where DisPlay = 1 and Delflag = 0 ");
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        public DataTable GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            DataTable Fie = GetTrainingFie();
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();

            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from (   ");
            sb.Append(" select a.AccountId,b.TeacherNo,case when b.RealName is null  ");
            sb.Append(" then g.UserName else b.RealName end as RealName,d.Title,  ");
            sb.Append(" convert(varchar,c.Credits)+'/'+CONVERT(varchar,e.MinValue) ");
            sb.Append(" as value,f.Result  from Member_ClassRegister a  ");
            sb.Append(" left join Member_BaseInfo b on a.AccountId = b.AccountId   ");
            sb.Append(" left join Member_TrainingRedit c on a.AccountId = c.AccountId  ");
            sb.Append(" left join Traning_Field d on c.TrainingField = d.Id ");
            sb.Append(" left join Training_Credits e on d.Id = e.TraningField  ");
            sb.Append(" and b.Organid = e.OrganId   ");
            sb.Append(" left join Member_PlanOverall f on a.AccountId = f.AccountId  ");
            sb.Append(" left join Member_Account g on a.AccountId = g.Id  where a.Delflag = 0  ");
            if (!string.IsNullOrEmpty(where))
                sb.Append(where);
            sb.Append(" ) t pivot ( Max(value) for t.Title in ( ");
            for (int i = 0; i < Fie.Rows.Count; i++)
            {
                if (i != Fie.Rows.Count - 1)
                {
                    sb.Append("[" + Fie.Rows[i][1].ToString() + "],");
                }
                else
                {
                    sb.Append("[" + Fie.Rows[i][1].ToString() + "]");
                }
            }
            sb.Append(")) as ourpivot");
            recordCount = MSEntLibSqlHelper.ExecuteDataSetBySql(sb.ToString()).Tables[0].Rows.Count;

            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;

            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from ( ");
            sql.Append(" select *,ROW_NUMBER() over (order by " + orderBy + ") as RowNum from (   ");
            sql.Append(" select a.AccountId,b.TeacherNo,case when b.RealName is null  ");
            sql.Append(" then g.UserName else b.RealName end as RealName,d.Title,  ");
            sql.Append(" convert(varchar,c.Credits)+'/'+CONVERT(varchar,e.MinValue) ");
            sql.Append(" as value,f.Result  from Member_ClassRegister a  ");
            sql.Append(" left join Member_BaseInfo b on a.AccountId = b.AccountId   ");
            sql.Append(" left join Member_TrainingRedit c on a.AccountId = c.AccountId  ");
            sql.Append(" left join Traning_Field d on c.TrainingField = d.Id ");
            sql.Append(" left join Training_Credits e on d.Id = e.TraningField  ");
            sql.Append(" and b.Organid = e.OrganId   ");
            sql.Append(" left join Member_PlanOverall f on a.AccountId = f.AccountId  ");
            sql.Append(" left join Member_Account g on a.AccountId = g.Id  where a.Delflag = 0  ");
            if (!string.IsNullOrEmpty(where))
                sql.Append(where);
            sql.Append(" ) t pivot ( Max(value) for t.Title in ( ");
            for (int i = 0; i < Fie.Rows.Count; i++)
            {
                if (i != Fie.Rows.Count - 1)
                {
                    sql.Append("[" + Fie.Rows[i][1].ToString() + "],");
                }
                else
                {
                    sql.Append("[" + Fie.Rows[i][1].ToString() + "]");
                }
            }
            sql.Append(")) as ourpivot) as t  where RowNum between " + start + " and " + end);

            DataTable dt = MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
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
            dt.Columns.Add("CertificateCode");
            for (int i = 0; i < fiedt.Rows.Count; i++)
            {
                dt.Columns.Add(fiedt.Rows[i][1].ToString());
            }

            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from ( ");
            sql.Append(" select *,ROW_NUMBER() over (order by " + orderBy + ") as RowNum from ( ");
            sql.Append(" select distinct a.AccountId, ");
            sql.Append(" c.Nickname as RealName,b.Result,d.TeacherNo,b.CertificateCode ");
            sql.Append(" from Member_ClassRegister a  ");
            sql.Append(" left join Member_PlanOverall b on a.AccountId = b.AccountId and a.PlanId = b.PlanId ");
            sql.Append(" left join Member_Account c on a.AccountId = c.Id ");
            sql.Append(" left join Member_BaseInfo d on a.AccountId = d.AccountId where 1=1 ");
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
            sql.Append(")as d)as T where RowNum between @start and @end");
            SqlParameter[] para ={
                                    new SqlParameter("@start",start),
                                    new SqlParameter("@end",end)
                                 };
            DataTable adt = MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString(), para).Tables[0];

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
            sb.Append(" select a.Credits+(case when (select SUM(Credits) ");
            sb.Append(" from Member_PlanExemption where AccountId = a.AccountId ");
            sb.Append(" and PlanId = a.PlanId and PEType = a.TrainingField and Delflag = 0) IS null then 0 else (select SUM(Credits) ");
            sb.Append(" from Member_PlanExemption where AccountId = a.AccountId ");
            sb.Append(" and PlanId = a.PlanId and PEType = a.TrainingField) end) as Credits from Member_TrainingRedit a ");
            sb.Append(" where a.AccountId =" + AccountId + " and a.TrainingField = " + TraningField + " and a.PlanId = " + PlanId + " ");
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sb.ToString()).Tables[0];
            
        }

        public DataTable GetExport(string where, string orderBy,int PlanId)
        {
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

            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();

            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from ( ");
            sql.Append(" select *,ROW_NUMBER() over (order by " + orderBy + ") as RowNum from ( ");
            sql.Append(" select distinct a.AccountId, ");
            sql.Append(" case when d.RealName is null then c.Nickname else d.RealName end as RealName,b.Result,d.TeacherNo ");
            sql.Append(" from Member_ClassRegister a  ");
            sql.Append(" left join Member_PlanOverall b on a.AccountId = b.AccountId ");
            sql.Append(" left join Member_Account c on a.AccountId = c.Id ");
            sql.Append(" left join Member_BaseInfo d on a.AccountId = d.AccountId where 1=1 ");
            if (!string.IsNullOrEmpty(where))
            {
                sql.Append(where);
            }
            sql.Append(")as d)as T");
            DataTable adt = MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];

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


        public DataTable GetSearch(int AccountId, int PlanId)
        {
            DataTable dt = new DataTable();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select c.Title as FidTitle,b.TotalTime from Member_ClassRegister a ");
            sql.Append(" left join Traning_Detail b on a.TrainingId = b.Id ");
            sql.Append(" left join NationalAbility_Course c on b.NationalCoursId = c.Id ");
            sql.Append(" where AccountId = " + AccountId + " and PlanId = " + PlanId + " and Result = 1 and b.Delflag = 0 ");
            dt = MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];

            StringBuilder sb = new StringBuilder();
            sb.Append(" select d.Title as FidTitle,sum(c.Credits) as TotalTime ");
            sb.Append(" from Member_PracticalCourse a ");
            sb.Append(" left join PracticalCourse_Detail b on a.PracticalCourseId = b.Id ");
            sb.Append(" left join PracticalCourse_RoleCredits c on a.RoleId = c.RoleId ");
            sb.Append(" left join NationalAbility_Course d on b.NationalCoursId = b.Id ");
            sb.Append(" AND b.TraingField = c.TraingField AND b.TraingCategory = c.TraingCategory AND b.TraingTopic = c.TraingTopic  ");
            sb.Append(" where a.AccountId = '" + AccountId + "' and b.PlanId = '" + PlanId + "' and a.Status = 2 and a.Delflag = 0 and b.Delflag = 0 and c.Delflag = 0 group by b.Title,a.CreateDate,d.Title ");
            DataTable table = MSEntLibSqlHelper.ExecuteDataSetBySql(sb.ToString()).Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["FidTitle"] = table.Rows[i]["FidTitle"] == null ? "" : table.Rows[i]["FidTitle"].ToString();
                dr["TotalTime"] = table.Rows[i]["TotalTime"].ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }


        public bool CertificateCode(int AccountId,int PlanId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from dbo.Member_PlanOverall where AccountId='" + AccountId + "' and PlanId = '" + PlanId + "' ");
            DataTable dt = MSEntLibSqlHelper.ExecuteDataSetBySql(sb.ToString()).Tables[0];
            StringBuilder ss = new StringBuilder();
            ss.Append(" select case when (select OType from Organ_Detail where Id = a.OrganId ) = 1 ");
            ss.Append(" then (select code from Organ_Detail where Id = a.OrganId) ");
            ss.Append(" else (select code from Organ_Detail where Id in  ");
            ss.Append(" (select ParentId from Organ_Detail where Id=a.OrganId )) end as Code  ");
            ss.Append(" from Member_Account a where Id = " + AccountId + " ");
            string OrganCode = MSEntLibSqlHelper.ExecuteScalarBySql(ss.ToString()).ToString();
            string code = dt.Rows[dt.Rows.Count - 1]["CertificateCode"].ToString();
            if (code != null && code != "")
            {
                code = (int.Parse(code.Substring(code.Length - 6)) + 1).ToString();
                if (code.Length == 1)
                {
                    code = "00000" + code;
                }
                else if (code.Length == 2)
                {
                    code = "0000" + code;
                }
                else if (code.Length == 3)
                {
                    code = "000" + code;
                }
                else if (code.Length == 4)
                {
                    code = "00" + code;
                }
                else if (code.Length == 5)
                {
                    code = "0" + code;
                }
            }
            else
            {
                code = "000001";
            }

            string info = "NLTS31" + DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2) + "GP" + "01" + OrganCode + code;
            StringBuilder sql = new StringBuilder();
            sql.Append(" update dbo.Member_PlanOverall set CertificateCode = '" + info + "',CertificateCreateTime='" + DateTime.Now + "' where AccountId=" + AccountId + " and PlanId=" + PlanId + " ");
            int i = MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString());

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ResultAdd(int AccountId, int PlanId, bool type)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select count(1) from Member_PlanOverall where PlanId = '" + PlanId + "' and AccountId=" + AccountId + " ");
            int count = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            StringBuilder sql = new StringBuilder();
            int i = 0;
            if (count == 0)
            {
                sql.Append(" insert into dbo.Member_PlanOverall ");
                sql.Append("(PlanId,AccountId,Result,Delflag,CreateDate) ");
                sql.Append(" values (@PlanId,@AccountId,@Result,@Delflag,@CreateDate) ");

                SqlParameter[] para ={
                                    new SqlParameter("@PlanId",SqlDbType.Int),
                                    new SqlParameter("@AccountId",SqlDbType.Int),
                                    new SqlParameter("@Result",SqlDbType.Int),
                                    new SqlParameter("@Delflag",SqlDbType.Bit),
                                    new SqlParameter("@CreateDate",SqlDbType.Date)
                                 };
                para[0].Value = PlanId;
                para[1].Value = AccountId;
                if (type)
                {
                    para[2].Value = 2;
                }
                else
                {
                    para[2].Value = 1;
                }
                para[3].Value = false;
                para[4].Value = DateTime.Now;
                i = MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), para);
            }
            else
            {
                sql.Append(" update Member_PlanOverall set Result=@Result where PlanId = @PlanId and AccountId=@AccountId ");
                SqlParameter[] para ={
                                        new SqlParameter("@Result",SqlDbType.Int),
                                        new SqlParameter("@PlanId",SqlDbType.Int),
                                        new SqlParameter("@AccountId",SqlDbType.Int)
                                     };

                if (type)
                {
                    para[0].Value = 2;
                }
                else
                {
                    para[0].Value = 1;
                }
                para[1].Value = PlanId;
                para[2].Value = AccountId;
                i = MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), para);
            }
            
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
