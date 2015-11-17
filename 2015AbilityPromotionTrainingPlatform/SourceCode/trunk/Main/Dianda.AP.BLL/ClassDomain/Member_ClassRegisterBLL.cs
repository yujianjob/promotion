using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.BLL
{
    public partial class Member_ClassRegisterBLL
    {
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public System.Data.DataTable GetNoGroupper(int classId)
        {
            return dal.GetNoGroupper(classId);
        }
    }
}
