﻿using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.DAL
{
    //Class_Detail
    public partial class Class_DetailDAL
    {
        public Class_DetailDAL()
        { }

        #region  Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(Id) from Class_Detail";
            DataSet obj = MSEntLibSqlHelper.ExecuteDataSetBySql(strsql);
            if (obj.Tables.Count <= 0 || obj.Tables[0].Rows[0] == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.Tables[0].Rows[0].ToString());
            }
        }
        // <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Class_Detail");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");
            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[0].Value = Id;
            return MSEntLibSqlHelper.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Class_Detail model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Class_Detail(");
            strSql.Append("People,LimitPeopleCnt,Address,Content,ManagerId,OrganId,Subject,StudyLevel,TeachGrade,TeachRank,Title,OrganRange,Instructor,Status,ApplyRemark,Display,Delflag,CreateDate,PartitionId,TraningId,PlanId,SignUpStartTime,SignUpEndTime,OpenClassFrom,OpenClassTo,ClassForm");
            strSql.Append(") values (");
            strSql.Append("@People,@LimitPeopleCnt,@Address,@Content,@ManagerId,@OrganId,@Subject,@StudyLevel,@TeachGrade,@TeachRank,@Title,@OrganRange,@Instructor,@Status,@ApplyRemark,@Display,@Delflag,@CreateDate,@PartitionId,@TraningId,@PlanId,@SignUpStartTime,@SignUpEndTime,@OpenClassFrom,@OpenClassTo,@ClassForm");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@People", SqlDbType.Int,4) ,            
                        new SqlParameter("@LimitPeopleCnt", SqlDbType.Int,4) ,            
                        new SqlParameter("@Address", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@Content", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@ManagerId", SqlDbType.Int,4) ,            
                        new SqlParameter("@OrganId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Subject", SqlDbType.Bit,1) ,            
                        new SqlParameter("@StudyLevel", SqlDbType.Bit,1) ,            
                        new SqlParameter("@TeachGrade", SqlDbType.Bit,1) ,            
                        new SqlParameter("@TeachRank", SqlDbType.Bit,1) ,            
                        new SqlParameter("@Title", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@OrganRange", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@Instructor", SqlDbType.Int,4) ,            
                        new SqlParameter("@Status", SqlDbType.Int,4) ,            
                        new SqlParameter("@ApplyRemark", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@Display", SqlDbType.Bit,1) ,            
                        new SqlParameter("@Delflag", SqlDbType.Bit,1) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@PartitionId", SqlDbType.Int,4) ,            
                        new SqlParameter("@TraningId", SqlDbType.Int,4) ,            
                        new SqlParameter("@PlanId", SqlDbType.Int,4) ,            
                        new SqlParameter("@SignUpStartTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@SignUpEndTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@OpenClassFrom", SqlDbType.DateTime) ,            
                        new SqlParameter("@OpenClassTo", SqlDbType.DateTime) ,            
                        new SqlParameter("@ClassForm", SqlDbType.Int,4)             
              
            };
            parameters[0].Value = model.People;
            parameters[1].Value = model.LimitPeopleCnt;
            parameters[2].Value = model.Address;
            parameters[3].Value = model.Content;
            parameters[4].Value = model.ManagerId;
            parameters[5].Value = model.OrganId;
            parameters[6].Value = model.Subject;
            parameters[7].Value = model.StudyLevel;
            parameters[8].Value = model.TeachGrade;
            parameters[9].Value = model.TeachRank;
            parameters[10].Value = model.Title;
            parameters[11].Value = model.OrganRange;
            parameters[12].Value = model.Instructor;
            parameters[13].Value = model.Status;
            parameters[14].Value = model.ApplyRemark;
            parameters[15].Value = model.Display;
            parameters[16].Value = model.Delflag;
            parameters[17].Value = model.CreateDate;
            parameters[18].Value = model.PartitionId;
            parameters[19].Value = model.TraningId;
            parameters[20].Value = model.PlanId;
            parameters[21].Value = model.SignUpStartTime;
            parameters[22].Value = model.SignUpEndTime;
            parameters[23].Value = model.OpenClassFrom;
            parameters[24].Value = model.OpenClassTo;
            parameters[25].Value = model.ClassForm;



            object obj = MSEntLibSqlHelper.ExecuteScalarBySql(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {

                return Convert.ToInt32(obj);

            }

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Class_Detail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Class_Detail set ");

            strSql.Append(" People = @People , ");
            strSql.Append(" LimitPeopleCnt = @LimitPeopleCnt , ");
            strSql.Append(" Address = @Address , ");
            strSql.Append(" Content = @Content , ");
            strSql.Append(" ManagerId = @ManagerId , ");
            strSql.Append(" OrganId = @OrganId , ");
            strSql.Append(" Subject = @Subject , ");
            strSql.Append(" StudyLevel = @StudyLevel , ");
            strSql.Append(" TeachGrade = @TeachGrade , ");
            strSql.Append(" TeachRank = @TeachRank , ");
            strSql.Append(" Title = @Title , ");
            strSql.Append(" OrganRange = @OrganRange , ");
            strSql.Append(" Instructor = @Instructor , ");
            strSql.Append(" Status = @Status , ");
            strSql.Append(" ApplyRemark = @ApplyRemark , ");
            strSql.Append(" Display = @Display , ");
            strSql.Append(" Delflag = @Delflag , ");
            strSql.Append(" CreateDate = @CreateDate , ");
            strSql.Append(" PartitionId = @PartitionId , ");
            strSql.Append(" TraningId = @TraningId , ");
            strSql.Append(" PlanId = @PlanId , ");
            strSql.Append(" SignUpStartTime = @SignUpStartTime , ");
            strSql.Append(" SignUpEndTime = @SignUpEndTime , ");
            strSql.Append(" OpenClassFrom = @OpenClassFrom , ");
            strSql.Append(" OpenClassTo = @OpenClassTo , ");
            strSql.Append(" ClassForm = @ClassForm  ");
            strSql.Append(" where  ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@People", SqlDbType.Int,4) ,      
                        new SqlParameter("@LimitPeopleCnt", SqlDbType.Int,4) ,      
                        new SqlParameter("@Address", SqlDbType.VarChar,200) ,      
                        new SqlParameter("@Content", SqlDbType.VarChar,500) ,      
                        new SqlParameter("@ManagerId", SqlDbType.Int,4) ,      
                        new SqlParameter("@OrganId", SqlDbType.Int,4) ,      
                        new SqlParameter("@Subject", SqlDbType.Bit,1) ,      
                        new SqlParameter("@StudyLevel", SqlDbType.Bit,1) ,      
                        new SqlParameter("@TeachGrade", SqlDbType.Bit,1) ,      
                        new SqlParameter("@TeachRank", SqlDbType.Bit,1) ,      
                        new SqlParameter("@Title", SqlDbType.VarChar,200) ,      
                        new SqlParameter("@OrganRange", SqlDbType.VarChar,500) ,      
                        new SqlParameter("@Instructor", SqlDbType.Int,4) ,      
                        new SqlParameter("@Status", SqlDbType.Int,4) ,      
                        new SqlParameter("@ApplyRemark", SqlDbType.VarChar,500) ,      
                        new SqlParameter("@Display", SqlDbType.Bit,1) ,      
                        new SqlParameter("@Delflag", SqlDbType.Bit,1) ,      
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,      
                        new SqlParameter("@PartitionId", SqlDbType.Int,4) ,      
                        new SqlParameter("@TraningId", SqlDbType.Int,4) ,      
                        new SqlParameter("@PlanId", SqlDbType.Int,4) ,      
                        new SqlParameter("@SignUpStartTime", SqlDbType.DateTime) ,      
                        new SqlParameter("@SignUpEndTime", SqlDbType.DateTime) ,      
                        new SqlParameter("@OpenClassFrom", SqlDbType.DateTime) ,      
                        new SqlParameter("@OpenClassTo", SqlDbType.DateTime) ,      
                        new SqlParameter("@ClassForm", SqlDbType.Int,4) ,      
              
                                                  new SqlParameter("@Id", SqlDbType.Int,4)  
                            		
            };
            parameters[0].Value = model.People;
            parameters[1].Value = model.LimitPeopleCnt;
            parameters[2].Value = model.Address;
            parameters[3].Value = model.Content;
            parameters[4].Value = model.ManagerId;
            parameters[5].Value = model.OrganId;
            parameters[6].Value = model.Subject;
            parameters[7].Value = model.StudyLevel;
            parameters[8].Value = model.TeachGrade;
            parameters[9].Value = model.TeachRank;
            parameters[10].Value = model.Title;
            parameters[11].Value = model.OrganRange;
            parameters[12].Value = model.Instructor;
            parameters[13].Value = model.Status;
            parameters[14].Value = model.ApplyRemark;
            parameters[15].Value = model.Display;
            parameters[16].Value = model.Delflag;
            parameters[17].Value = model.CreateDate;
            parameters[18].Value = model.PartitionId;
            parameters[19].Value = model.TraningId;
            parameters[20].Value = model.PlanId;
            parameters[21].Value = model.SignUpStartTime;
            parameters[22].Value = model.SignUpEndTime;
            parameters[23].Value = model.OpenClassFrom;
            parameters[24].Value = model.OpenClassTo;
            parameters[25].Value = model.ClassForm;
            parameters[26].Value = model.Id;


            int rows = MSEntLibSqlHelper.ExecuteNonQueryBySql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Class_Detail ");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[0].Value = Id;

            int rows = MSEntLibSqlHelper.ExecuteNonQueryBySql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Class_Detail ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
            int rows = MSEntLibSqlHelper.ExecuteNonQueryBySql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Class_Detail GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, People, LimitPeopleCnt, Address, Content, ManagerId, OrganId, Subject, StudyLevel, TeachGrade, TeachRank, Title, OrganRange, Instructor, Status, ApplyRemark, Display, Delflag, CreateDate, PartitionId, TraningId, PlanId, SignUpStartTime, SignUpEndTime, OpenClassFrom, OpenClassTo, ClassForm  ");
            strSql.Append("  from Class_Detail ");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[0].Value = Id;

            Model.Class_Detail model = new Model.Class_Detail();
            DataSet ds = MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["People"].ToString() != "")
                {
                    model.People = int.Parse(ds.Tables[0].Rows[0]["People"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LimitPeopleCnt"].ToString() != "")
                {
                    model.LimitPeopleCnt = int.Parse(ds.Tables[0].Rows[0]["LimitPeopleCnt"].ToString());
                }
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                if (ds.Tables[0].Rows[0]["ManagerId"].ToString() != "")
                {
                    model.ManagerId = int.Parse(ds.Tables[0].Rows[0]["ManagerId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrganId"].ToString() != "")
                {
                    model.OrganId = int.Parse(ds.Tables[0].Rows[0]["OrganId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Subject"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Subject"].ToString() == "1") || (ds.Tables[0].Rows[0]["Subject"].ToString().ToLower() == "true"))
                    {
                        model.Subject = true;
                    }
                    else
                    {
                        model.Subject = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["StudyLevel"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["StudyLevel"].ToString() == "1") || (ds.Tables[0].Rows[0]["StudyLevel"].ToString().ToLower() == "true"))
                    {
                        model.StudyLevel = true;
                    }
                    else
                    {
                        model.StudyLevel = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["TeachGrade"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["TeachGrade"].ToString() == "1") || (ds.Tables[0].Rows[0]["TeachGrade"].ToString().ToLower() == "true"))
                    {
                        model.TeachGrade = true;
                    }
                    else
                    {
                        model.TeachGrade = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["TeachRank"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["TeachRank"].ToString() == "1") || (ds.Tables[0].Rows[0]["TeachRank"].ToString().ToLower() == "true"))
                    {
                        model.TeachRank = true;
                    }
                    else
                    {
                        model.TeachRank = false;
                    }
                }
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                model.OrganRange = ds.Tables[0].Rows[0]["OrganRange"].ToString();
                if (ds.Tables[0].Rows[0]["Instructor"].ToString() != "")
                {
                    model.Instructor = int.Parse(ds.Tables[0].Rows[0]["Instructor"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                model.ApplyRemark = ds.Tables[0].Rows[0]["ApplyRemark"].ToString();
                if (ds.Tables[0].Rows[0]["Display"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Display"].ToString() == "1") || (ds.Tables[0].Rows[0]["Display"].ToString().ToLower() == "true"))
                    {
                        model.Display = true;
                    }
                    else
                    {
                        model.Display = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Delflag"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Delflag"].ToString() == "1") || (ds.Tables[0].Rows[0]["Delflag"].ToString().ToLower() == "true"))
                    {
                        model.Delflag = true;
                    }
                    else
                    {
                        model.Delflag = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PartitionId"].ToString() != "")
                {
                    model.PartitionId = int.Parse(ds.Tables[0].Rows[0]["PartitionId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TraningId"].ToString() != "")
                {
                    model.TraningId = int.Parse(ds.Tables[0].Rows[0]["TraningId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PlanId"].ToString() != "")
                {
                    model.PlanId = int.Parse(ds.Tables[0].Rows[0]["PlanId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SignUpStartTime"].ToString() != "")
                {
                    model.SignUpStartTime = DateTime.Parse(ds.Tables[0].Rows[0]["SignUpStartTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SignUpEndTime"].ToString() != "")
                {
                    model.SignUpEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["SignUpEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OpenClassFrom"].ToString() != "")
                {
                    model.OpenClassFrom = DateTime.Parse(ds.Tables[0].Rows[0]["OpenClassFrom"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OpenClassTo"].ToString() != "")
                {
                    model.OpenClassTo = DateTime.Parse(ds.Tables[0].Rows[0]["OpenClassTo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClassForm"].ToString() != "")
                {
                    model.ClassForm = int.Parse(ds.Tables[0].Rows[0]["ClassForm"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Class_Detail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM Class_Detail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString());
        }
        #endregion
    }
}