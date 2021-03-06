﻿using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.DAL
{
    public class PlanExemptionDAL
    {
        public List<PlanExemption> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(1) from dbo.Member_PlanExemption a left join Member_BaseInfo b on a.AccountId = b.AccountId left join Organ_Detail c on b.Organid = c.Id left join Member_Account d on a.AccountId = d.Id where a.Delflag = 0 ");
            if (!string.IsNullOrEmpty(where))
                sb.Append(where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select a.*,b.TeacherNo,b.RealName,c.Id as Organid,c.Title as OrganTitle,ROW_NUMBER() over (order by " + orderBy + ") as RowNum from dbo.Member_PlanExemption a left join Member_BaseInfo b on a.AccountId = b.AccountId  left join Member_Account d on a.AccountId = d.Id left join Organ_Detail c on d.Organid = c.Id where a.Delflag = 0 ");
            if (!string.IsNullOrEmpty(where))
                sql.Append(where);
            sql.Append(")as T where RowNum between @start and @end");
            SqlParameter[] para ={
                                    new SqlParameter("@start",start),
                                    new SqlParameter("@end",end)
                                 };
            List<PlanExemption> list = new List<PlanExemption>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString(),para))
            {
                while (reader.Read())
                {
                    PlanExemption model = new PlanExemption();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        public DataTable GetOrgan(int ParentId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select Id,Title from Organ_Detail where ParentId = " + ParentId + " and Delflag = 0 ");
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        public DataTable GetOrganqu(int OrganId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select Id,Title from Organ_Detail where Id=" + OrganId + " and Delflag = 0 ");
            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        /// <summary>
        /// 将导入的数据添加到数据库
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int AddExem(List<PlanExemption> list)
        {
            StringBuilder sql = new StringBuilder();
            if (list.Count != 0)
            {
                sql.Append(" insert into dbo.Member_PlanExemption (PlanId,Status,Remark,Credits,PEType,Creater,AccountId,Delflag,CreateDate) ");
                sql.Append(" values ");
                for (int i = 0; i < list.Count; i++)
                {
                    if (i != list.Count - 1)
                    {
                        sql.Append("('" + list[i].PlanId + "','" + list[i].Status + "','" + list[i].Remark + "','" + list[i].Credits + "','" + list[i].PEType + "','" + list[i].Creater + "','" + list[i].AccountId + "','" + list[i].Delflag + "','" + list[i].CreateDate + "'),");
                    }
                    else
                    {
                        sql.Append("('" + list[i].PlanId + "','" + list[i].Status + "','" + list[i].Remark + "','" + list[i].Credits + "','" + list[i].PEType + "','" + list[i].Creater + "','" + list[i].AccountId + "','" + list[i].Delflag + "','" + list[i].CreateDate + "')");
                    }
                }
            }
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString());
        }

        public int GetAccount(string RealName,string TeacherNo,string OrganTitle)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select distinct AccountId from Member_BaseInfo a ");
            sql.Append(" left join dbo.Organ_Detail b on a.Organid = b.Id ");
            sql.Append(" left join dbo.Member_Account c on a.AccountId = c.Id ");
            sql.Append(" where a.TeacherNo = '" + TeacherNo + "' and c.Nickname = '" + RealName + "' and b.Title = '" + OrganTitle + "' ");
            return Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sql.ToString()));
        }

        public bool GetSchool(string SchoolName,int OrganId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select ParentId from Organ_Detail where Title = '" + SchoolName + "' ");
            int id = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sql.ToString()));
            if (id != OrganId)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool GetSchoolTo(string SchoolName, int OrganId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select Id from Organ_Detail where Title = '" + SchoolName + "' ");
            int id = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sql.ToString()));
            if (id != OrganId)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int GetTeacherNo(string TeacherNo)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select AccountId from Member_BaseInfo where TeacherNo = '" + TeacherNo + "' ");
            return Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sql.ToString()));
        }

        public bool DelExem(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" update Member_PlanExemption set Delflag = 1 where id=" + id + " ");
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

        public PlanExemption GetModel(int id)
        {
            PlanExemption model = new PlanExemption();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select b.TeacherNo,a.Remark,a.Credits,a.PEType from Member_PlanExemption a  ");
            sql.Append(" left join Member_BaseInfo b on a.AccountId = b.AccountId where a.id=@id ");
            SqlParameter[] para ={
                                    new SqlParameter("@id",id)
                                 };
            IDataReader dr = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString(), para);
            if (dr.Read())
            {
                model.TeacherNo = dr["TeacherNo"].ToString();
                model.Remark = dr["Remark"].ToString();
                model.Credits = Convert.ToDouble(dr["Credits"]);
                model.PEType = Convert.ToInt32(dr["PEType"]);
            }
            return model;
        }

        public bool ExemEdit(PlanExemption model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" update Member_PlanExemption set AccountId=@AccountId,Remark=@Remark,Credits=@Credits,PEType=@PEType ");
            sql.Append(" where id=@id ");
            SqlParameter[] para = { 
                                    new SqlParameter("@AccountId",SqlDbType.Int),
                                    new SqlParameter("@Remark",SqlDbType.VarChar),
                                    new SqlParameter("@Credits",SqlDbType.Float),
                                    new SqlParameter("@PEType",SqlDbType.Int),
                                    new SqlParameter("@id",SqlDbType.Int)
                                  };
            para[0].Value = model.AccountId;
            para[1].Value = model.Remark;
            para[2].Value = model.Credits;
            para[3].Value = model.PEType;
            para[4].Value = model.Id;

            int i = MSEntLibSqlHelper.ExecuteNonQueryBySql(sql.ToString(), para);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 取得学习空间——免修学分
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetExemptionCredit(string where)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select PEType as TrainingField,sum(Credits) as Credits from Member_PlanExemption");
            if (!string.IsNullOrEmpty(where))
                sql.Append(" where " + where);
            sql.Append(" group by PEType");

            return MSEntLibSqlHelper.ExecuteDataSetBySql(sql.ToString()).Tables[0];
        }

        private void ConvertToModel(IDataReader reader, PlanExemption model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);

            if (reader["PlanId"] != DBNull.Value)
                model.PlanId = Convert.ToInt32(reader["PlanId"].ToString());

            if (reader["Status"] != DBNull.Value)
                model.Status = Convert.ToInt32(reader["Status"].ToString());

            if (reader["Remark"] != DBNull.Value)
                model.Remark = reader["Remark"].ToString();

            if (reader["Credits"] != DBNull.Value)
                model.Credits = Convert.ToDouble(reader["Credits"]);

            if (reader["PEType"] != DBNull.Value)
                model.PEType = Convert.ToInt32(reader["PEType"]);

            if (reader["Creater"] != DBNull.Value)
                model.Creater = Convert.ToInt32(reader["Creater"]);

            if (reader["AccountId"] != DBNull.Value)
                model.AccountId = Convert.ToInt32(reader["AccountId"]);

            if (reader["Delflag"] != DBNull.Value)
                model.Delflag = Convert.ToBoolean(reader["Delflag"]);

            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);

            if (reader["TeacherNo"] != DBNull.Value)
                model.TeacherNo = reader["TeacherNo"].ToString();

            if (reader["RealName"] != DBNull.Value)
                model.RealName = reader["RealName"].ToString();

            if (reader["Organid"] != DBNull.Value)
                model.Organid = Convert.ToInt32(reader["Organid"]);

            if (reader["OrganTitle"] != DBNull.Value)
                model.OrganTitle = reader["OrganTitle"].ToString();
        }

    }
}
