using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dianda.AP.Model;
using Dianda.AP.DAL;

namespace Dianda.AP.BLL
{
    public class CertificateBLL
    {
        CertificateDAL dal = new CertificateDAL();

        public DataTable GetTrainingFie()
        {
            return dal.GetTrainingFie();
        }

        public DataTable GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, where, orderBy, out recordCount);
        }
        public DataTable GetExport(string where, string orderBy, int PlanId)
        {
            return dal.GetExport(where, orderBy, PlanId);
        }

        public DataTable GetListTo(int pageSize, int pageIndex, string where, string orderBy, out int recordCount, int PlanId)
        {
            return dal.GetListTo(pageSize, pageIndex, where, orderBy, out recordCount, PlanId);
        }

        public DataTable GetSearch(int AccountId, int PlanId)
        {
            return dal.GetSearch(AccountId, PlanId);
        }

        public bool CertificateCode(int AccountId, int PlanId)
        {
            return dal.CertificateCode(AccountId, PlanId);
        }

        public bool ResultAdd(int AccountId, int PlanId, bool type)
        {
            return dal.ResultAdd(AccountId, PlanId, type);
        }
        public DataTable List(int pageSize, int pageIndex, string where, string orderBy, out int recordCount, int PlanId)
        {
            return dal.List(pageSize, pageIndex, where, orderBy, out recordCount, PlanId);
        }
    }
}
