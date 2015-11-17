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
    //Member_ContentAnswerResult
    public partial class Member_ContentAnswerResultDAL
    {
        public Member_ContentAnswerResultDAL()
        { }

        #region  Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(Id) from Member_ContentAnswerResult";
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
            strSql.Append("select count(1) from Member_ContentAnswerResult");
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
        public int Add(Model.Member_ContentAnswerResult model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Member_ContentAnswerResult(");
            strSql.Append("AccountId,Delflag,CreateDate,Verson,UnitContent,ClassId,Score,QuestionCnt,RightAnswer,WrongAnswer,Result");
            strSql.Append(") values (");
            strSql.Append("@AccountId,@Delflag,@CreateDate,@Verson,@UnitContent,@ClassId,@Score,@QuestionCnt,@RightAnswer,@WrongAnswer,@Result");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@AccountId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Delflag", SqlDbType.Bit,1) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Verson", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@UnitContent", SqlDbType.Int,4) ,            
                        new SqlParameter("@ClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Score", SqlDbType.Float,8) ,            
                        new SqlParameter("@QuestionCnt", SqlDbType.Int,4) ,            
                        new SqlParameter("@RightAnswer", SqlDbType.Int,4) ,            
                        new SqlParameter("@WrongAnswer", SqlDbType.Int,4) ,            
                        new SqlParameter("@Result", SqlDbType.Bit,1)             
              
            };
            parameters[0].Value = model.AccountId;
            parameters[1].Value = model.Delflag;
            parameters[2].Value = model.CreateDate;
            parameters[3].Value = model.Verson;
            parameters[4].Value = model.UnitContent;
            parameters[5].Value = model.ClassId;
            parameters[6].Value = model.Score;
            parameters[7].Value = model.QuestionCnt;
            parameters[8].Value = model.RightAnswer;
            parameters[9].Value = model.WrongAnswer;
            parameters[10].Value = model.Result;



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
        public bool Update(Model.Member_ContentAnswerResult model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Member_ContentAnswerResult set ");

            strSql.Append(" AccountId = @AccountId , ");
            strSql.Append(" Delflag = @Delflag , ");
            strSql.Append(" CreateDate = @CreateDate , ");
            strSql.Append(" Verson = @Verson , ");
            strSql.Append(" UnitContent = @UnitContent , ");
            strSql.Append(" ClassId = @ClassId , ");
            strSql.Append(" Score = @Score , ");
            strSql.Append(" QuestionCnt = @QuestionCnt , ");
            strSql.Append(" RightAnswer = @RightAnswer , ");
            strSql.Append(" WrongAnswer = @WrongAnswer , ");
            strSql.Append(" Result = @Result  ");
            strSql.Append(" where  ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@AccountId", SqlDbType.Int,4) ,      
                        new SqlParameter("@Delflag", SqlDbType.Bit,1) ,      
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,      
                        new SqlParameter("@Verson", SqlDbType.VarChar,20) ,      
                        new SqlParameter("@UnitContent", SqlDbType.Int,4) ,      
                        new SqlParameter("@ClassId", SqlDbType.Int,4) ,      
                        new SqlParameter("@Score", SqlDbType.Float,8) ,      
                        new SqlParameter("@QuestionCnt", SqlDbType.Int,4) ,      
                        new SqlParameter("@RightAnswer", SqlDbType.Int,4) ,      
                        new SqlParameter("@WrongAnswer", SqlDbType.Int,4) ,      
                        new SqlParameter("@Result", SqlDbType.Bit,1) ,      
              
                                                  new SqlParameter("@Id", SqlDbType.Int,4)  
                            		
            };
            parameters[0].Value = model.AccountId;
            parameters[1].Value = model.Delflag;
            parameters[2].Value = model.CreateDate;
            parameters[3].Value = model.Verson;
            parameters[4].Value = model.UnitContent;
            parameters[5].Value = model.ClassId;
            parameters[6].Value = model.Score;
            parameters[7].Value = model.QuestionCnt;
            parameters[8].Value = model.RightAnswer;
            parameters[9].Value = model.WrongAnswer;
            parameters[10].Value = model.Result;
            parameters[11].Value = model.Id;


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
            strSql.Append("delete from Member_ContentAnswerResult ");
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
            strSql.Append("delete from Member_ContentAnswerResult ");
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
        public Model.Member_ContentAnswerResult GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, AccountId, Delflag, CreateDate, Verson, UnitContent, ClassId, Score, QuestionCnt, RightAnswer, WrongAnswer, Result  ");
            strSql.Append("  from Member_ContentAnswerResult ");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[0].Value = Id;

            Model.Member_ContentAnswerResult model = new Model.Member_ContentAnswerResult();
            DataSet ds = MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AccountId"].ToString() != "")
                {
                    model.AccountId = int.Parse(ds.Tables[0].Rows[0]["AccountId"].ToString());
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
                model.Verson = ds.Tables[0].Rows[0]["Verson"].ToString();
                if (ds.Tables[0].Rows[0]["UnitContent"].ToString() != "")
                {
                    model.UnitContent = int.Parse(ds.Tables[0].Rows[0]["UnitContent"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClassId"].ToString() != "")
                {
                    model.ClassId = int.Parse(ds.Tables[0].Rows[0]["ClassId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Score"].ToString() != "")
                {
                    model.Score = decimal.Parse(ds.Tables[0].Rows[0]["Score"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QuestionCnt"].ToString() != "")
                {
                    model.QuestionCnt = int.Parse(ds.Tables[0].Rows[0]["QuestionCnt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RightAnswer"].ToString() != "")
                {
                    model.RightAnswer = int.Parse(ds.Tables[0].Rows[0]["RightAnswer"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WrongAnswer"].ToString() != "")
                {
                    model.WrongAnswer = int.Parse(ds.Tables[0].Rows[0]["WrongAnswer"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Result"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Result"].ToString() == "1") || (ds.Tables[0].Rows[0]["Result"].ToString().ToLower() == "true"))
                    {
                        model.Result = true;
                    }
                    else
                    {
                        model.Result = false;
                    }
                }

                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 取得一条记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public Member_ContentAnswerResult GetModel(string where)
        {
            string sql = "select top 1 * from [dbo].[Member_ContentAnswerResult] Where 1 = 1";
            if (!string.IsNullOrEmpty(where))
                sql += " and " + where;
            sql += " order by Score desc";
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql))
            {
                if (reader.Read())
                {
                    Member_ContentAnswerResult model = new Member_ContentAnswerResult();
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Member_ContentAnswerResult ");
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
            strSql.Append(" FROM Member_ContentAnswerResult ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString());
        }

        #endregion

        private void ConvertToModel(IDataReader reader, Member_ContentAnswerResult model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);
            if (reader["Verson"] != DBNull.Value)
                model.Verson = Convert.ToString(reader["Verson"]);
            if (reader["UnitContent"] != DBNull.Value)
                model.UnitContent = Convert.ToInt32(reader["UnitContent"]);
            if (reader["ClassId"] != DBNull.Value)
                model.ClassId = Convert.ToInt32(reader["ClassId"]);
            if (reader["Score"] != DBNull.Value)
                model.Score = Convert.ToDecimal(reader["Score"]);
            if (reader["QuestionCnt"] != DBNull.Value)
                model.QuestionCnt = Convert.ToInt32(reader["QuestionCnt"]);
            if (reader["RightAnswer"] != DBNull.Value)
                model.RightAnswer = Convert.ToInt32(reader["RightAnswer"]);
            if (reader["WrongAnswer"] != DBNull.Value)
                model.WrongAnswer = Convert.ToInt32(reader["WrongAnswer"]);
            if (reader["Result"] != DBNull.Value)
                model.Result = Convert.ToBoolean(reader["Result"]);
            if (reader["AccountId"] != DBNull.Value)
                model.AccountId = Convert.ToInt32(reader["AccountId"]);
            if (reader["Delflag"] != DBNull.Value)
                model.Delflag = Convert.ToBoolean(reader["Delflag"]);
            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
        }

    }
}