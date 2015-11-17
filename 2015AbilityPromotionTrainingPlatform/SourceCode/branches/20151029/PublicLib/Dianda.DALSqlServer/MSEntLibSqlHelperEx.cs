using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.DALSqlServer
{
    public class MSEntLibSqlHelperEx : MSEntLibSqlHelper
    {

        public static DataSet ExecuteDataSetByStoreProcecdure(string storedProcName, IDataParameter[] cmdInputParms, ref IDataParameter[] cmdOutputParms)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            BuildDBParameter(db, dbCommand, cmdInputParms);
            BuildDBParameterForReturnValue(db, dbCommand, cmdOutputParms);

            DataSet ds = db.ExecuteDataSet(dbCommand);

            foreach (SqlParameter sp in cmdOutputParms)
            {

                sp.Value = db.GetParameterValue(dbCommand, sp.ParameterName);

            }

            return ds;
        }
    }
}
