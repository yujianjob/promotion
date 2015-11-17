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
    //Class_GroupMember
    public partial class Class_GroupMemberDAL
    {
        public Class_GroupMemberDAL()
        { }

        #region  Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(Id) from Class_GroupMember";
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
        public bool Exists(int GroupId, int AccountId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Class_GroupMember");
            strSql.Append(" where ");
            strSql.Append(" GroupId = @GroupId and  ");
            strSql.Append(" AccountId = @AccountId  ");
            SqlParameter[] parameters = {
                                            	new SqlParameter("@GroupId", SqlDbType.Int,4) , 
                                                            	new SqlParameter("@AccountId", SqlDbType.Int,4)  
                            };
            parameters[0].Value = GroupId; parameters[1].Value = AccountId;
            return MSEntLibSqlHelper.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Class_GroupMember model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Class_GroupMember(");
            strSql.Append("GroupId,AccountId,Delflag,CreateDate");
            strSql.Append(") values (");
            strSql.Append("@GroupId,@AccountId,@Delflag,@CreateDate");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@GroupId", SqlDbType.Int,4) ,            
                        new SqlParameter("@AccountId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Delflag", SqlDbType.Bit,1) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime)             
              
            };
            parameters[0].Value = model.GroupId;
            parameters[1].Value = model.AccountId;
            parameters[2].Value = model.Delflag;
            parameters[3].Value = model.CreateDate;
            return MSEntLibSqlHelper.ExecuteNonQueryBySql(strSql.ToString(), parameters);


        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Class_GroupMember model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Class_GroupMember set ");

            strSql.Append(" GroupId = @GroupId , ");
            strSql.Append(" AccountId = @AccountId , ");
            strSql.Append(" Delflag = @Delflag , ");
            strSql.Append(" CreateDate = @CreateDate  ");
            strSql.Append(" where  ");
            strSql.Append(" GroupId = @GroupId and  ");
            strSql.Append(" AccountId = @AccountId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@GroupId", SqlDbType.Int,4) ,      
                        new SqlParameter("@AccountId", SqlDbType.Int,4) ,      
                        new SqlParameter("@Delflag", SqlDbType.Bit,1) ,      
                        new SqlParameter("@CreateDate", SqlDbType.DateTime)
                            		
            };
            parameters[0].Value = model.GroupId;
            parameters[1].Value = model.AccountId;
            parameters[2].Value = model.Delflag;
            parameters[3].Value = model.CreateDate;


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
        public bool Delete(int GroupId, int AccountId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Class_GroupMember ");
            strSql.Append(" where ");
            strSql.Append(" GroupId = @GroupId and  ");
            strSql.Append(" AccountId = @AccountId  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@GroupId", SqlDbType.Int,4) , 
                                                            	new SqlParameter("@AccountId", SqlDbType.Int,4)  
                            };
            parameters[0].Value = GroupId; parameters[1].Value = AccountId;

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
        /// 得到一个对象实体
        /// </summary>
        public Model.Class_GroupMember GetModel(int GroupId, int AccountId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GroupId, AccountId, Delflag, CreateDate  ");
            strSql.Append("  from Class_GroupMember ");
            strSql.Append(" where ");
            strSql.Append(" GroupId = @GroupId and  ");
            strSql.Append(" AccountId = @AccountId  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@GroupId", SqlDbType.Int,4) , 
                                                            	new SqlParameter("@AccountId", SqlDbType.Int,4)  
                            };
            parameters[0].Value = GroupId; parameters[1].Value = AccountId;

            Model.Class_GroupMember model = new Model.Class_GroupMember();
            DataSet ds = MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["GroupId"].ToString() != "")
                {
                    model.GroupId = int.Parse(ds.Tables[0].Rows[0]["GroupId"].ToString());
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
            strSql.Append(" FROM Class_GroupMember ");
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
            strSql.Append(" FROM Class_GroupMember ");
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