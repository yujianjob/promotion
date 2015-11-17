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
    //Class_TraningCommentResult
    public partial class Class_TraningCommentResultDAL
    {
        public Class_TraningCommentResultDAL()
        { }

        #region  Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(Id) from Class_TraningCommentResult";
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
            strSql.Append("select count(1) from Class_TraningCommentResult");
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
        public int Add(Model.Class_TraningCommentResult model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Class_TraningCommentResult(");
            strSql.Append("Verson,ClassId,Score,AccountId,Delflag,CreateDate");
            strSql.Append(") values (");
            strSql.Append("@Verson,@ClassId,@Score,@AccountId,@Delflag,@CreateDate");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Verson", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Score", SqlDbType.Int,4) ,            
                        new SqlParameter("@AccountId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Delflag", SqlDbType.Bit,1) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.Verson;
            parameters[1].Value = model.ClassId;
            parameters[2].Value = model.Score;
            parameters[3].Value = model.AccountId;
            parameters[4].Value = model.Delflag;
            parameters[5].Value = model.CreateDate;

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
        public bool Update(Model.Class_TraningCommentResult model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Class_TraningCommentResult set ");

            strSql.Append(" Verson = @Verson , ");
            strSql.Append(" ClassId = @ClassId , ");
            strSql.Append(" Score = @Score , ");
            strSql.Append(" AccountId = @AccountId , ");
            strSql.Append(" Delflag = @Delflag , ");
            strSql.Append(" CreateDate = @CreateDate  ");
            strSql.Append(" where  ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[7].Value = model.Id;
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
            strSql.Append("delete from Class_TraningCommentResult ");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[8].Value = Id;

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
            strSql.Append("delete from Class_TraningCommentResult ");
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
        public Model.Class_TraningCommentResult GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, Verson, ClassId, Score, AccountId, Delflag, CreateDate  ");
            strSql.Append("  from Class_TraningCommentResult ");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[9].Value = Id;

            Model.Class_TraningCommentResult model = new Model.Class_TraningCommentResult();
            DataSet ds = MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.Verson = ds.Tables[0].Rows[0]["Verson"].ToString();
                if (ds.Tables[0].Rows[0]["ClassId"].ToString() != "")
                {
                    model.ClassId = int.Parse(ds.Tables[0].Rows[0]["ClassId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Score"].ToString() != "")
                {
                    model.Score = int.Parse(ds.Tables[0].Rows[0]["Score"].ToString());
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
            strSql.Append(" FROM Class_TraningCommentResult ");
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
            strSql.Append(" FROM Class_TraningCommentResult ");
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

