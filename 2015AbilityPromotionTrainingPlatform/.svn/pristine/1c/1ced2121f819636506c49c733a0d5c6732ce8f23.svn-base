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
    //Organ_Detail
    public partial class Organ_DetailDAL
    {
        public Organ_DetailDAL()
        { }

        #region  Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(Id) from Organ_Detail";
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
            strSql.Append("select count(1) from Organ_Detail");
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
        public int Add(Model.Organ_Detail model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Organ_Detail(");
            strSql.Append("Title,ParentId,OType,Remark,Delflag,CreateDate,PartitionId");
            strSql.Append(") values (");
            strSql.Append("@Title,@ParentId,@OType,@Remark,@Delflag,@CreateDate,@PartitionId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Title", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,            
                        new SqlParameter("@OType", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Delflag", SqlDbType.Bit,1) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@PartitionId", SqlDbType.Int,4)             
              
            };
            parameters[0].Value = model.Title; parameters[1].Value = model.ParentId; parameters[2].Value = model.OType; parameters[3].Value = model.Remark; parameters[4].Value = model.Delflag; parameters[5].Value = model.CreateDate; parameters[6].Value = model.PartitionId;


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
        public bool Update(Model.Organ_Detail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Organ_Detail set ");

            strSql.Append(" Title = @Title , ");
            strSql.Append(" ParentId = @ParentId , ");
            strSql.Append(" OType = @OType , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" Delflag = @Delflag , ");
            strSql.Append(" CreateDate = @CreateDate , ");
            strSql.Append(" PartitionId = @PartitionId  ");
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
            strSql.Append("delete from Organ_Detail ");
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
            strSql.Append("delete from Organ_Detail ");
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
        public Model.Organ_Detail GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, Title, ParentId, OType, Remark, Delflag, CreateDate, PartitionId  ");
            strSql.Append("  from Organ_Detail ");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");

            SqlParameter[] parameters = {
                                            	new SqlParameter("@Id", SqlDbType.Int,4)  
                            };
            parameters[0].Value = Id;

            Model.Organ_Detail model = new Model.Organ_Detail();
            DataSet ds = MSEntLibSqlHelper.ExecuteDataSetBySql(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                if (ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OType"].ToString() != "")
                {
                    model.OType = int.Parse(ds.Tables[0].Rows[0]["OType"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
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
            strSql.Append(" FROM Organ_Detail ");
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
            strSql.Append(" FROM Organ_Detail ");
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

