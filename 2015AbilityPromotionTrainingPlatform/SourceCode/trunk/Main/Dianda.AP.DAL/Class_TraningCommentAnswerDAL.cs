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
    //Class_TraningCommentAnswer
    public partial class Class_TraningCommentAnswerDAL
    {
        public Class_TraningCommentAnswerDAL()
        { }

        #region  Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(Id) from Class_TraningCommentAnswer";
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
            strSql.Append("select count(1) from Class_TraningCommentAnswer");
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
        public int Add(Model.Class_TraningCommentAnswer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Class_TraningCommentAnswer(");
            strSql.Append("AnswerResult,Question,Credit,Chose,AccountId,Delflag,CreateDate");
            strSql.Append(") values (");
            strSql.Append("@AnswerResult,@Question,@Credit,@Chose,@AccountId,@Delflag,@CreateDate");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@AnswerResult", SqlDbType.Int,4) ,            
                        new SqlParameter("@Question", SqlDbType.Int,4) ,            
                        new SqlParameter("@Credit", SqlDbType.Int,4) ,            
                        new SqlParameter("@Chose", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@AccountId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Delflag", SqlDbType.Bit,1) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.AnswerResult;
            parameters[1].Value = model.Question;
            parameters[2].Value = model.Credit;
            parameters[3].Value = model.Chose;
            parameters[4].Value = model.AccountId;
            parameters[5].Value = model.Delflag;
            parameters[6].Value = model.CreateDate;

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
        public bool Update(Model.Class_TraningCommentAnswer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Class_TraningCommentAnswer set ");

            strSql.Append(" AnswerResult = @AnswerResult , ");
            strSql.Append(" Question = @Question , ");
            strSql.Append(" Credit = @Credit , ");
            strSql.Append(" Chose = @Chose , ");
            strSql.Append(" AccountId = @AccountId , ");
            strSql.Append(" Delflag = @Delflag , ");
            strSql.Append(" CreateDate = @CreateDate  ");
            strSql.Append(" where  ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[8].Value = model.Id;
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
            strSql.Append("delete from Class_TraningCommentAnswer ");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[9].Value = Id;

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
            strSql.Append("delete from Class_TraningCommentAnswer ");
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
        public Model.Class_TraningCommentAnswer GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, AnswerResult, Question, Credit, Chose, AccountId, Delflag, CreateDate  ");
            strSql.Append("  from Class_TraningCommentAnswer ");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[10].Value = Id;

            Model.Class_TraningCommentAnswer model = new Model.Class_TraningCommentAnswer();
            DataSet ds = MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AnswerResult"].ToString() != "")
                {
                    model.AnswerResult = int.Parse(ds.Tables[0].Rows[0]["AnswerResult"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Question"].ToString() != "")
                {
                    model.Question = int.Parse(ds.Tables[0].Rows[0]["Question"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Credit"].ToString() != "")
                {
                    model.Credit = int.Parse(ds.Tables[0].Rows[0]["Credit"].ToString());
                }
                model.Chose = ds.Tables[0].Rows[0]["Chose"].ToString();
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
            strSql.Append(" FROM Class_TraningCommentAnswer ");
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
            strSql.Append(" FROM Class_TraningCommentAnswer ");
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

