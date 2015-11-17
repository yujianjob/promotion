using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.DAL
{
    public partial class Organ_DetailDAL
    {
        public System.Data.DataTable GetShiOrganDetailList()
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(@"SELECT  *
FROM    dbo.Organ_Detail a
WHERE   ParentId = ( SELECT id
                     FROM   dbo.Organ_Detail b
                     WHERE  b.OType = 4
                   )
        AND a.OType IN ( 1, 2, 3 )
        AND a.OType != 5
UNION
SELECT  *
FROM    dbo.Organ_Detail c
WHERE   c.ParentId IN ( SELECT  a.Id
                        FROM    dbo.Organ_Detail a
                        WHERE   ParentId = ( SELECT id
                                             FROM   dbo.Organ_Detail b
                                             WHERE  b.OType = 4
                                           )
                                AND a.OType = 5 )
								AND c.OType IN ( 1, 2, 3 )
								AND ISNULL(c.OutSourceId,0)=0");

            return MSEntLibSqlHelper.ExecuteDataSetBySql(sbSql.ToString()).Tables[0];
        }
    }
}
