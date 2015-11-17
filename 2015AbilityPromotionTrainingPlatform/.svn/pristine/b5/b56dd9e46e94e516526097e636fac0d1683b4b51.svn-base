using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dianda.AP.DAL;
using Dianda.AP.Model;

namespace Dianda.AP.BLL
{
    public class PeriodBLL
    {
        PeriodDAL dal = new PeriodDAL();

        public DataTable GetTrainingFie()
        {
            return dal.GetTrainingFie();
        }
        public DataTable GetSearch(int AccountId,int PlanId)
        {
            return dal.GetSearch(AccountId, PlanId);
        }
        public DataTable GetListTo(int pageSize, int pageIndex, string where, string orderBy, out int recordCount, int PlanId)
        {
            return dal.GetListTo(pageSize, pageIndex, where, orderBy, out recordCount, PlanId);
        }
        public DataTable List(int pageSize, int pageIndex, string where, string orderBy, out int recordCount, int PlanId)
        {
            return dal.List(pageSize, pageIndex, where, orderBy, out recordCount, PlanId);
        }
    }
}
