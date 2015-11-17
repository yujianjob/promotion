using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.DAL
{
    public partial class Course_UnitContentDAL
    {
        public  int GetUnitCountByClassAndUnitType(int classId, string unitType)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(@"SELECT  COUNT(1)
FROM    Course_UnitContent
WHERE   UnitId IN ( SELECT  id
                    FROM    Course_UnitDetail
                    WHERE   TrainingId = ( SELECT   TraningId
                                           FROM     dbo.Class_Detail
                                           WHERE    id =" + classId + @" ) )
        AND Delflag = 0
        AND UnitType IN ( " + unitType + ")");

            return (int)MSEntLibSqlHelper.ExecuteScalarBySql(sbSql.ToString());

        }

        public DataTable GetUnitByClassAndUnitType(int classId, string unitType)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(@"SELECT *
FROM    Course_UnitContent
WHERE   UnitId IN ( SELECT  id
                    FROM    Course_UnitDetail
                    WHERE   TrainingId = ( SELECT   TraningId
                                           FROM     dbo.Class_Detail
                                           WHERE    id =" + classId + @" ) )
        AND Delflag = 0
        AND UnitType IN ( " + unitType + ")");

            return MSEntLibSqlHelper.ExecuteDataSetBySql(sbSql.ToString()).Tables[0];
        }
    }
}
