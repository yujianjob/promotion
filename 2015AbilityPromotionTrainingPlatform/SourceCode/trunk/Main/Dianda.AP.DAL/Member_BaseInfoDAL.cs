﻿using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianda.AP.Model.Prepare.ClassAddressList;

namespace Dianda.AP.DAL
{
    //Member_BaseInfo
    public partial class Member_BaseInfoDAL
    {
        public Member_BaseInfoDAL()
        { }

        #region  Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(Id) from Member_BaseInfo";
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
            strSql.Append("select count(1) from Member_BaseInfo");
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
        public int Add(Model.Member_BaseInfo model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Member_BaseInfo(");
            strSql.Append("CredentialsType,CredentialsNumber,Sex,Birthday,Nation,PoliticalStatus,Delflag,CreateDate,RegionId,StudySection,AccountId,Organid,Job,WorkRank,TeachDate,TeachStudySection,TeachSubject,TeachGrade,TraningType,TraningObject,EduLevel,TeacherNo,EduDegree,EduMajor,EduMajorOhter,GraduateSchool,GraduateTime,GroupId,RealName,Mobile,Phone,Address,PostCode");
            strSql.Append(") values (");
            strSql.Append("@CredentialsType,@CredentialsNumber,@Sex,@Birthday,@Nation,@PoliticalStatus,@Delflag,@CreateDate,@RegionId,@StudySection,@AccountId,@Organid,@Job,@WorkRank,@TeachDate,@TeachStudySection,@TeachSubject,@TeachGrade,@TraningType,@TraningObject,@EduLevel,@TeacherNo,@EduDegree,@EduMajor,@EduMajorOhter,@GraduateSchool,@GraduateTime,@GroupId,@RealName,@Mobile,@Phone,@Address,@PostCode");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@CredentialsType", SqlDbType.Int,4) ,            
                        new SqlParameter("@CredentialsNumber", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@Sex", SqlDbType.Int,4) ,            
                        new SqlParameter("@Birthday", SqlDbType.DateTime) ,            
                        new SqlParameter("@Nation", SqlDbType.Int,4) ,            
                        new SqlParameter("@PoliticalStatus", SqlDbType.Int,4) ,            
                        new SqlParameter("@Delflag", SqlDbType.Bit,1) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@RegionId", SqlDbType.Int,4) ,            
                        new SqlParameter("@StudySection", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@AccountId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Organid", SqlDbType.Int,4) ,            
                        new SqlParameter("@Job", SqlDbType.Int,4) ,            
                        new SqlParameter("@WorkRank", SqlDbType.Int,4) ,            
                        new SqlParameter("@TeachDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@TeachStudySection", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@TeachSubject", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@TeachGrade", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@TraningType", SqlDbType.Int,4) ,            
                        new SqlParameter("@TraningObject", SqlDbType.Int,4) ,            
                        new SqlParameter("@EduLevel", SqlDbType.Int,4) ,            
                        new SqlParameter("@TeacherNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@EduDegree", SqlDbType.Int,4) ,            
                        new SqlParameter("@EduMajor", SqlDbType.Int,4) ,            
                        new SqlParameter("@EduMajorOhter", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@GraduateSchool", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@GraduateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@GroupId", SqlDbType.Int,4) ,            
                        new SqlParameter("@RealName", SqlDbType.VarChar,120) ,            
                        new SqlParameter("@Mobile", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@Phone", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@Address", SqlDbType.VarChar,300) ,            
                        new SqlParameter("@PostCode", SqlDbType.VarChar,10)             
              
            };
            parameters[0].Value = model.CredentialsType;
            parameters[1].Value = model.CredentialsNumber;
            parameters[2].Value = model.Sex;
            parameters[3].Value = model.Birthday;
            parameters[4].Value = model.Nation;
            parameters[5].Value = model.PoliticalStatus;
            parameters[6].Value = model.Delflag;
            parameters[7].Value = model.CreateDate;
            parameters[8].Value = model.RegionId;
            parameters[9].Value = model.StudySection;
            parameters[10].Value = model.AccountId;
            parameters[11].Value = model.Organid;
            parameters[12].Value = model.Job;
            parameters[13].Value = model.WorkRank;
            parameters[14].Value = model.TeachDate;
            parameters[15].Value = model.TeachStudySection;
            parameters[16].Value = model.TeachSubject;
            parameters[17].Value = model.TeachGrade;
            parameters[18].Value = model.TraningType;
            parameters[19].Value = model.TraningObject;
            parameters[20].Value = model.EduLevel;
            parameters[21].Value = model.TeacherNo;
            parameters[22].Value = model.EduDegree;
            parameters[23].Value = model.EduMajor;
            parameters[24].Value = model.EduMajorOhter;
            parameters[25].Value = model.GraduateSchool;
            parameters[26].Value = model.GraduateTime;
            parameters[27].Value = model.GroupId;
            parameters[28].Value = model.RealName;
            parameters[29].Value = model.Mobile;
            parameters[30].Value = model.Phone;
            parameters[31].Value = model.Address;
            parameters[32].Value = model.PostCode;



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
        public bool Update(Model.Member_BaseInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Member_BaseInfo set ");

            strSql.Append(" CredentialsType = @CredentialsType , ");
            strSql.Append(" CredentialsNumber = @CredentialsNumber , ");
            strSql.Append(" Sex = @Sex , ");
            strSql.Append(" Birthday = @Birthday , ");
            strSql.Append(" Nation = @Nation , ");
            strSql.Append(" PoliticalStatus = @PoliticalStatus , ");
            strSql.Append(" Delflag = @Delflag , ");
            strSql.Append(" CreateDate = @CreateDate , ");
            strSql.Append(" RegionId = @RegionId , ");
            strSql.Append(" StudySection = @StudySection , ");
            strSql.Append(" AccountId = @AccountId , ");
            strSql.Append(" Organid = @Organid , ");
            strSql.Append(" Job = @Job , ");
            strSql.Append(" WorkRank = @WorkRank , ");
            strSql.Append(" TeachDate = @TeachDate , ");
            strSql.Append(" TeachStudySection = @TeachStudySection , ");
            strSql.Append(" TeachSubject = @TeachSubject , ");
            strSql.Append(" TeachGrade = @TeachGrade , ");
            strSql.Append(" TraningType = @TraningType , ");
            strSql.Append(" TraningObject = @TraningObject , ");
            strSql.Append(" EduLevel = @EduLevel , ");
            strSql.Append(" TeacherNo = @TeacherNo , ");
            strSql.Append(" EduDegree = @EduDegree , ");
            strSql.Append(" EduMajor = @EduMajor , ");
            strSql.Append(" EduMajorOhter = @EduMajorOhter , ");
            strSql.Append(" GraduateSchool = @GraduateSchool , ");
            strSql.Append(" GraduateTime = @GraduateTime , ");
            strSql.Append(" GroupId = @GroupId , ");
            strSql.Append(" RealName = @RealName , ");
            strSql.Append(" Mobile = @Mobile , ");
            strSql.Append(" Phone = @Phone , ");
            strSql.Append(" Address = @Address , ");
            strSql.Append(" PostCode = @PostCode  ");
            strSql.Append(" where  ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@CredentialsType", SqlDbType.Int,4) ,      
                        new SqlParameter("@CredentialsNumber", SqlDbType.VarChar,30) ,      
                        new SqlParameter("@Sex", SqlDbType.Int,4) ,      
                        new SqlParameter("@Birthday", SqlDbType.DateTime) ,      
                        new SqlParameter("@Nation", SqlDbType.Int,4) ,      
                        new SqlParameter("@PoliticalStatus", SqlDbType.Int,4) ,      
                        new SqlParameter("@Delflag", SqlDbType.Bit,1) ,      
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,      
                        new SqlParameter("@RegionId", SqlDbType.Int,4) ,      
                        new SqlParameter("@StudySection", SqlDbType.VarChar,200) ,      
                        new SqlParameter("@AccountId", SqlDbType.Int,4) ,      
                        new SqlParameter("@Organid", SqlDbType.Int,4) ,      
                        new SqlParameter("@Job", SqlDbType.Int,4) ,      
                        new SqlParameter("@WorkRank", SqlDbType.Int,4) ,      
                        new SqlParameter("@TeachDate", SqlDbType.DateTime) ,      
                        new SqlParameter("@TeachStudySection", SqlDbType.VarChar,200) ,      
                        new SqlParameter("@TeachSubject", SqlDbType.VarChar,200) ,      
                        new SqlParameter("@TeachGrade", SqlDbType.VarChar,200) ,      
                        new SqlParameter("@TraningType", SqlDbType.Int,4) ,      
                        new SqlParameter("@TraningObject", SqlDbType.Int,4) ,      
                        new SqlParameter("@EduLevel", SqlDbType.Int,4) ,      
                        new SqlParameter("@TeacherNo", SqlDbType.VarChar,20) ,      
                        new SqlParameter("@EduDegree", SqlDbType.Int,4) ,      
                        new SqlParameter("@EduMajor", SqlDbType.Int,4) ,      
                        new SqlParameter("@EduMajorOhter", SqlDbType.VarChar,200) ,      
                        new SqlParameter("@GraduateSchool", SqlDbType.VarChar,200) ,      
                        new SqlParameter("@GraduateTime", SqlDbType.DateTime) ,      
                        new SqlParameter("@GroupId", SqlDbType.Int,4) ,      
                        new SqlParameter("@RealName", SqlDbType.VarChar,120) ,      
                        new SqlParameter("@Mobile", SqlDbType.VarChar,30) ,      
                        new SqlParameter("@Phone", SqlDbType.VarChar,30) ,      
                        new SqlParameter("@Address", SqlDbType.VarChar,300) ,      
                        new SqlParameter("@PostCode", SqlDbType.VarChar,10) ,      
              
                                                  new SqlParameter("@Id", SqlDbType.Int,4)  
                            		
            };
            parameters[0].Value = model.CredentialsType;
            parameters[1].Value = model.CredentialsNumber;
            parameters[2].Value = model.Sex;
            parameters[3].Value = model.Birthday;
            parameters[4].Value = model.Nation;
            parameters[5].Value = model.PoliticalStatus;
            parameters[6].Value = model.Delflag;
            parameters[7].Value = model.CreateDate;
            parameters[8].Value = model.RegionId;
            parameters[9].Value = model.StudySection;
            parameters[10].Value = model.AccountId;
            parameters[11].Value = model.Organid;
            parameters[12].Value = model.Job;
            parameters[13].Value = model.WorkRank;
            parameters[14].Value = model.TeachDate;
            parameters[15].Value = model.TeachStudySection;
            parameters[16].Value = model.TeachSubject;
            parameters[17].Value = model.TeachGrade;
            parameters[18].Value = model.TraningType;
            parameters[19].Value = model.TraningObject;
            parameters[20].Value = model.EduLevel;
            parameters[21].Value = model.TeacherNo;
            parameters[22].Value = model.EduDegree;
            parameters[23].Value = model.EduMajor;
            parameters[24].Value = model.EduMajorOhter;
            parameters[25].Value = model.GraduateSchool;
            parameters[26].Value = model.GraduateTime;
            parameters[27].Value = model.GroupId;
            parameters[28].Value = model.RealName;
            parameters[29].Value = model.Mobile;
            parameters[30].Value = model.Phone;
            parameters[31].Value = model.Address;
            parameters[32].Value = model.PostCode;
            parameters[33].Value = model.Id;


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
            strSql.Append("delete from Member_BaseInfo ");
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
            strSql.Append("delete from Member_BaseInfo ");
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
        public Model.Member_BaseInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, CredentialsType, CredentialsNumber, Sex, Birthday, Nation, PoliticalStatus, Delflag, CreateDate, RegionId, StudySection, AccountId, Organid, Job, WorkRank, TeachDate, TeachStudySection, TeachSubject, TeachGrade, TraningType, TraningObject, EduLevel, TeacherNo, EduDegree, EduMajor, EduMajorOhter, GraduateSchool, GraduateTime, GroupId, RealName, Mobile, Phone, Address, PostCode  ");
            strSql.Append("  from Member_BaseInfo ");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[0].Value = Id;

            Model.Member_BaseInfo model = new Model.Member_BaseInfo();
            DataSet ds = MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CredentialsType"].ToString() != "")
                {
                    model.CredentialsType = int.Parse(ds.Tables[0].Rows[0]["CredentialsType"].ToString());
                }
                model.CredentialsNumber = ds.Tables[0].Rows[0]["CredentialsNumber"].ToString();
                if (ds.Tables[0].Rows[0]["Sex"].ToString() != "")
                {
                    model.Sex = int.Parse(ds.Tables[0].Rows[0]["Sex"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Nation"].ToString() != "")
                {
                    model.Nation = int.Parse(ds.Tables[0].Rows[0]["Nation"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PoliticalStatus"].ToString() != "")
                {
                    model.PoliticalStatus = int.Parse(ds.Tables[0].Rows[0]["PoliticalStatus"].ToString());
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
                if (ds.Tables[0].Rows[0]["RegionId"].ToString() != "")
                {
                    model.RegionId = int.Parse(ds.Tables[0].Rows[0]["RegionId"].ToString());
                }
                model.StudySection = ds.Tables[0].Rows[0]["StudySection"].ToString();
                if (ds.Tables[0].Rows[0]["AccountId"].ToString() != "")
                {
                    model.AccountId = int.Parse(ds.Tables[0].Rows[0]["AccountId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Organid"].ToString() != "")
                {
                    model.Organid = int.Parse(ds.Tables[0].Rows[0]["Organid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Job"].ToString() != "")
                {
                    model.Job = int.Parse(ds.Tables[0].Rows[0]["Job"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkRank"].ToString() != "")
                {
                    model.WorkRank = int.Parse(ds.Tables[0].Rows[0]["WorkRank"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TeachDate"].ToString() != "")
                {
                    model.TeachDate = DateTime.Parse(ds.Tables[0].Rows[0]["TeachDate"].ToString());
                }
                model.TeachStudySection = ds.Tables[0].Rows[0]["TeachStudySection"].ToString();
                model.TeachSubject = ds.Tables[0].Rows[0]["TeachSubject"].ToString();
                model.TeachGrade = ds.Tables[0].Rows[0]["TeachGrade"].ToString();
                if (ds.Tables[0].Rows[0]["TraningType"].ToString() != "")
                {
                    model.TraningType = int.Parse(ds.Tables[0].Rows[0]["TraningType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TraningObject"].ToString() != "")
                {
                    model.TraningObject = int.Parse(ds.Tables[0].Rows[0]["TraningObject"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EduLevel"].ToString() != "")
                {
                    model.EduLevel = int.Parse(ds.Tables[0].Rows[0]["EduLevel"].ToString());
                }
                model.TeacherNo = ds.Tables[0].Rows[0]["TeacherNo"].ToString();
                if (ds.Tables[0].Rows[0]["EduDegree"].ToString() != "")
                {
                    model.EduDegree = int.Parse(ds.Tables[0].Rows[0]["EduDegree"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EduMajor"].ToString() != "")
                {
                    model.EduMajor = int.Parse(ds.Tables[0].Rows[0]["EduMajor"].ToString());
                }
                model.EduMajorOhter = ds.Tables[0].Rows[0]["EduMajorOhter"].ToString();
                model.GraduateSchool = ds.Tables[0].Rows[0]["GraduateSchool"].ToString();
                if (ds.Tables[0].Rows[0]["GraduateTime"].ToString() != "")
                {
                    model.GraduateTime = DateTime.Parse(ds.Tables[0].Rows[0]["GraduateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GroupId"].ToString() != "")
                {
                    model.GroupId = int.Parse(ds.Tables[0].Rows[0]["GroupId"].ToString());
                }
                model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                model.PostCode = ds.Tables[0].Rows[0]["PostCode"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }



        public Model.Member_BaseInfo GetModelByAccountId(int AccountId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, CredentialsType, CredentialsNumber, Sex, Birthday, Nation, PoliticalStatus, Delflag, CreateDate, RegionId, StudySection, AccountId, Organid, Job, WorkRank, TeachDate, TeachStudySection, TeachSubject, TeachGrade, TraningType, TraningObject, EduLevel, TeacherNo, EduDegree, EduMajor, EduMajorOhter, GraduateSchool, GraduateTime, GroupId, RealName, Mobile, Phone, Address, PostCode  ");
            strSql.Append("  from Member_BaseInfo ");
            strSql.Append(" where ");
            strSql.Append(" AccountId = @AccountId and Delflag='false' ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@AccountId", SqlDbType.Int,4)  
                            };
            parameters[0].Value = AccountId;

            Model.Member_BaseInfo model = new Model.Member_BaseInfo();
            DataSet ds = MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CredentialsType"].ToString() != "")
                {
                    model.CredentialsType = int.Parse(ds.Tables[0].Rows[0]["CredentialsType"].ToString());
                }
                model.CredentialsNumber = ds.Tables[0].Rows[0]["CredentialsNumber"].ToString();
                if (ds.Tables[0].Rows[0]["Sex"].ToString() != "")
                {
                    model.Sex = int.Parse(ds.Tables[0].Rows[0]["Sex"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Nation"].ToString() != "")
                {
                    model.Nation = int.Parse(ds.Tables[0].Rows[0]["Nation"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PoliticalStatus"].ToString() != "")
                {
                    model.PoliticalStatus = int.Parse(ds.Tables[0].Rows[0]["PoliticalStatus"].ToString());
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
                if (ds.Tables[0].Rows[0]["RegionId"].ToString() != "")
                {
                    model.RegionId = int.Parse(ds.Tables[0].Rows[0]["RegionId"].ToString());
                }
                model.StudySection = ds.Tables[0].Rows[0]["StudySection"].ToString();
                if (ds.Tables[0].Rows[0]["AccountId"].ToString() != "")
                {
                    model.AccountId = int.Parse(ds.Tables[0].Rows[0]["AccountId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Organid"].ToString() != "")
                {
                    model.Organid = int.Parse(ds.Tables[0].Rows[0]["Organid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Job"].ToString() != "")
                {
                    model.Job = int.Parse(ds.Tables[0].Rows[0]["Job"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkRank"].ToString() != "")
                {
                    model.WorkRank = int.Parse(ds.Tables[0].Rows[0]["WorkRank"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TeachDate"].ToString() != "")
                {
                    model.TeachDate = DateTime.Parse(ds.Tables[0].Rows[0]["TeachDate"].ToString());
                }
                model.TeachStudySection = ds.Tables[0].Rows[0]["TeachStudySection"].ToString();
                model.TeachSubject = ds.Tables[0].Rows[0]["TeachSubject"].ToString();
                model.TeachGrade = ds.Tables[0].Rows[0]["TeachGrade"].ToString();
                if (ds.Tables[0].Rows[0]["TraningType"].ToString() != "")
                {
                    model.TraningType = int.Parse(ds.Tables[0].Rows[0]["TraningType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TraningObject"].ToString() != "")
                {
                    model.TraningObject = int.Parse(ds.Tables[0].Rows[0]["TraningObject"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EduLevel"].ToString() != "")
                {
                    model.EduLevel = int.Parse(ds.Tables[0].Rows[0]["EduLevel"].ToString());
                }
                model.TeacherNo = ds.Tables[0].Rows[0]["TeacherNo"].ToString();
                if (ds.Tables[0].Rows[0]["EduDegree"].ToString() != "")
                {
                    model.EduDegree = int.Parse(ds.Tables[0].Rows[0]["EduDegree"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EduMajor"].ToString() != "")
                {
                    model.EduMajor = int.Parse(ds.Tables[0].Rows[0]["EduMajor"].ToString());
                }
                model.EduMajorOhter = ds.Tables[0].Rows[0]["EduMajorOhter"].ToString();
                model.GraduateSchool = ds.Tables[0].Rows[0]["GraduateSchool"].ToString();
                if (ds.Tables[0].Rows[0]["GraduateTime"].ToString() != "")
                {
                    model.GraduateTime = DateTime.Parse(ds.Tables[0].Rows[0]["GraduateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GroupId"].ToString() != "")
                {
                    model.GroupId = int.Parse(ds.Tables[0].Rows[0]["GroupId"].ToString());
                }
                model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                model.PostCode = ds.Tables[0].Rows[0]["PostCode"].ToString();

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
            strSql.Append(" FROM Member_BaseInfo ");
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
            strSql.Append(" FROM Member_BaseInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString());
        }

        /// <summary>
        /// 获取指定班级的会员信息
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        public List<AddressList> GetMemberInfo(int TrainingId,int ClassId)
        {
            List<AddressList> list = new List<AddressList>();

            //讲师信息获取
            string sql = @"select distinct F.RealName as RealName,F.GraduateSchool as GraduateSchool,D.Title as Title,F.Mobile as Mobile,E.Email as Email,E.Pic as Pic   
                        from dbo.Traning_Teacher as A,dbo.Class_Detail as B,dbo.PlatformManager_Detail as C,dbo.Organ_Detail as D,dbo.Member_Account as E,dbo.Member_BaseInfo as F,dbo.Member_ClassRegister as G 
                        where A.TraningId = B.TraningId and A.PlatformManagerId = C.Id and C.OrganId = D.Id and C.AccountId = E.Id and E.Id = F.AccountId and G.ClassId = B.Id 
                        and A.Delflag = 0 and B.Delflag = 0 and C.Delflag = 0 and D.Delflag = 0 and E.Delflag =0 and F.Delflag =0  and G.Delflag = 0 and G.Status =4 and A.TraningId=@TrainingId and B.Id=@ClassId";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = TrainingId },
                new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = ClassId }
            };

            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    AddressList model = new AddressList();
                    model.RealName = reader["RealName"].ToString();
                    model.MemberType = 1;
                    model.GraduateSchool = reader["GraduateSchool"].ToString();
                    model.Title = reader["Title"].ToString();
                    model.Mobile = reader["Mobile"].ToString();
                    model.Email = reader["Email"].ToString();
                    model.Pic = reader["Pic"].ToString();
                    list.Add(model);
                }
            }

            //辅导员信息获取
            sql = @"select distinct E.RealName as RealName,E.GraduateSchool as GraduateSchool,C.Title as Title,E.Mobile as Mobile,D.Email as Email,D.Pic as Pic    
                    from dbo.Class_Detail as A,dbo.PlatformManager_Detail as B,dbo.Organ_Detail as C,dbo.Member_Account as D,dbo.Member_BaseInfo as E,dbo.Member_ClassRegister as F 
                    where A.Instructor = B.Id and B.AccountId = D.Id and B.OrganId = C.Id and D.Id = E.AccountId and F.ClassId = A.Id 
                    and A.Delflag = 0 and B.Delflag = 0 and C.Delflag = 0 and D.Delflag = 0 and F.Delflag = 0 and E.Delflag =0 and F.Status =4 and A.TraningId=@TrainingId AND A.Id=@ClassId";
            cmdParams = new SqlParameter[]{
                new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = TrainingId },
                new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = ClassId }
            };

            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    AddressList model = new AddressList();
                    model.RealName = reader["RealName"].ToString();
                    model.MemberType = 2;
                    model.GraduateSchool = reader["GraduateSchool"].ToString();
                    model.Title = reader["Title"].ToString();
                    model.Mobile = reader["Mobile"].ToString();
                    model.Email = reader["Email"].ToString();
                    model.Pic = reader["Pic"].ToString();
                    list.Add(model);
                }
            }

            //学员信息获取
            sql = @"select distinct E.RealName as RealName,E.GraduateSchool as GraduateSchool,C.Title as Title,E.Mobile as Mobile,D.Email as Email,D.Pic as Pic   
                    from dbo.Member_ClassRegister as A,dbo.Class_Detail as B,dbo.Organ_Detail as C,dbo.Member_Account as D,dbo.Member_BaseInfo as E  
                    where A.AccountId = D.Id and A.ClassId = B.Id and B.OrganId = C.Id and D.Id = E.AccountId  
                    and A.Delflag = 0 and B.Delflag = 0 and C.Delflag = 0 and D.Delflag = 0 and E.Delflag =0 and A.Status = 4 and B.TraningId=@TrainingId AND B.Id=@ClassId";
            cmdParams = new SqlParameter[]{
                new SqlParameter("@TrainingId", SqlDbType.Int, 4) { Value = TrainingId },
                new SqlParameter("@ClassId", SqlDbType.Int, 4) { Value = ClassId }
            };

            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                while (reader.Read())
                {
                    AddressList model = new AddressList();
                    model.RealName = reader["RealName"].ToString();
                    model.MemberType = 3;
                    model.GraduateSchool = reader["GraduateSchool"].ToString();
                    model.Title = reader["Title"].ToString();
                    model.Mobile = reader["Mobile"].ToString();
                    model.Email = reader["Email"].ToString();
                    model.Pic = reader["Pic"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
        #endregion

    }
}