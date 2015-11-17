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
    //Class_HomeWork
    public partial class Class_HomeWorkDAL
    {
        public Class_HomeWorkDAL()
        { }

        #region  Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(Id) from Class_HomeWork";
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
            strSql.Append("select count(1) from Class_HomeWork");
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
        public int Add(Model.Class_HomeWork model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Class_HomeWork(");
            strSql.Append("CreateDate,HomeWorkId,ClassId,Content,AccountId,ParentReplyId,AttList,Display,Delflag");
            strSql.Append(") values (");
            strSql.Append("@CreateDate,@HomeWorkId,@ClassId,@Content,@AccountId,@ParentReplyId,@AttList,@Display,@Delflag");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@CreateDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@HomeWorkId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Content", SqlDbType.VarChar,2000) ,            
                        new SqlParameter("@AccountId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ParentReplyId", SqlDbType.Int,4) ,            
                        new SqlParameter("@AttList", SqlDbType.VarChar,2000) ,            
                        new SqlParameter("@Display", SqlDbType.Bit,1) ,            
                        new SqlParameter("@Delflag", SqlDbType.Bit,1)             
              
            };

            parameters[1].Value = model.CreateDate;
            parameters[2].Value = model.HomeWorkId;
            parameters[3].Value = model.ClassId;
            parameters[4].Value = model.Content;
            parameters[5].Value = model.AccountId;
            parameters[6].Value = model.ParentReplyId;
            parameters[7].Value = model.AttList;
            parameters[8].Value = model.Display;
            parameters[9].Value = model.Delflag;

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
        public bool Update(Model.Class_HomeWork model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Class_HomeWork set ");

            strSql.Append(" CreateDate = @CreateDate , ");
            strSql.Append(" HomeWorkId = @HomeWorkId , ");
            strSql.Append(" ClassId = @ClassId , ");
            strSql.Append(" Content = @Content , ");
            strSql.Append(" AccountId = @AccountId , ");
            strSql.Append(" ParentReplyId = @ParentReplyId , ");
            strSql.Append(" AttList = @AttList , ");
            strSql.Append(" Display = @Display , ");
            strSql.Append(" Delflag = @Delflag  ");
            strSql.Append(" where  ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[10].Value = model.Id;
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
            strSql.Append("delete from Class_HomeWork ");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[11].Value = Id;

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
            strSql.Append("delete from Class_HomeWork ");
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
        public Model.Class_HomeWork GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, CreateDate, HomeWorkId, ClassId, Content, AccountId, ParentReplyId, AttList, Display, Delflag  ");
            strSql.Append("  from Class_HomeWork ");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[12].Value = Id;

            Model.Class_HomeWork model = new Model.Class_HomeWork();
            DataSet ds = MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HomeWorkId"].ToString() != "")
                {
                    model.HomeWorkId = int.Parse(ds.Tables[0].Rows[0]["HomeWorkId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClassId"].ToString() != "")
                {
                    model.ClassId = int.Parse(ds.Tables[0].Rows[0]["ClassId"].ToString());
                }
                model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                if (ds.Tables[0].Rows[0]["AccountId"].ToString() != "")
                {
                    model.AccountId = int.Parse(ds.Tables[0].Rows[0]["AccountId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParentReplyId"].ToString() != "")
                {
                    model.ParentReplyId = int.Parse(ds.Tables[0].Rows[0]["ParentReplyId"].ToString());
                }
                model.AttList = ds.Tables[0].Rows[0]["AttList"].ToString();
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
            strSql.Append(" FROM Class_HomeWork ");
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
            strSql.Append(" FROM Class_HomeWork ");
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

