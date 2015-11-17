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
    //Class_Notice
    public partial class Class_NoticeDAL
    {
        public Class_NoticeDAL()
        { }

        #region  Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(Id) from Class_Notice";
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
            strSql.Append("select count(1) from Class_Notice");
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
        public int Add(Model.Class_Notice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Class_Notice(");
            strSql.Append("ClassId,Title,Content,Creater,Display,Delflag,CreateDate");
            strSql.Append(") values (");
            strSql.Append("@ClassId,@Title,@Content,@Creater,@Display,@Delflag,@CreateDate");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@ClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Title", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@Content", SqlDbType.VarChar,8000) ,            
                        new SqlParameter("@Creater", SqlDbType.Int,4) ,            
                        new SqlParameter("@Display", SqlDbType.Bit,1) ,            
                        new SqlParameter("@Delflag", SqlDbType.Bit,1) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.ClassId;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Content;
            parameters[3].Value = model.Creater;
            parameters[4].Value = model.Display;
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
        public bool Update(Model.Class_Notice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Class_Notice set ");
            strSql.Append(" Title = @Title , ");
            strSql.Append(" Content = @Content , ");
            strSql.Append(" Creater = @Creater , ");
            strSql.Append(" Display = @Display ");
            strSql.Append(" where  ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4),
                                                new SqlParameter("@Title",SqlDbType.VarChar),
                                                new SqlParameter("@Content",SqlDbType.VarChar),
                                                new SqlParameter("@Creater",SqlDbType.VarChar),
                                                new SqlParameter("@Display",SqlDbType.Bit)
                            };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Content;
            parameters[3].Value = model.Creater;
            parameters[4].Value = model.Display;
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
            strSql.Append("delete from Class_Notice ");
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
            strSql.Append("delete from Class_Notice ");
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
        public Model.Class_Notice GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.Id, a.ClassId, a.Title, a.Content, a.Creater, a.Display, a.Delflag, a.CreateDate,b.RealName  ");
            strSql.Append("  from Class_Notice a left join Member_BaseInfo b on a.Creater = b.AccountId ");
            strSql.Append(" where ");
            strSql.Append(" a.Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[0].Value = Id;

            Model.Class_Notice model = new Model.Class_Notice();
            DataSet ds = MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClassId"].ToString() != "")
                {
                    model.ClassId = int.Parse(ds.Tables[0].Rows[0]["ClassId"].ToString());
                }
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
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
                if (ds.Tables[0].Rows[0]["RealName"].ToString() != "")
                {
                    model.OrganTitle = ds.Tables[0].Rows[0]["RealName"].ToString();
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
            strSql.Append(" FROM Class_Notice ");
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
            strSql.Append(" FROM Class_Notice ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString());
        }

        #endregion

        public bool Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" update dbo.Class_Notice set Delflag = 'true' where id=" + id + " ");
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

        public List<Class_Notice> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(1) from [dbo].[Class_Notice] a where a.Delflag = 0 ");
            if (!string.IsNullOrEmpty(where))
                sb.Append(where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select a.*,b.RealName,ROW_NUMBER() over (order by " + orderBy + ") as RowNum from [dbo].[Class_Notice] a left join dbo.Member_BaseInfo b on a.Creater = b.AccountId where a.Delflag = 0 ");
            if (!string.IsNullOrEmpty(where))
                sql.Append(where);
            sql.Append(") as T where RowNum between " + start + " and " + end);
            List<Class_Notice> list = new List<Class_Notice>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString()))
            {
                while (reader.Read())
                {
                    Class_Notice model = new Class_Notice();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        public void ConvertToModel(IDataReader reader, Class_Notice model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);

            if (reader["ClassId"] != DBNull.Value)
                model.ClassId = Convert.ToInt32(reader["ClassId"]);

            if (reader["Title"] != DBNull.Value)
                model.Title = reader["Title"].ToString();

            if (reader["Content"] != DBNull.Value)
                model.Content = reader["Content"].ToString();

            if (reader["Creater"] != DBNull.Value)
                model.Creater = Convert.ToInt32(reader["Creater"]);

            if (reader["Display"] != DBNull.Value)
                model.Display = Convert.ToBoolean(reader["Display"]);

            if (reader["Delflag"] != DBNull.Value)
                model.Display = Convert.ToBoolean(reader["Delflag"]);

            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);

            if (reader["RealName"] != DBNull.Value)
                model.OrganTitle = reader["RealName"].ToString();
        }
    }
}

