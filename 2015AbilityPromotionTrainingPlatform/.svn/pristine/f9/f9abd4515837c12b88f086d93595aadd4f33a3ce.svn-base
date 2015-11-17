using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dianda.AP.DAL
{
    //Training_Plan
    public partial class Training_PlanDAL
    {
        public Training_PlanDAL()
        { }

        #region  Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(Id) from Training_Plan";
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
            strSql.Append("select count(1) from Training_Plan");
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
        public int Add(Model.Training_Plan model)
        {
            if (model.IsOpen)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" update Training_Plan set IsOpen = 0 where PartitionId=" + model.PartitionId + " ");
                MSEntLibSqlHelper.ExecuteNonQueryBySql(sb.ToString());
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Training_Plan(");
            strSql.Append("PartitionId,Title,IsOpen,Sort,Display,Delflag,CreateDate,SignUpStartTime,SignUpEndTime,OpenClassFrom,OpenClassTo");
            strSql.Append(") values (");
            strSql.Append("@PartitionId,@Title,@IsOpen,@Sort,@Display,@Delflag,@CreateDate,@SignUpStartTime,@SignUpEndTime,@OpenClassFrom,@OpenClassTo");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@PartitionId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Title", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@IsOpen", SqlDbType.Bit,1) ,            
                        new SqlParameter("@Sort", SqlDbType.Int,4) ,            
                        new SqlParameter("@Display", SqlDbType.Bit,1) ,            
                        new SqlParameter("@Delflag", SqlDbType.Bit,1) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime),
                        new SqlParameter("@SignUpStartTime",SqlDbType.DateTime),
                        new SqlParameter("@SignUpEndTime",SqlDbType.DateTime),
                        new SqlParameter("@OpenClassFrom",SqlDbType.DateTime),
                        new SqlParameter("@OpenClassTo",SqlDbType.DateTime)
              
            };
            parameters[0].Value = model.PartitionId; parameters[1].Value = model.Title; parameters[2].Value = model.IsOpen; parameters[3].Value = model.Sort; parameters[4].Value = model.Display; parameters[5].Value = model.Delflag; parameters[6].Value = model.CreateDate; parameters[7].Value = model.SignUpStartTime; parameters[8].Value = model.SignUpEndTime; parameters[9].Value = model.OpenClassFrom; parameters[10].Value = model.OpenClassTo;


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
        public bool Update(Model.Training_Plan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Training_Plan set ");

            strSql.Append(" PartitionId = @PartitionId , ");
            strSql.Append(" Title = @Title , ");
            strSql.Append(" IsOpen = @IsOpen , ");
            strSql.Append(" Sort = @Sort , ");
            strSql.Append(" Display = @Display , ");
            strSql.Append(" Delflag = @Delflag , ");
            strSql.Append(" CreateDate = @CreateDate  ");
            strSql.Append(" where  ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[1].Value = model.Id;
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
            strSql.Append("delete from Training_Plan ");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[2].Value = Id;

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
            strSql.Append("delete from Training_Plan ");
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
        public Model.Training_Plan GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, PartitionId, Title, IsOpen, Sort, Display, Delflag, CreateDate,SignUpStartTime,SignUpEndTime,OpenClassFrom,OpenClassTo  ");
            strSql.Append("  from Training_Plan ");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[0].Value = Id;

            Model.Training_Plan model = new Model.Training_Plan();
            DataSet ds = MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PartitionId"].ToString() != "")
                {
                    model.PartitionId = int.Parse(ds.Tables[0].Rows[0]["PartitionId"].ToString());
                }
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                if (ds.Tables[0].Rows[0]["IsOpen"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsOpen"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsOpen"].ToString().ToLower() == "true"))
                    {
                        model.IsOpen = true;
                    }
                    else
                    {
                        model.IsOpen = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
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
            strSql.Append(" FROM Training_Plan ");
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
            strSql.Append(" FROM Training_Plan ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString());
        }

        #endregion


        public List<Training_Plan> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException();
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(1) from dbo.Training_Plan where Delflag = 0 and DisPlay = 1 ");
            if (!string.IsNullOrEmpty(where))
                sb.Append(where);
            recordCount = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sb.ToString()));
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select *,ROW_NUMBER() over (order by " + orderBy + ") as RowNum from dbo.Training_Plan  where Delflag = 0 and DisPlay = 1 ");
            if (!string.IsNullOrEmpty(where))
                sql.Append(where);
            sql.Append(")as T where RowNum between @start and @end");
            SqlParameter[] para ={
                                    new SqlParameter("@start",start),
                                    new SqlParameter("@end",end)
                                 };
            List<Training_Plan> list = new List<Training_Plan>();
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql.ToString(),para))
            {
                while (reader.Read())
                {
                    Training_Plan model = new Training_Plan();
                    ConvertToModel(reader, model);
                    list.Add(model);
                }
            }
            return list;
        }

        public bool SemesterUpd(Training_Plan model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" update Training_Plan Set Title=@Title,Display=@Display, ");
            sql.Append(" SignUpStartTime=@SignUpStartTime,SignUpEndTime=@SignUpEndTime,OpenClassFrom=@OpenClassFrom,OpenClassTo=@OpenClassTo ");
            sql.Append(" where id=@id ");
            SqlParameter[] para ={
                                    new SqlParameter("@Title",SqlDbType.VarChar),
                                    new SqlParameter("@Display",SqlDbType.Bit),
                                    new SqlParameter("@id",SqlDbType.Int),
                                    new SqlParameter("@SignUpStartTime",SqlDbType.DateTime),
                                    new SqlParameter("@SignUpEndTime",SqlDbType.DateTime),
                                    new SqlParameter("@OpenClassFrom",SqlDbType.DateTime),
                                    new SqlParameter("@OpenClassTo",SqlDbType.Date)
                                 };
            para[0].Value = model.Title;
            para[1].Value = model.Display;
            para[2].Value = model.Id;
            para[3].Value = model.SignUpStartTime;
            para[4].Value = model.SignUpEndTime;
            para[5].Value = model.OpenClassFrom;
            para[6].Value = model.OpenClassTo;
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

        public bool Disable(int id, int type, int PartitionId)
        {
            StringBuilder sql = new StringBuilder();
            if (type == 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" update Training_Plan set IsOpen = 0 where PartitionId=" + PartitionId + " ");
                MSEntLibSqlHelper.ExecuteNonQueryBySql(sb.ToString());
            }
            sql.Append(" update Training_Plan set IsOpen = " + type + " where id=" + id + " and PartitionId=" + PartitionId + " ");
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


        public bool SemesterDel(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" update Training_Plan set Delflag = 1 where id=" + id + " ");
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

        public bool YzPlanName(string Title,int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select count(1) from Training_Plan where Title = '" + Title + "' and Id <> '" + id + "' and Delflag = 0 ");
            int i = Convert.ToInt32(MSEntLibSqlHelper.ExecuteScalarBySql(sql.ToString()));
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void ConvertToModel(IDataReader reader, Training_Plan model)
        {
            if (reader["Id"] != DBNull.Value)
                model.Id = Convert.ToInt32(reader["Id"]);

            if (reader["Title"] != DBNull.Value)
                model.Title = reader["Title"].ToString();

            if (reader["PartitionId"] != DBNull.Value)
                model.PartitionId = Convert.ToInt32(reader["PartitionId"]);

            if (reader["IsOpen"] != DBNull.Value)
                model.IsOpen = Convert.ToBoolean(reader["IsOpen"]);

            if (reader["Sort"] != DBNull.Value)
                model.Sort = Convert.ToInt32(reader["Sort"]);

            if (reader["Display"] != DBNull.Value)
                model.Display = Convert.ToBoolean(reader["Display"]);

            if (reader["Delflag"] != DBNull.Value)
                model.Delflag = Convert.ToBoolean(reader["Delflag"]);

            if (reader["CreateDate"] != DBNull.Value)
                model.CreateDate = Convert.ToDateTime(reader["CreateDate"]);

            if (reader["SignUpStartTime"] != DBNull.Value)
                model.SignUpStartTime = Convert.ToDateTime(reader["SignUpStartTime"]);

            if (reader["SignUpEndTime"] != DBNull.Value)
                model.SignUpEndTime = Convert.ToDateTime(reader["SignUpEndTime"]);

            if (reader["OpenClassFrom"] != DBNull.Value)
                model.OpenClassFrom = Convert.ToDateTime(reader["OpenClassFrom"]);

            if (reader["OpenClassTo"] != DBNull.Value)
                model.OpenClassTo = Convert.ToDateTime(reader["OpenClassTo"]);
        }

    }
}

