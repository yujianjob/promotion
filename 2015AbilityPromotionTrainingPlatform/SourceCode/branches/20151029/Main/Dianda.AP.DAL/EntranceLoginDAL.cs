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
    public class EntranceLoginDAL
    {
        /// <summary>
        /// 判断用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Login(string userName, string password, out int userId)
        {
            string sql = "select Id from Member_Account where Delflag=0 and Status=2 and UserName=@UserName collate Chinese_PRC_CS_AS and Password like @Password";
            SqlParameter[] cmdParams = new SqlParameter[]{
				new SqlParameter("@UserName", SqlDbType.VarChar, 50) { Value = userName },
				new SqlParameter("@Password", SqlDbType.VarChar, 100) { Value = password }
			};
            using (IDataReader reader = MSEntLibSqlHelper.ExecuteDataReaderBySql(sql, cmdParams))
            {
                if (reader.Read())
                {
                    userId = Convert.ToInt32(reader["Id"]);
                    return true;
                }
                else
                {
                    userId = 0;
                    return false;
                }
            }
        }

    }
}
