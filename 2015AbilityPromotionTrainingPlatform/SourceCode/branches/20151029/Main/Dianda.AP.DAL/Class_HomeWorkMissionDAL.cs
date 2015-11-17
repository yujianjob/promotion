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
    //Class_HomeWorkMission
    public partial class Class_HomeWorkMissionDAL
    {
        public Class_HomeWorkMissionDAL()
        { }

        #region  Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(Id) from Class_HomeWorkMission";
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
            strSql.Append("select count(1) from Class_HomeWorkMission");
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
        public int Add(Model.Class_HomeWorkMission model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Class_HomeWorkMission(");
            strSql.Append("Delflag,CreateDate,Title,ClassId,Content,StartDate,EndDate,AttList,Creater,Display");
            strSql.Append(") values (");
            strSql.Append("@Delflag,@CreateDate,@Title,@ClassId,@Content,@StartDate,@EndDate,@AttList,@Creater,@Display");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Delflag", SqlDbType.Bit,1) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Title", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Content", SqlDbType.VarChar,2000) ,            
                        new SqlParameter("@StartDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@EndDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@AttList", SqlDbType.VarChar,2000) ,            
                        new SqlParameter("@Creater", SqlDbType.Int,4) ,            
                        new SqlParameter("@Display", SqlDbType.Bit,1)             
              
            };
            parameters[0].Value = model.Delflag;
            parameters[1].Value = model.CreateDate;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.ClassId;
            parameters[4].Value = model.Content;
            parameters[5].Value = model.StartDate;
            parameters[6].Value = model.EndDate;
            parameters[7].Value = model.AttList;
            parameters[8].Value = model.Creater;
            parameters[9].Value = model.Display;



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
        public bool Update(Model.Class_HomeWorkMission model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Class_HomeWorkMission set ");

            strSql.Append(" Delflag = @Delflag , ");
            strSql.Append(" CreateDate = @CreateDate , ");
            strSql.Append(" Title = @Title , ");
            strSql.Append(" ClassId = @ClassId , ");
            strSql.Append(" Content = @Content , ");
            strSql.Append(" StartDate = @StartDate , ");
            strSql.Append(" EndDate = @EndDate , ");
            strSql.Append(" AttList = @AttList , ");
            strSql.Append(" Creater = @Creater , ");
            strSql.Append(" Display = @Display  ");
            strSql.Append(" where  ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Delflag", SqlDbType.Bit,1) ,      
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,      
                        new SqlParameter("@Title", SqlDbType.VarChar,50) ,      
                        new SqlParameter("@ClassId", SqlDbType.Int,4) ,      
                        new SqlParameter("@Content", SqlDbType.VarChar,2000) ,      
                        new SqlParameter("@StartDate", SqlDbType.DateTime) ,      
                        new SqlParameter("@EndDate", SqlDbType.DateTime) ,      
                        new SqlParameter("@AttList", SqlDbType.VarChar,2000) ,      
                        new SqlParameter("@Creater", SqlDbType.Int,4) ,      
                        new SqlParameter("@Display", SqlDbType.Bit,1) ,      
              
                                                  new SqlParameter("@Id", SqlDbType.Int,4)  
                            		
            };
            parameters[0].Value = model.Delflag;
            parameters[1].Value = model.CreateDate;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.ClassId;
            parameters[4].Value = model.Content;
            parameters[5].Value = model.StartDate;
            parameters[6].Value = model.EndDate;
            parameters[7].Value = model.AttList;
            parameters[8].Value = model.Creater;
            parameters[9].Value = model.Display;
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
            strSql.Append("delete from Class_HomeWorkMission ");
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
            strSql.Append("delete from Class_HomeWorkMission ");
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
        public Model.Class_HomeWorkMission GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, Delflag, CreateDate, Title, ClassId, Content, StartDate, EndDate, AttList, Creater, Display  ");
            strSql.Append("  from Class_HomeWorkMission ");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[0].Value = Id;

            Model.Class_HomeWorkMission model = new Model.Class_HomeWorkMission();
            DataSet ds = MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
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
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                if (ds.Tables[0].Rows[0]["ClassId"].ToString() != "")
                {
                    model.ClassId = int.Parse(ds.Tables[0].Rows[0]["ClassId"].ToString());
                }
                model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                if (ds.Tables[0].Rows[0]["StartDate"].ToString() != "")
                {
                    model.StartDate = DateTime.Parse(ds.Tables[0].Rows[0]["StartDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndDate"].ToString() != "")
                {
                    model.EndDate = DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
                }
                model.AttList = ds.Tables[0].Rows[0]["AttList"].ToString();
                if (ds.Tables[0].Rows[0]["Creater"].ToString() != "")
                {
                    model.Creater = int.Parse(ds.Tables[0].Rows[0]["Creater"].ToString());
                }
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
            strSql.Append(" FROM Class_HomeWorkMission ");
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
            strSql.Append(" FROM Class_HomeWorkMission ");
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