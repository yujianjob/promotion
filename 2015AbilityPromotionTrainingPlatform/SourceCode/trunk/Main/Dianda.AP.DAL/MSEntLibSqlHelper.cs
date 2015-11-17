using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Dianda.AP.DAL
{
    public class MSEntLibSqlHelper
    {
        //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
        protected static Database db = new DatabaseProviderFactory().Create("ConnectionString");
        //protected static Database db = DatabaseFactory.CreateDatabase("ConnectionString");

        #region Prepare Parameters

        /// <summary>
        /// 加载参数
        /// </summary>
        public static void BuildDBParameter(Database db, DbCommand dbCommand, params IDataParameter[] cmdParms)
        {
            foreach (SqlParameter sp in cmdParms)
            {
                if (sp.Direction == ParameterDirection.InputOutput || sp.Direction == ParameterDirection.Output)
                {
                    db.AddOutParameter(dbCommand, sp.ParameterName, sp.DbType, sp.Size);
                }
                else
                {
                    db.AddInParameter(dbCommand, sp.ParameterName, sp.DbType, sp.Value);
                }
            }
        }

        /// <summary>
        /// 为储存过程加载输出变量参数
        /// </summary>
        /// <param name="db">database</param>
        /// <param name="dbCommand">db command</param>
        /// <param name="cmdParms">参数数组</param>
        public static void BuildDBParameterForReturnValue(Database db, DbCommand dbCommand, params IDataParameter[] cmdParms)
        {
            foreach (SqlParameter sp in cmdParms)
            {
                db.AddParameter(dbCommand, sp.ParameterName, sp.DbType, sp.Size, ParameterDirection.Output, false, 0, 0, sp.SourceColumn, DataRowVersion.Current, DBNull.Value);
            }
        }

        #endregion

        #region Check Exist

        /// <summary>
        /// 检测一个记录是否存在(SQL语句方式)
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool Exists(string strSql)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            object obj = db.ExecuteScalar(dbCommand);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        ///  检测一个记录是否存在(SqlParameter语句方式)
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static bool Exists(string strSql, params IDataParameter[] cmdParms)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);
            object obj = db.ExecuteScalar(dbCommand);
            int cmdresult;

            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region executedatareader

        public static IDataReader ExecuteDataReaderBySql(string strSql)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            return db.ExecuteReader(dbCommand);
        }

        public static IDataReader ExecuteDataReaderBySql(string strSql, params IDataParameter[] cmdParms)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);
            return db.ExecuteReader(dbCommand);
        }

        public static IDataReader ExecuteDataReaderByStoredProc(string storedProcName)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            return db.ExecuteReader(dbCommand);
        }

        public static IDataReader ExecuteDataReaderByStoredProc(string storedProcName, params IDataParameter[] cmdParms)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            BuildDBParameter(db, dbCommand, cmdParms);
            return db.ExecuteReader(dbCommand);
        }

        public static IDataReader ExecuteDataReaderByStoredProc(string storedProcName, IDataParameter[] cmdParms, params IDataParameter[] cmdOutputParms)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            BuildDBParameter(db, dbCommand, cmdParms);
            BuildDBParameterForReturnValue(db, dbCommand, cmdOutputParms);
            return db.ExecuteReader(dbCommand);
        }

        #endregion

        #region executenonquery

        /// <summary>
        /// 执行SQL（SQL）
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static int ExecuteNonQueryBySql(string strSql)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 执行SQL（SQL）
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static int ExecuteNonQueryBySql(string strSql, params IDataParameter[] cmdParms)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);

            int result = db.ExecuteNonQuery(dbCommand);

            for (int i = 0; i < cmdParms.Length; i++)
            {
                if (cmdParms[i].Direction == ParameterDirection.Output || cmdParms[i].Direction == ParameterDirection.InputOutput)
                {
                    cmdParms[i].Value = dbCommand.Parameters[i].Value;
                }
            }

            return result;
        }

        public static int ExecuteNonQueryBySqlTran(string strSql)
        {
            using (DbConnection dbconn = db.CreateConnection())
            {
                int rst = 0;
                dbconn.Open();
                DbTransaction dbtran = dbconn.BeginTransaction();
                try
                {
                    DbCommand dbCommand = db.GetSqlStringCommand(strSql);
                    rst = db.ExecuteNonQuery(dbCommand);
                    dbtran.Commit();
                }
                catch
                {
                    dbtran.Rollback();
                    rst = 0;
                }
                finally
                {
                    dbconn.Close();
                }
                return rst;
            }
        }

        public static int ExecuteNonQueryBySqlTran(string strSql, params IDataParameter[] cmdParms)
        {
            using (DbConnection dbconn = db.CreateConnection())
            {
                int rst = 0;
                dbconn.Open();
                DbTransaction dbtran = dbconn.BeginTransaction();
                try
                {
                    DbCommand dbCommand = db.GetSqlStringCommand(strSql);
                    BuildDBParameter(db, dbCommand, cmdParms);
                    rst = db.ExecuteNonQuery(dbCommand);
                    dbtran.Commit();
                }
                catch
                {
                    dbtran.Rollback();
                    rst = 0;
                }
                finally
                {
                    dbconn.Close();
                }
                return rst;
            }
        }

        /// <summary>
        /// 返回影响的记录数
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteNonQueryByProcecdure(string storedProcName)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 返回影响的记录数
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parms">输入参数</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteNonQueryByProcecdure(string storedProcName, IDataParameter[] parms)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            BuildDBParameter(db, dbCommand, parms);
            return db.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region executescalar

        public static object ExecuteScalarBySql(string strSql)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            return db.ExecuteScalar(dbCommand);
        }

        public static object ExecuteScalarBySql(string strSql, params IDataParameter[] cmdParms)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);
            return db.ExecuteScalar(dbCommand);
        }

        /// <summary>
        /// 返回处理结果的第一行第一列的值
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <returns></returns>
        public static object ExecuteScalarByProcecdure(string storedProcName)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            return db.ExecuteScalar(dbCommand);
        }

        /// <summary>
        /// 返回处理结果的第一行第一列的值
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parms">输入参数</param>
        /// <returns></returns>
        public static object ExecuteScalarByProcecdure(string storedProcName, IDataParameter[] parms)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            BuildDBParameter(db, dbCommand, parms);
            return db.ExecuteScalar(dbCommand);
        }


        #endregion

        #region executedataset

        public static DataSet ExecuteDataSetBySql(string strSql)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            return db.ExecuteDataSet(dbCommand);
        }

        public static DataSet ExecuteDataSetBySql(string strSql, IDataParameter[] cmdParms)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);
            return db.ExecuteDataSet(dbCommand);
        }

        public static DataSet ExecuteDataSetByStoreProcecdure(string storedProcName)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            return db.ExecuteDataSet(dbCommand);
        }

        public static DataSet ExecuteDataSetByStoreProcecdure(string storedProcName, IDataParameter[] cmdParms)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            BuildDBParameter(db, dbCommand, cmdParms);
            return db.ExecuteDataSet(dbCommand);
        }

        #endregion

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static int ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (DbConnection dbconn = db.CreateConnection())
            {
                int rst = 0;
                dbconn.Open();
                DbTransaction dbtran = dbconn.BeginTransaction();
                try
                {
                    //执行语句
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            DbCommand dbCommand = db.GetSqlStringCommand(strsql);
                            rst += db.ExecuteNonQuery(dbCommand);
                        }
                    }
                    //执行存储过程
                    //db.ExecuteNonQuery(CommandType.StoredProcedure, "InserOrders");
                    //db.ExecuteDataSet(CommandType.StoredProcedure, "UpdateProducts");
                    dbtran.Commit();
                }
                catch
                {
                    dbtran.Rollback();
                    rst = 0;
                }
                finally
                {
                    dbconn.Close();
                }
                return rst;
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static int ExecuteSqlTran(IList<string> SQLStringList)
        {
            using (DbConnection dbconn = db.CreateConnection())
            {
                int rst = 0;
                dbconn.Open();
                DbTransaction dbtran = dbconn.BeginTransaction();
                try
                {
                    //执行语句
                    foreach (string sql in SQLStringList)
                    {
                        if (sql.Trim().Length > 1)
                        {
                            DbCommand dbCommand = db.GetSqlStringCommand(sql);
                            rst += db.ExecuteNonQuery(dbCommand);
                        }
                    }
                    //for (int n = 0; n < SQLStringList.Count; n++)
                    //{
                    //    string strsql = SQLStringList[n].ToString();
                    //    if (strsql.Trim().Length > 1)
                    //    {
                    //        DbCommand dbCommand = db.GetSqlStringCommand(strsql);
                    //        rst += db.ExecuteNonQuery(dbCommand);
                    //    }
                    //}
                    //执行存储过程
                    //db.ExecuteNonQuery(CommandType.StoredProcedure, "InserOrders");
                    //db.ExecuteDataSet(CommandType.StoredProcedure, "UpdateProducts");
                    dbtran.Commit();
                }
                catch
                {
                    dbtran.Rollback();
                    rst = 0;
                }
                finally
                {
                    dbconn.Close();
                }
                return rst;
            }
        }

    }
}
