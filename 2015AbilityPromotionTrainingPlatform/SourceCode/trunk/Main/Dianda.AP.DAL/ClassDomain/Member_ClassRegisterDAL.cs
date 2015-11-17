﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.DAL
{
    public partial class Member_ClassRegisterDAL
    {
        public System.Data.DataTable GetNoGroupper(int classId)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(@"SELECT t.*,'' AS ClassId_GroupId FROM dbo.Member_ClassRegister t
LEFT JOIN dbo.Member_Account m ON t.AccountId=m.id
                     where NOT EXISTS ( SELECT 1
                     FROM   dbo.Class_Group cg
                            JOIN dbo.Class_GroupMember cgm ON cg.Id = cgm.GroupId
                     WHERE  cg.ClassId = t.ClassId AND t.AccountId=cgm.AccountId
                            and cgm.Delflag=0 AND t.Status=4 AND t.ClassId=" + classId + ") AND t.Status=4 AND t.ClassId=" + classId);

            return MSEntLibSqlHelper.ExecuteDataSetBySql(sbSql.ToString()).Tables[0];

        }
    }
}
